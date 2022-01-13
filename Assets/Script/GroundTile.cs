using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

using UnityEngine.Tilemaps;

[System.Serializable]
public class GroundTile : MonoBehaviour
{
	public bool isWall;
	public GroundTile ParentTile;
	public bool attackfloodFill = false;
	public bool movefloodFill = false;
	public bool isPlayer = false;
	public Claimant isClaimant = null;
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
	public int tileMesh;//임시
	public int tileGrowthExp;

	//smoke
	public GameObject smokePrefab;

	//data
	public bool tileIsWeat;
	public bool tileIsFire;
	public bool tileIsSmoke;

	public int tileWeatValue;
	public int tileExp;
	public float tileHp;

	public int tileSaveSmokeValue;
	public int tileSmokeValue;

	public int index;

	//public TileSaveData data;

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
		sd.firehp = fire.fireHp;

		
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

		fire.fireHp = sd.firehp;
		
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
		//var fire = firePrefab.GetComponentInChildren<ParticleSystem>();
		//var smoke = smokePrefab.GetComponentInChildren<ParticleSystem>();

		//if (tileIsFire)
  //      {
		//	firePrefab.SetActive(true);
  //      }
		//else if(tileIsFire && CheakVision)
  //      {
		//	fire.Play();
		//}
		//else
  //      {
		//	if(fire.isPlaying)
  //          {
		//		fire.Stop();
  //          }
  //      }
		//if (smokePrefab)
		//{
		//	smokePrefab.SetActive(true);
		//}
		//else if (tileIsSmoke && CheakVision)
		//{
		//	smoke.Play();
		//}
		//else
		//{
		//	if (smoke.isPlaying)
		//	{
		//		smoke.Stop();
		//	}
		//}
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
					break;
				}
				else if(elem.tag == "Wall")
				{
					isWall = true;
					break;
				}
				else if(elem.tag == "Claimant")
				{
					isClaimant = elem.GetComponent<Claimant>();
					break;
				}
			}
		}
		else
		{
			isWall = false;
			isPlayer = false;
			isClaimant = null;
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
		var objectsMesh = 0;
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
				tile.tileHp -= fire.fireDamage;
			}
			
			tile.tileHp = tile.tileHp < 0 ? 0 : tileHp;
			
			if(tile.tileHp < 0 && tile.tileGrowthExp >=0)
			{
				tile.tileGrowthExp *= -1;
			}

		}
		tile.tileExp = damage * (tile.tileMesh - weat + objectsMesh);

		if (tile.tileIsFire)
		{
			tile.tileSmokeValue = fire.fireMakeSmoke +tile.tileSaveSmokeValue;
		}
		else
		{
			tile.tileSmokeValue = tile.tileSaveSmokeValue;
		}
	}
	public void CheckParticleOn(GroundTile tile)
	{

		if (tile.tileExp <= 0)//tile.tileExp <= 100
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
				firePrefab.GetComponentInChildren<ParticleSystem>().Play();
			}
			//tile.firePrefab.SetActive(true);
		}

		if (tile.tileSmokeValue < 5 && tile.tag != "Wall")//임시 연기 표시기준
		{
			tile.tileIsSmoke = false;
			//tile.smokePrefab.SetActive(false);
		}
		else if((tile.tileSmokeValue >= 5 && tile.tag != "Wall"))
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
		if(tileIsFire && cheakVision)
		{
			//firePrefab.SetActive(true);
			var particle = firePrefab.GetComponentInChildren<ParticleSystem>();
			particle.Play();
		}
		else
		{
			//firePrefab.SetActive(false);
			firePrefab.GetComponentInChildren<ParticleSystem>().Stop();
		}
		if(tileIsSmoke && cheakVision)
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
		int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | (1 << LayerMask.NameToLayer("UI")));
		layerMask = ~layerMask;
		hits = Physics.RaycastAll(transform.position, transform.up, 10, layerMask);
		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			visionList.Add(hit.collider.gameObject);
		}
        foreach (var elem in visionList)
        {
			if(elem.tag == "Wall" && cheakVision )
            {
				//var material = elem.GetComponent<Renderer>().material;
				//material.renderQueue = 3020;
				//material.shader.FindPropertyIndex("")
            }				
			else if( elem.tag == "Wall" && !cheakVision)
            {
				//var material = elem.GetComponent<Renderer>().material;
				//material.renderQueue = 2000; 
			}
			if(elem.tag == "Claimant" && cheakVision)
            {
				//elem.GetComponent<Renderer>().enabled = true;
				//var material = elem.GetComponent<Renderer>().material;
				//material.renderQueue = 3020;
			}
			else if(elem.tag == "Claimant" && !cheakVision)
            {
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
