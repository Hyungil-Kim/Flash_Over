using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMeshCreator : MonoBehaviour
{
    public LayerMask obstacleLayerMask;

    public bool debug;
    public Transform parentTransform;
    public int vision;
    public float tileSize;
    public float precision;
    List<Vector3> pointVector = new List<Vector3>();

    Vector3 startPoint;
    Mesh viewMesh;
    MeshFilter viewMeshFilter;


    public void Start()
    {
        viewMeshFilter = GetComponent<MeshFilter>();
        PointSetting();
        viewMesh = new Mesh { name = "View Mesh" };
        viewMeshFilter.mesh = viewMesh;
        updateTest();
    }
    private void Update()
    {
        updateTest();
        //transform.position = parentTransform.transform.position;
    }
    public void MovePoint(Vector3 startVector, Vector3 secondVector, bool first = false)
    {
        var maxIndex = (vision + 1) * 2 - 1;
        maxIndex = first ? maxIndex + 1 : maxIndex;
        for (int i = 0; i < maxIndex; i++)
        {
            if (i % 2 == 0)
            {
                startPoint += startVector;
            }
            else
            {
                startPoint += secondVector;
            }
            pointVector.Add(startPoint);
        }
    }
    public void PointSetting()
    {
        pointVector.Clear();

        var xPoint = -(tileSize * 0.5f);
        var yPoint = vision * tileSize + tileSize * 0.5f;
        startPoint = new Vector3(xPoint, transform.position.y, yPoint);

        pointVector.Add(startPoint);
        MovePoint(new Vector3(tileSize, 0, 0), -new Vector3(0, 0, tileSize), true);
        MovePoint(-new Vector3(tileSize, 0, 0), -new Vector3(0, 0, tileSize));
        MovePoint(new Vector3(0, 0, tileSize), -new Vector3(tileSize, 0, 0));
        MovePoint(new Vector3(tileSize, 0, 0), new Vector3(0, 0, tileSize));

    }
    public List<Vector3> VertexSetting()
    {
        PointSetting();
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i < pointVector.Count - 1; i++)
        {
            for (int j = 0; j < precision; j++)
            {
                Vector3 lerpVector3 =
                    Vector3.Lerp(pointVector[i], pointVector[i + 1], j / (precision - 1));
                ObstacleInfo newObstacle = FindObstacles(lerpVector3);
                //viewPoints.Add(newObstacle.point);
                viewPoints.Add(transform.position + lerpVector3);
            }
        }

        //float stepAngleSize = 360f / 50f;
        //List<Vector3> viewPoints = new List<Vector3>();
        //for (int i = 0; i < 50; i++)
        //{
        //    float angle = transform.eulerAngles.y - 360 / 2 + stepAngleSize * i;
        //    ObstacleInfo newObstacle = FindObstacles(angle);

        //    viewPoints.Add(newObstacle.point);
        //}

        List<Vector3> verticesList = new List<Vector3>();

        verticesList.Clear();
        verticesList.Add(Vector3.zero);
        for (int i = 0; i < viewPoints.Count; i++)
        {
            verticesList.Add(transform.InverseTransformPoint(viewPoints[i]));
        }

        return verticesList;
    }
    public int[] TrisSetting(int count)
    {
        var tris = new int[(count - 2) * 3];
        for (int i = 0; i < count; i++)
        {
            if (i < count - 2)
            {
                tris[i * 3] = 0;
                tris[i * 3 + 1] = i + 1;
                tris[i * 3 + 2] = i + 2;
            }
        }
        return tris;
    }

    public void updateTest()
    {
        viewMesh.Clear();
        var vertices = VertexSetting();
        var triangles = TrisSetting(vertices.Count);
        viewMesh.vertices = vertices.ToArray();
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    ObstacleInfo FindObstacles(Vector3 nowVector)
    {

        var vectorAngle = Vector3.Angle(Vector3.forward, nowVector.normalized);
        var angle = transform.eulerAngles.y + vectorAngle;
        Vector3 dir = DirFromAngle(angle, true);
        
        var dis = Vector3.Distance(Vector3.zero, nowVector);
        RaycastHit hit;

        if (DebugRayCast(transform.position, dir, out hit, dis, obstacleLayerMask))
        {
            return new ObstacleInfo(true, hit.point + hit.normal * -0.05f /** -viewDepth*/, hit.distance, angle);
        }
        return new ObstacleInfo(false, transform.position + dir * (dis - 0.05f/*- viewDepth*/), dis, angle);
    }
    ObstacleInfo FindObstacles(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (DebugRayCast(transform.position, dir, out hit, 2.5f, obstacleLayerMask))
        {
            return new ObstacleInfo(true, hit.point + hit.normal * - 0.05f, hit.distance, globalAngle);
        }
        return new ObstacleInfo(false, transform.position + dir * (5 - 1.5f), 5, globalAngle);
    }

    bool DebugRayCast(Vector3 origin, Vector3 direction, out RaycastHit hit, float maxDistance, int mask)
    {
        if (Physics.Raycast(origin, direction, out hit, maxDistance, mask))
        {
            if (debug)
                Debug.DrawLine(origin, hit.point);
            return true;
        }
        if (debug)
            Debug.DrawLine(origin, origin + direction * maxDistance);
        return false;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public struct ObstacleInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ObstacleInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
