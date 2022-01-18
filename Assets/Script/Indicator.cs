//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class Indicator : MonoBehaviour
//{
//    private Vector2 currentVec;//현재 위치 벡터?
//    private Vector2 currentScreenVec;//스크린 상의 벡터?
//    private float angleRU;//우측상단 대각선 각도
//    private float screenHalfHeight = 0.5f;//화면 높이 절반
//    private float screenHalfWidt = 0.5f;//화면 폭 절반


//    // Start is called before the first frame update
//    void Start()
//    {
//        currentScreenVec = Camera.main.WorldToScreenPoint();
//        currentVec = Camera.main.WorldToViewportPoint();

//        Vector2 vecRU = new Vector2(Screen.width, Screen.height) - currentScreenVec;
//        vecRU = vecRU.normalized;
//        angleRU = Vector2.Angle(vecRU, Vector2.up);
//    }
//    public void DrawIndicator(GameObject obj, GameObject indicatorObj)
//    {
//        Image indicator = indicatorObj.GetComponent<Image>();
        
//        Vector2 objScreenVec = Camera.main.WorldToScreenPoint();
//        Vector2 objVec = Camera.main.WorldToScreenPoint();

//        Vector2 targetVec = objScreenVec = currentScreenVec;
//        targetVec = targetVec.normalized;

//        float targetAngle = Vector2.Angle(targetVec, Vector2.up);//0~180
//        int sign = Vector3.Cross(targetVec, Vector2.up).z < 0 ? -1 : 1;
//        targetAngle *= sign;//-180~180


//        float xPrime=
        
//    }



//    // Update is called once per frame
//    void Update()
//    {
     
        
//    }
//}
