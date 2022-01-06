using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
public class AllTileMesh : MonoBehaviour
{
    private GameManager gm;
    private TilemapManager tilemapManager;

    List<GroundTile> tileList = new List<GroundTile>();
    List<GroundTile> allTile = new List<GroundTile>();
    MeshFilter meshFilter;
    Mesh viewMesh;
    Tilemap tilemap;
    Material fogMaterial;
    public static AllTileMesh instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    public void Init()
    {
        gm = GameManager.instance;
        tilemapManager = gm.tilemapManager;
        tilemap = gm.tilemap;
        viewMesh = new Mesh { name = "All Tile Mesh" };
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = viewMesh;

        fogMaterial = GetComponent<Renderer>().material;

        VisionCheck.Init();

        UpdateFog();
        Turn.TurnOff += UpdateFog;

        SetMaterial();
    }
    public void SetMaterial()
    {
        Texture2D texture = new Texture2D(128, 128, TextureFormat.ARGB32,false);
        texture.wrapMode = TextureWrapMode.Clamp;
        var colorBuffer = new Color[10];
        
        texture.SetPixel(0,0,fogMaterial.color);
        texture.SetPixel(0,1,fogMaterial.color);
        texture.SetPixel(0,2,fogMaterial.color);
        texture.SetPixel(0,3,fogMaterial.color);
        texture.SetPixel(0,4,fogMaterial.color);
        texture.SetPixel(0,5,fogMaterial.color);
        texture.Apply();
        fogMaterial.SetTexture("_MainTex", texture);
    }

    public void UpdateFog()
    {
        ExceptVisiable();
        DrawMesh();
    }
    public void ExceptVisiable()
    {
        allTile = AllTile.allTile;
        foreach (var tile in tileList)
        {
            if(!allTile.Contains(tile))
            allTile.Add(tile);
        }
        VisionCheck.AddTile();
        foreach (var tile in AllTile.visionTile)
        {
            if(allTile.Contains(tile))
            {
                if (!tileList.Contains(tile))
                {
                    tileList.Add(tile);
                }
                allTile.Remove(tile);
            }
        }
    }
    public void DrawMesh()
    {
        viewMesh.Clear();

        Vector3[] vertices = new Vector3[allTile.Count * 4];
        int[] tris = new int[allTile.Count * 6];

        int count = 0;
        foreach (var tile in allTile)
        {
            var worldPos = tilemap.CellToWorld(tile.cellpos);
            var verticesIndex = count * 4;
            var trisIndex = count * 6;
            vertices[verticesIndex] = new Vector3(worldPos.x, worldPos.y, worldPos.z);
            vertices[verticesIndex + 1] = new Vector3(worldPos.x + 1, worldPos.y, worldPos.z);
            vertices[verticesIndex + 2] = new Vector3(worldPos.x, worldPos.y, worldPos.z + 1);
            vertices[verticesIndex + 3] = new Vector3(worldPos.x + 1, worldPos.y, worldPos.z + 1);

            tris[trisIndex] = verticesIndex;
            tris[trisIndex + 1] = verticesIndex + 2;
            tris[trisIndex + 2] = verticesIndex + 1;
            tris[trisIndex + 3] = verticesIndex + 2;
            tris[trisIndex + 4] = verticesIndex + 3;
            tris[trisIndex + 5] = verticesIndex + 1;
            count++;
        }

        viewMesh.vertices = vertices;
        viewMesh.triangles = tris;
        viewMesh.RecalculateNormals();
    }
}
