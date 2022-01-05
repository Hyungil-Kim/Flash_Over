using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MyMeshCreate : MonoBehaviour
{
    private GameManager gm;
    private TilemapManager tilemapManager;
    public VisionRange[] visionRanges;
    public FogProjector fogProjector;

    List<GroundTile> tileList = new List<GroundTile>();
    MeshFilter meshFilter;
    Mesh viewMesh;
    Tilemap tilemap;

    private void Start()
    {
        gm = GameManager.instance;
        tilemapManager = gm.tilemapManager;
        tilemap = gm.tilemap;
        viewMesh = new Mesh { name = "View Mesh" };
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = viewMesh;
        AddTile();
        DrawMesh();

        Turn.TurnOff = new Turn.turnOff(Test);
    }
    public void AddTile()
    {
        tileList.Clear();
        for (int i = 0; i < visionRanges.Length; i++)
        {
            visionRanges[i].ResetTile();
            var visionRange = visionRanges[i].CheackVision();
            for (int j = 0; j < visionRange.Count; j++)
            {
                if (!tileList.Contains(visionRange[j]))
                {   
                    visionRange[j].CheckParticle();
                    tileList.Add(visionRange[j]);
                }
            }
            visionRanges[i].CheckPrevTile();
        }
    }
    public void Test()
    {
        AddTile();
        DrawMesh();
        //fogProjector.UpdateFog();
    }
    public void DrawMesh()
    {
        viewMesh.Clear();   

        Vector3[] vertices = new Vector3[tileList.Count * 4];
        int[] tris = new int[tileList.Count*6];
        //{
        //    // lower left triangle
        //    0, 2, 1,
        //    // upper right triangle
        //    2, 3, 1,

        //    4, 6, 5,
        //    6, 7, 5
        //};
        for (int i = 0; i < tileList.Count; i++)
        {
            var worldPos = tilemap.CellToWorld(tileList[i].cellpos);
            var verticesIndex = i * 4;
            var trisIndex = i * 6;
            vertices[verticesIndex] = new Vector3(worldPos.x, 5, worldPos.z);
            vertices[verticesIndex + 1] = new Vector3(worldPos.x + 1, 5, worldPos.z);
            vertices[verticesIndex + 2] = new Vector3(worldPos.x, 5, worldPos.z + 1);
            vertices[verticesIndex + 3] = new Vector3(worldPos.x + 1, 5, worldPos.z + 1);

            tris[trisIndex] = verticesIndex;
            tris[trisIndex + 1] = verticesIndex + 2;
            tris[trisIndex + 2] = verticesIndex + 1;
            tris[trisIndex + 3] = verticesIndex + 2;
            tris[trisIndex + 4] = verticesIndex + 3;
            tris[trisIndex + 5] = verticesIndex + 1;
        }


        viewMesh.vertices = vertices;
        viewMesh.triangles = tris;
        viewMesh.RecalculateNormals();
    }
}
