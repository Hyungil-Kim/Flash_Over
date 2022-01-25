using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;
using UnityEngine.Tilemaps;

[System.Serializable]
public class GroundTile : MonoBehaviour
{
	
	public bool isWall;
	public GroundTile ParentTile;
	public bool attackfloodFill = false;
	public bool movefloodFill = false;
	public bool isPlayer = false;
	public bool isDoor = false;
	public bool isObstacle = false;
	public Claimant isClaimant = null;
	public Obstacle obstacle;
	public int checkSum = 0;
	public int G, H;
	public GameObject colorTile;
	public Color oldColor;
	public Vector3Int cellpos;
	public List<GroundTile> nextTileList = new List<GroundTile>();
	public List<GroundTile> nextVisionList = new List<GroundTile>();
	public List<GameObject> fillList;
	public Tilemap tilemap;
	public GameObject firePrefab;
	public bool cheakVision = false;
	public bool obstacleVision = false;
	public bool CheakVision
    {
        get { return cheakVision; }
		set { cheakVision = value; }
    }
	public int cheakSumVision = 0;
	public int CheakVisionSum
    {
        get { return cheakSumVision; }
		set { cheakSumVision = value; }
    }
	//public int sumVision;

	//tileState
	public int tileArea;
	public float tileMesh;//타일 가중치
	public int tileGrowthExp;// 타일 hp 0일때 가중치 역으로 바꿔줌

	//smoke
	public GameObject smokePrefab;
	public Smoke smoke;
	//data
	public bool tileIsWeat;	//물젖음?
	public bool tileIsFire; 
	public bool tileIsSmoke;
	public bool tileIsClaimant;

	public int tileWeatValue;//물 묻으면 생기는 가중치
	public int tileExp;// 타일 경험치
	public float tileHp; //타일 hp

	public int tileSaveSmokeValue;//연기 다음에 추가될 값
	public int tileSmokeValue;//현재 연기값

	public int index;

	//public TileSaveData data;

	public int infoIndex;

	public GroundTile(int x , int y)
	{
		cellpos.x = x;
		cellpos.y = y;
	}
	public void Awake()
	{
		//firePrefab = GetComponentInChildren<Fire>();
		//AllTile.allTile.Add(index, this);
		AllTile.allTile.Add(this);
		AllTile.SaveTile.Add(this);
	}
	public void Start()
	{
		oldColor = colorTile.GetComponent<Renderer>().material.color;
		tilemap = transform.GetComponentInParent<Tilemap>();
		cellpos = tilemap.WorldToCell(transform.position);
		index = cellpos.x + cellpos.y * tilemap.size.x;
		for (int dir = 0; dir < 4; dir++)
		{
			int[] checkX = { 1, 0, -1, 0 };
			int[] checkY = { 0, 1, 0, -1 };
			int nextX = Mathf.FloorToInt(cellpos.x) + checkX[dir];
			int nextY = Mathf.FloorToInt(cellpos.y) + checkY[dir];
			var nextTileOb = tilemap.GetInstantiatedObject(new Vector3Int(nextX, nextY, 0));
			if (nextTileOb != null)
			{
				var nextTile = nextTileOb.GetComponent<GroundTile>();
				nextTileList.Add(nextTile);
			}
		}

		if(tileIsFire || tileIsSmoke)
		{
			Turn.smokes.Add(this.GetComponentInChildren<Smoke>(true));
		}
		if(tileIsFire)
        {
			firePrefab.SetActive(true);
        }
		CheckParticle();
		//TestFogOfWar();
		//var materials = GetComponentsInChildren<Renderer>();
		//foreach (var renderer in materials)
		//{
		//	renderer.material.renderQueue = 2450;
		//}
	}
	public TileSaveData GetData()
    {
		var sd = new TileSaveData();
		var fire = firePrefab.GetComponent<Fire>();
		sd.tileWeatValue = tileWeatValue;

		sd.tileExp = tileExp;
		sd.tileHp = tileHp;

		sd.tileSaveSmokeValue = tileSaveSmokeValue;
		sd.tileSmokeValue = tileSmokeValue;

		sd.tileIsFire = tileIsFire;
		if(sd.tileIsFire)
        {
			Debug.Log("isFire");
        }

		sd.tileIsWeat = tileIsWeat;
		sd.tileIsSmoke = tileIsSmoke;
		sd.tileIsClaimant = tileIsClaimant;

		if (fire != null)
		{
			sd.firehp = fire.fireHp;
		}
		
		return sd;
    }
	
