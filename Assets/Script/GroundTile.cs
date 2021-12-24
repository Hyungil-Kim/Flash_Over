using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class GroundTile : MonoBehaviour
{
	public bool isFill;
	public GroundTile ParentTile;
	public bool attackfloodFill = false;
	public bool movefloodFill = false;
	public bool isPlayer = false;
	public int checkSum = 0;
	public int G, H;
	public GameObject colorTile;
	public Color oldColor;
	public Vector3Int cellpos;
	public List<GroundTile> nextTileList = new List<GroundTile>();
	public List<GameObject> fillList;
	public Tilemap tilemap;

	public GroundTile(int x , int y)
	{
		cellpos.x = x;
		cellpos.y = y;
	}
	public void Awake()
	{
	}
	public void Start()
	{
		oldColor = colorTile.GetComponent<Renderer>().material.color;
		tilemap = transform.GetComponentInParent<Tilemap>();
		cellpos = tilemap.WorldToCell(transform.position);
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
	}
	public int F { get { return G + H; } }

	public void Update()
	{
		CheckFillToRay();
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
			isFill = true;
			foreach(var elem in fillList)
			{
				if (elem.layer == LayerMask.NameToLayer("Player"))
				{
					isPlayer = true;
					break;
				}
					isPlayer = false;
			}
		}
		else
		{
			isFill = false;
			isPlayer = false;
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
	public void ResetExceptColor(Color pathColor)
	{
		attackfloodFill = false;
		movefloodFill = false;
		checkSum = 0;
		colorTile.GetComponent<Renderer>().material.color = pathColor;
	}

}
