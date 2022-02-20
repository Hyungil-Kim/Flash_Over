using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

using UnityEngine.Profiling;
public class AllTileMesh : MonoBehaviour
{
    private GameManager gm;
    private TilemapManager tilemapManager;

    private int mapWidth;
    private int mapHeight;

    List<GroundTile> tileList = new List<GroundTile>();
    List<GroundTile> allTile = new List<GroundTile>();
    MeshFilter meshFilter;
    Mesh viewMesh;
    Tilemap tilemap;
    Material fogMaterial;
    public static AllTileMesh instance;

    private Material blurMat;

    private Texture2D texBuffer;

    private Texture2D testTexture;
    private RenderTexture testBuffer;

    private RenderTexture blurBuffer; // 렌더텍스쳐 여러장 쓰는 이유 : 안개를 더 부드럽게 보이게 하기 위해
    private RenderTexture blurBuffer2;

    private RenderTexture curTexture;
    private RenderTexture lerpBuffer;
    private RenderTexture nextTexture;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
      //  LerpBlur();
    }
    private void Start()
    {
        //blurMat = new Material(Shader.Find("FogOfWar/AverageBlur"));
        //texBuffer = new Texture2D(/*mapWidth*/ 128, /*mapHeight*/128, TextureFormat.ARGB32, false);
        //texBuffer.wrapMode = TextureWrapMode.Clamp;

        //int width = (int)(/*mapWidth*/128 * 1.5f);
        //int height = (int)(/*mapHeight*/ 128* 1.5f);    

        //blurBuffer = RenderTexture.GetTemporary(width, height, 0);
        //blurBuffer2 = RenderTexture.GetTemporary(width, height, 0);

        //curTexture = RenderTexture.GetTemporary(width, height, 0);
        //nextTexture = RenderTexture.GetTemporary(width, height, 0);
        //lerpBuffer = RenderTexture.GetTemporary(width, height, 0);

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

        //SetMaterial();
    }
    //public void SetMaterial()
    //{
    //    //Texture2D texture = new Texture2D(128, 128, TextureFormat.ARGB32,false);
    //    //texture.wrapMode = TextureWrapMode.Clamp;
    //    ////var colorBuffer = new Color[10];

    //    //texture.SetPixel(0,0,fogMaterial.color);
    //    //texture.SetPixel(0,1,fogMaterial.color);
    //    //texture.SetPixel(0,2,fogMaterial.color);
    //    //texture.SetPixel(0,3,fogMaterial.color);
    //    //texture.SetPixel(0,4,fogMaterial.color);
    //    //texture.SetPixel(0,5,fogMaterial.color);
    //    //texture.Apply();
    //    //Graphics.Blit(texture, testBuffer, blurMat, 1);

    //    //Graphics.Blit(texBuffer, blurBuffer, blurMat, 0);
    //    //Graphics.Blit(blurBuffer, blurBuffer2, blurMat, 0);
    //    //Graphics.Blit(blurBuffer2, blurBuffer, blurMat, 0);

    //    //Graphics.Blit(blurBuffer, nextTexture);


    //    //testTexture = new Texture2D(16, 16);
    //    //testTexture.Apply();
    //    //Graphics.Blit(testTexture, testBuffer);
    //    //fogMaterial.SetTexture("_MainTex", testBuffer);


    //    //if (curTexture != null)
    //    //{
    //    //    //fogMaterial.SetTexture("_MainTex", curTexture);
    //    //}
    //}
    //public void LerpBlur()
    //{
    //    // CurTexture  -> LerpBuffer
    //    // LerpBuffer  -> "_LastTex"
    //    // NextTexture -> FogTexture [Pass 1 : Lerp]

    //    Graphics.Blit(curTexture, lerpBuffer);
    //    blurMat.SetTexture("_LastTex", lerpBuffer);

    //    Graphics.Blit(nextTexture, curTexture, blurMat, 1);
    //}
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
        //foreach (var tile in allTile)
        //{
        //    var material = tile.GetComponent<Renderer>().material;
        //    material.renderQueue = 3020;
        //}

        //Vector3[] vertices = new Vector3[4];
        //int[] tris = new int[6];
        //var size = tilemap.size;
        //Debug.Log(size);
        //vertices[0] = new Vector3(-size.x/2 , -size.z, -size.y / 2);
        //vertices[1] = new Vector3(size.x / 2, -size.z, -size.y / 2);
        //vertices[2] = new Vector3(-size.x / 2, -size.z, size.y / 2);
        //vertices[3] = new Vector3(size.x / 2, -size.z, size.y / 2);

        //tris[0] = 0;
        //tris[1] = 2;
        //tris[2] = 1;
        //tris[3] = 2;
        //tris[4] = 3;
        //tris[5] = 1;

        viewMesh.vertices = vertices;
        viewMesh.triangles = tris;
        viewMesh.RecalculateNormals();
    }
}