	public void SaveInit(TileSaveData sd)
    {
		var fire = firePrefab.GetComponent<Fire>();

		tileWeatValue = sd.tileWeatValue;

		tileExp = sd.tileExp;
		tileHp = sd.tileHp;

		tileSaveSmokeValue = sd.tileSaveSmokeValue;
		tileSmokeValue = sd.tileSmokeValue;

		tileIsFire = sd.tileIsFire;
		tileIsWeat = sd.tileIsWeat;
		tileIsSmoke = sd.tileIsSmoke;
		tileIsClaimant = sd.tileIsClaimant;
		if (fire != null)
		{
			fire.fireHp = sd.firehp;
		}
		
		if(tileIsFire)
        {
			Debug.Log("SaveFire");
			firePrefab.SetActive(true);
        }
		else
        {
			if (Turn.fires.Contains(firePrefab.GetComponent<Fire>()))
			{
				Turn.fires.Remove(firePrefab.GetComponent<Fire>());
			}
			firePrefab.SetActive(false);
        }
		if(tileIsSmoke)
        {
			smokePrefab.SetActive(true);
        }
		else
        {
			smokePrefab.SetActive(false);
        }
		CheckParticle();
	}
	public List<GroundTile> SetNextVision(GroundTile playerTile)
    {
		nextVisionList.Clear();
		cellpos = tilemap.WorldToCell(transform.position);
		//if(this == playerTile)
		//{
		//	for (int dir = 0; dir < 4; dir++)
		//	{
		//		int[] indexX = { 1, 0, -1, 0 };
		//		int[] indexY = { 0, 1, 0, -1 };
		//		int nextX = Mathf.FloorToInt(cellpos.x) + indexX[dir];
		//		int nextY = Mathf.FloorToInt(cellpos.y) + indexY[dir];
		//		var nextTileOb = tilemap.GetInstantiatedObject(new Vector3Int(nextX, nextY, 0));
		//		if (nextTileOb != null)
		//		{
		//			var nextTile = nextTileOb.GetComponent<GroundTile>();
		//			nextVisionList.Add(nextTile);
		//		}
		//	}
		//	return nextVisionList;
		//}
		if (this == playerTile)
		{
			
			int[] indexX = { 0, 1, -1 };
			int[] indexY = { 0, 1, -1 };

			for (int i = 0; i < indexX.Length; i++)
			{
				for (int j = 0; j < indexY.Length; j++)
				{
					int nextX = Mathf.FloorToInt(cellpos.x) + indexX[i];
					int nextY = Mathf.FloorToInt(cellpos.y) + indexY[j];
					var nextTileOb = tilemap.GetInstantiatedObject(new Vector3Int(nextX, nextY, 0));
					if (nextTileOb != null)
					{
						var nextTile = nextTileOb.GetComponent<GroundTile>();
						nextTile.cheakSumVision = Mathf.Abs(indexX[i]) + Mathf.Abs(indexY[j]);

						var cheakTileSideOb = tilemap.GetInstantiatedObject(new Vector3Int(nextX, Mathf.FloorToInt(cellpos.y), 0));
						var cheakTileDownOb = tilemap.GetInstantiatedObject(new Vector3Int(Mathf.FloorToInt(cellpos.x), nextY, 0));

						if (nextTile.cheakSumVision == 2 && cheakTileSideOb != null && cheakTileDownOb != null)
                        {
							var cheakTileSide = cheakTileSideOb.GetComponent<GroundTile>();
							var cheakTileDown = cheakTileDownOb.GetComponent<GroundTile>();
							nextTile.cheakSumVision += Mathf.Min(cheakTileSide.tileSmokeValue, cheakTileDown.tileSmokeValue) / 10;
						}
						else if(nextTile.cheakSumVision == 2 && cheakTileSideOb != null)
                        {
							var cheakTileSide = cheakTileSideOb.GetComponent<GroundTile>();
							nextTile.cheakSumVision += cheakTileSide.tileSmokeValue / 10;
						}
						else if (nextTile.cheakSumVision == 2 && cheakTileDownOb != null)
						{
							var cheakTileDown = cheakTileDownOb.GetComponent<GroundTile>();
							nextTile.cheakSumVision += cheakTileDown.tileSmokeValue / 10;
						}
						nextVisionList.Add(nextTile);
					}
				}
			}
			
			if (nextVisionList.Contains(this))
			{
				nextVisionList.Remove(this);
			}
			return nextVisionList;
		}
		var checkX = cellpos.x - playerTile.cellpos.x > 0 ? 1 : -1;
		var checkY = cellpos.y - playerTile.cellpos.y > 0 ? 1 : -1;
		checkX = cellpos.x - playerTile.cellpos.x == 0 ? 0 : checkX;
		checkY = cellpos.y - playerTile.cellpos.y == 0 ? 0 : checkY;

        for (int i = 0; i < 2; i++)
        {
			int nextX = 0;
			int nextY = 0;
			switch (i)
            {
				case 0:
					nextX = Mathf.FloorToInt(cellpos.x);
					nextY = Mathf.FloorToInt(cellpos.y) + checkY;
					break;
				case 1:
					nextX = Mathf.FloorToInt(cellpos.x) + checkX;
					nextY = Mathf.FloorToInt(cellpos.y);
					break;
				case 2:
					nextX = Mathf.FloorToInt(cellpos.x) + checkX;
					nextY = Mathf.FloorToInt(cellpos.y) + checkY;
					break;
				default:
                    break;
            }
			var nextTileOb = tilemap.GetInstantiatedObject(new Vector3Int(nextX, nextY, 0));
			if (nextTileOb != null)
			{
				var nextTile = nextTileOb.GetComponent<GroundTile>();
				if (!nextVisionList.Contains(nextTile))
				{
					//nextTile.sumVision = Mathf.Abs(checkX) + Mathf.Abs(checkY);
					nextVisionList.Add(nextTile);
				}
			}
		}
		if(nextVisionList.Contains(this))
        {
			nextVisionList.Remove(this);
        }
		return nextVisionList;
	}
	public int F { get { return G + H; } }

