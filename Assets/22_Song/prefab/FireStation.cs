using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStation : MonoBehaviour
{
    public GameObject menuPanel;
    

    public float CameraMoveSpeed;
    public GameObject[] wayPoints;
    private float[] cameraSize;
    private int prevIndex;
    

    private void Start()
    {
        //cameraSize = new float[wayPoints.Length];
        //for (int i = 0; i < cameraSize.Length; i++)
        //{
        //    cameraSize[i] = float.Parse(wayPoints[i].name.Split('/')[1]);
        //}
    }

    public void OnClick(int index)
    {
        //menuPanel.SetActive(false);
        //StopCoroutine(CoMoveCamera(prevIndex));
        //StopCoroutine(CoZoomCamera(prevIndex));

        StopAllCoroutines();
        StartCoroutine(CoMoveCamera(index));
        //StartCoroutine(CoZoomCamera(index));
        prevIndex = index;

    }
    public void MainMenu()
    {

        //StopCoroutine(CoMoveCamera(prevIndex));
        //StopCoroutine(CoZoomCamera(prevIndex));

        StopAllCoroutines();
        StartCoroutine(CoMoveCamera(wayPoints.Length-1));
        //StartCoroutine(CoZoomCamera(wayPoints.Length - 1));
        prevIndex = wayPoints.Length - 1;
    }
    IEnumerator CoMoveCamera(int index)
    {
        var wayPos = wayPoints[index].transform.position;
        while (Vector3.Distance(wayPos, Camera.main.transform.position) > 0.08f)
        {
            var cameraPos = Camera.main.transform.position;
            Camera.main.transform.position = Vector3.Lerp(cameraPos, wayPos, Time.deltaTime * CameraMoveSpeed);
            yield return 0;
        }
        //if (prevIndex == wayPoints.Length - 1)
        //{
        //    menuPanel.SetActive(true);
        //}
    }
    //IEnumerator CoZoomCamera(int index)
    //{
    //    while (Mathf.Abs(Camera.main.orthographicSize - cameraSize[index]) >= 0.07f)
    //    {
    //        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSize[index], Time.deltaTime * CameraMoveSpeed);
    //        yield return 0;
    //    }
    //    if(prevIndex == cameraSize.Length - 1)
    //    {
    //        menuPanel.SetActive(true);
    //    }
    //}
}
