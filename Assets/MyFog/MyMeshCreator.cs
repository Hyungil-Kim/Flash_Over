using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMeshCreator : MonoBehaviour
{
    public int vision;
    public float tileSize;
    public float precision;
    List<Vector3> pointVector = new List<Vector3>();
    public Vector3 DirFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void Start()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        var sideCount = ((vision-1) * 4 + 12);
        int vertexCount = (int)(sideCount * precision);
        //Vector3[] vertices = new Vector3[vertexCount];
        List<Vector3> verticesList = new List<Vector3>();
        //var tris = new int[(vertexCount - 2) * 3];
        List<int> trisList = new List<int>();
        //vertices[0] = Vector3.zero;
        

        var xPoint = -(tileSize * 0.5f);
        var yPoint = vision * tileSize + tileSize * 0.5f;
        var startPoint = new Vector3(xPoint, 0, yPoint);

        verticesList.Add(Vector3.zero);
        verticesList.Add(startPoint);

        pointVector.Add(Vector3.zero);
        pointVector.Add(startPoint);

        for (int i = 0; i < (vision + 1)*2; i++)
        {
            if(i% 2 ==0)
            {
                startPoint += new Vector3(tileSize, 0, 0);
            }
            else
            {
                startPoint -= new Vector3(0, 0, tileSize);
            }
            pointVector.Add(startPoint);
        }
        for (int i = 0; i < (vision + 1) * 2 - 1; i++)
        {
            if (i % 2 == 0)
            {
                startPoint -= new Vector3(tileSize, 0, 0);
            }
            else
            {
                startPoint -= new Vector3(0, 0, tileSize);
            }
            pointVector.Add(startPoint);
        }
        for (int i = 0; i < (vision + 1) * 2 - 1; i++)
        {
            if (i % 2 == 0)
            {
                startPoint += new Vector3(0, 0, tileSize);
            }
            else
            {
                startPoint -= new Vector3(tileSize, 0, 0);
            }
            pointVector.Add(startPoint);
        }
        for (int i = 0; i < (vision + 1) * 2 - 1; i++)
        {
            if (i % 2 == 0)
            {
                startPoint += new Vector3(tileSize, 0, 0);
            }
            else
            {
                startPoint += new Vector3(0, 0, tileSize);
            }
            pointVector.Add(startPoint);
        }
        //for (int i = 0; i < pointVector.Count ; i++)
        //{
        //    if (i < pointVector.Count - 2)
        //    {
        //        tris[i * 3] = 0;
        //        tris[i * 3 + 1] = i + 1;
        //        tris[i * 3 + 2] = i + 2;
        //    }
        //}

        for (int i = 0; i < pointVector.Count-1; i++)
        {
            for (int j = 0; j < precision; j++)
            {
                Vector3 lerpVector3 =
                    Vector3.Lerp(pointVector[i], pointVector[i + 1], j/(precision -1));

                var dis = Vector3.Distance(lerpVector3, transform.position);
                var dir = (lerpVector3 - transform.position).normalized;
                //RaycastHit hit;
                //Physics.Raycast(transform.position, dir,dis)
                verticesList.Add(lerpVector3);
            }
        }
        
        var tris = new int[(verticesList.Count - 2) * 3];
        for (int i = 0; i < verticesList.Count; i++)
        {
            if (i < verticesList.Count - 2)
            {
                tris[i * 3] = 0;
                tris[i * 3 + 1] = i + 1;
                tris[i * 3 + 2] = i + 2;
            }
        }
        mesh.Clear();

        mesh.SetVertices(verticesList);
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