	public void Update()
	{
		CheckFillToRay();
		TestFogOfWar();

	}

	public void CheckFillToRay()
	{
		fillList = new List<GameObject>();
		RaycastHit[] hits;
		int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | (1 << LayerMask.NameToLayer("UI")));
		layerMask = ~layerMask;
		hits =Physics.RaycastAll(transform.position, transform.up, 10,layerMask);
		for(int i = 0; i <hits.Length;i++)
		{
			RaycastHit hit = hits[i];
			fillList.Add(hit.collider.gameObject);
		}
		if(fillList.Count > 0)
		{
			foreach(var elem in fillList)
			{
				if (elem.tag == "Player")
				{
					isPlayer = true;
				}
				else if(elem.tag == "Wall")
				{
					isWall = true;

				}
				else if(elem.tag == "Claimant")
				{
					isClaimant = elem.GetComponent<Claimant>();
					tileIsClaimant = true;
				}
				else if(elem.tag == "DoorColider")
				{
					isDoor = true;
				}
				else if(elem.layer == LayerMask.NameToLayer("Obstacle"))
				{
					isObstacle = true;
					obstacle = elem.GetComponentInChildren<Obstacle>();
				}
			}
		}
		else
		{
			isWall = false;
			isPlayer = false;
			isClaimant = null;
			tileIsClaimant = false;
			isObstacle = false;
		}
	}

	private bool test;
	public void TestFogOfWar()
    {
		if (!test)
		{
			RaycastHit[] hits;
			int layerMask = 1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("Obstacle");
			layerMask = ~layerMask;
			hits = Physics.RaycastAll(transform.position, transform.up, 10, layerMask);
			foreach (var elem in hits)
			{
				
				test = true;
				var materials = elem.collider.gameObject.GetComponentsInChildren<Renderer>();
				foreach (var renderer in materials)
				{
					renderer.material.renderQueue = 3030;
					if (elem.collider.gameObject.tag == "Wall")
					{
						if (cheakVision)
						{
							renderer.material.color = new Color(1f, 1f, 1f);
						}
						else
						{
							renderer.material.color = new Color(0.5f, 0.5f, 0.5f);
						}
					}
				}
			}
		}
	}
	public void SetTileColor(Color color)
	{
		colorTile.GetComponent<Renderer>().material.color = color;
	}
	public void Reset()
	{
		attackfloodFill = false;
		movefloodFill = false;
		checkSum = 0;
		colorTile.GetComponent<Renderer>().material.color = oldColor;
	}
	public void ResetExceptColor()
	{
		attackfloodFill = false;
		movefloodFill = false;
		checkSum = 0;
	}

	public void ChangeTileState(GroundTile tile,int damage)
	{
		var objectsMesh = 0f;
		for (int i = 0; i < tile.fillList.Count; i++)//오브젝트매질 검사
		{
			if (tile.fillList[i].GetComponent<Obstacle>() != null)
			{
				objectsMesh = tile.fillList[i].GetComponent<Obstacle>().obstacleMesh;
				break;
			}
			objectsMesh = 0;
		}
		var fire = tile.GetComponentInChildren<Fire>(true);
		var weat = 0;
		if(tile.tileIsWeat)
		{
			weat = tileWeatValue;
		}
		else
		{
			weat = 0;
		}
		if (tile.tileIsFire)
		{
			if (tile.tileHp > 0)
			{ 
				tile.tileHp -= fire.data.dmg;
			}
			
			tile.tileHp = tile.tileHp < 0 ? 0 : tileHp;
			
			if(tile.tileHp < 0 && tile.tileGrowthExp >=0)
			{
				tile.tileGrowthExp *= -1;
			}

		}
		tile.tileExp = damage * (int)(tile.tileMesh - weat + objectsMesh);

		if (tile.tileIsFire)
		{
			tile.tileSmokeValue = fire.data.makeSmoke +tile.tileSaveSmokeValue;
		}
		else
		{
			tile.tileSmokeValue = tile.tileSaveSmokeValue;
		}
	}
	public void CheckParticleOn(GroundTile tile)
	{

		if (tile.tileExp <= 1)//tile.tileExp <= 100
		{

			tile.tileIsFire = false;
			tile.firePrefab.SetActive(false);
		}
		else if (tile.tileExp >= 10) //tile.tileExp >= 100
		{	
			tile.tileIsFire = true;
			tile.firePrefab.SetActive(true);
			if (tile.cheakVision)
			{
				var fire = firePrefab.GetComponent<Fire>();
				if(fire != null)
                {
					fire.OnFire();
                }
				//firePrefab.GetComponentInChildren<ParticleSystem>().Play();
			}
			//tile.firePrefab.SetActive(true);
		}

		if (tile.tileSmokeValue < 5 && !tile.isWall)//임시 연기 표시기준
		{
			tile.tileIsSmoke = false;
			//tile.smokePrefab.SetActive(false);
		}
		else if((tile.tileSmokeValue >= 5 && !tile.isWall))
		{
			tile.tileIsSmoke = true;
			tile.smokePrefab.SetActive(true);
			if (tile.cheakVision)
			{
				smokePrefab.GetComponentInChildren<ParticleSystem>().Play();
			}
			//tile.smokePrefab.SetActive(true);
		}
	}
	public void CheckParticle()
	{
		if (tileIsFire && cheakVision)
		{
			//firePrefab.SetActive(true);
			var fire = firePrefab.GetComponent<Fire>();
			if(fire !=null)
            {
				fire.OnFire();
            }
		}
		else if (!tileIsFire || !cheakVision )
		{
			var fire = firePrefab.GetComponent<Fire>();
			if (fire != null)
			{
				fire.OffFire();
			}

		}
		if (tileIsSmoke && cheakVision)
		{
			//smokePrefab.SetActive(true);
			smokePrefab.GetComponent<ParticleSystem>().Play();
		}
		else
		{
			//smokePrefab.SetActive(false);
			smokePrefab.GetComponent<ParticleSystem>().Stop();
		}


		var visionList = new List<GameObject>();
		RaycastHit[] hits;
		int layerMask = 1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("Obstacle");
		layerMask = ~layerMask;
		hits = Physics.RaycastAll(transform.position, transform.up, 10, layerMask);
		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			visionList.Add(hit.collider.gameObject);
		}
        foreach (var elem in visionList)
        {

			if(elem.tag == "Wall" && !cheakVision )
			{
				var materials = elem.GetComponentsInChildren<Renderer>();
				foreach (var renderer in materials)
				{
					renderer.material.color = new Color(0.5f, 0.5f, 0.5f);
				}
			}
			else if (elem.tag == "Wall" && cheakVision)
			{
				var materials = elem.GetComponentsInChildren<Renderer>();
				foreach (var renderer in materials)
				{
					renderer.material.color = new Color(1f, 1f, 1f);
				}
			}

			if(elem.tag == "Claimant" && cheakVision)
            {
				var materials = elem.GetComponentsInChildren<Renderer>();
                foreach (var renderer in materials)
                {
					renderer.enabled = true;
                }

			}
			else if(elem.tag == "Claimant" && !cheakVision)
            {
				var materials = elem.GetComponentsInChildren<Renderer>();
				foreach (var renderer in materials)
				{
					renderer.enabled = false;
				}
				//elem.GetComponent<Renderer>().enabled = false;
			}
			if (elem.tag == "Claimant" && cheakVision)
			{
				var materials = elem.GetComponentsInChildren<Renderer>();
				foreach (var renderer in materials)
				{
					renderer.enabled = true;
				}

			}
			else if (elem.tag == "Claimant" && !cheakVision)
			{
				var materials = elem.GetComponentsInChildren<Renderer>();
				foreach (var renderer in materials)
				{
					renderer.enabled = false;
				}
				//elem.GetComponent<Renderer>().enabled = false;
			}
		}
	}
	public void ParticleOff()
    {
		firePrefab.SetActive(false);
		smokePrefab.SetActive(false);
	}


}
