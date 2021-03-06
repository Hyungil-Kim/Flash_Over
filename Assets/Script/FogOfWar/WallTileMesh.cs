using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WallTileMesh : MonoBehaviour
{
    List<FadingObject> allTile = new List<FadingObject>();
    MeshFilter meshFilter;
    Mesh viewMesh;

    public Tilemap tilemap;
    Material fogMaterial;
    //public static AllTileMesh instance;

    void Awake()
    {
    }
    private void Start()
    {
        tilemap = GameManager.instance.tilemap;
        Init();
    }
    void Update()
    {

    }

    public void Init()
    {
        allTile = AllTile.wallTile;
        viewMesh = new Mesh { name = "Wall Tile Mesh" };
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = viewMesh;

        fogMaterial = GetComponent<Renderer>().material;

        VisionCheck.Init();

        DrawMesh();
        

        SetMaterial();
    }
    public void SetMaterial()
    {
        Texture2D texture = new Texture2D(128, 128, TextureFormat.ARGB32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        var colorBuffer = new Color[10];

        texture.SetPixel(0, 0, fogMaterial.color);
        texture.SetPixel(0, 1, fogMaterial.color);
        texture.SetPixel(0, 2, fogMaterial.color);
        texture.SetPixel(0, 3, fogMaterial.color);
        texture.SetPixel(0, 4, fogMaterial.color);
        texture.SetPixel(0, 5, fogMaterial.color);
        texture.Apply();
        fogMaterial.SetTexture("_MainTex", texture);
    }
    public void DrawMesh()
    {
        viewMesh.Clear();

        Vector3[] vertices = new Vector3[allTile.Count * 4 * 5];
        int[] tris = new int[allTile.Count * 6 * 5];

        int count = 0;
        foreach (var tile in allTile)
        {
            for (int i = 0; i < 5; i++)
            {
                var worldPos = tilemap.CellToWorld(tile.cellpos);
                var verticesIndex = count * 20 + (4 * i);
                var trisIndex = count * 30 + (6 * i);
                switch (i)
                {
                    case 0:
                        vertices[verticesIndex] = new Vector3(worldPos.x, worldPos.y, worldPos.z);
                        vertices[verticesIndex + 1] = new Vector3(worldPos.x + 1, worldPos.y, worldPos.z);
                        vertices[verticesIndex + 2] = new Vector3(worldPos.x, worldPos.y, worldPos.z + 1);
                        vertices[verticesIndex + 3] = new Vector3(worldPos.x + 1, worldPos.y, worldPos.z + 1);

                        break;
                    case 1:
                        vertices[verticesIndex + 1] = new Vector3(worldPos.x + 0.01f, worldPos.y, worldPos.z + 0.01f);
                        vertices[verticesIndex] = new Vector3(worldPos.x + 1 + 0.01f, worldPos.y, worldPos.z + 0.01f);
                        vertices[verticesIndex + 3] = new Vector3(worldPos.x + 0.01f, worldPos.y -1, worldPos.z + 0.01f);
                        vertices[verticesIndex + 2] = new Vector3(worldPos.x + 1 + 0.01f, worldPos.y -1, worldPos.z + 0.01f);

                        break;
                    case 2:
                        vertices[verticesIndex + 1] = new Vector3(worldPos.x + 1 - 0.01f, worldPos.y, worldPos.z - 0.01f);
                        vertices[verticesIndex] = new Vector3(worldPos.x + 1 - 0.01f, worldPos.y, worldPos.z + 1 - 0.01f);
                        vertices[verticesIndex + 3] = new Vector3(worldPos.x + 1 - 0.01f, worldPos.y - 1, worldPos.z - 0.01f);
                        vertices[verticesIndex + 2] = new Vector3(worldPos.x + 1 - 0.01f, worldPos.y - 1, worldPos.z + 1 - 0.01f);

                        break;
                    case 3:
                        vertices[verticesIndex + 1] = new Vector3(worldPos.x + 1 - 0.01f, worldPos.y, worldPos.z + 1 - 0.01f);
                        vertices[verticesIndex] = new Vector3(worldPos.x - 0.01f, worldPos.y, worldPos.z + 1 - 0.01f);
                        vertices[verticesIndex + 3] = new Vector3(worldPos.x + 1 - 0.01f, worldPos.y - 1, worldPos.z + 1 - 0.01f);
                        vertices[verticesIndex + 2] = new Vector3(worldPos.x - 0.01f, worldPos.y - 1, worldPos.z + 1 - 0.01f);

                        break;
                    case 4:
                        vertices[verticesIndex + 1] = new Vector3(worldPos.x + 0.01f, worldPos.y, worldPos.z + 1 + 0.01f);
                        vertices[verticesIndex] = new Vector3(worldPos.x + 0.01f, worldPos.y, worldPos.z + 0.01f);
                        vertices[verticesIndex + 3] = new Vector3(worldPos.x + 0.01f, worldPos.y -1, worldPos.z + 1 + 0.01f);
                        vertices[verticesIndex + 2] = new Vector3(worldPos.x + 0.01f, worldPos.y - 1, worldPos.z + 0.01f);

                        break;
                    default:
                        break;
                }

                tris[trisIndex] = verticesIndex;
                tris[trisIndex + 1] = verticesIndex + 2;
                tris[trisIndex + 2] = verticesIndex + 1;
                tris[trisIndex + 3] = verticesIndex + 2;
                tris[trisIndex + 4] = verticesIndex + 3;
                tris[trisIndex + 5] = verticesIndex + 1;
            }


            count++;
        }

        viewMesh.vertices = vertices;
        viewMesh.triangles = tris;
        viewMesh.RecalculateNormals();
    }
    //public void DrawMesh()
    //{
    //    viewMesh.Clear();

    //    Vector3[] vertices = new Vector3[allTile.Count * 4 * 5];
    //    int[] tris = new int[allTile.Count * 6 * 5];

    //    int count = 0;
    //    foreach (var tile in allTile)
    //    {
    //        var worldPos = tilemap.CellToWorld(tile.cellpos);
    //        var verticesIndex = count * 4;
    //        var trisIndex = count * 6;
    //        vertices[verticesIndex] = new Vector3(worldPos.x, worldPos.y, worldPos.z);
    //        vertices[verticesIndex + 1] = new Vector3(worldPos.x + 1, worldPos.y, worldPos.z);
    //        vertices[verticesIndex + 2] = new Vector3(worldPos.x, worldPos.y, worldPos.z + 1);
    //        vertices[verticesIndex + 3] = new Vector3(worldPos.x + 1, worldPos.y, worldPos.z + 1);

    //        tris[trisIndex] = verticesIndex;
    //        tris[trisIndex + 1] = verticesIndex + 2;
    //        tris[trisIndex + 2] = verticesIndex + 1;
    //        tris[trisIndex + 3] = verticesIndex + 2;
    //        tris[trisIndex + 4] = verticesIndex + 3;
    //        tris[trisIndex + 5] = verticesIndex + 1;

    //        count++;
    //    }

    //    viewMesh.vertices = vertices;
    //    viewMesh.triangles = tris;
    //    viewMesh.RecalculateNormals();
    //}
}
