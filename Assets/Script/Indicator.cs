//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class Indicator : MonoBehaviour
//{
//    private Vector2 currentVec;//���� ��ġ ����?
//    private Vector2 currentScreenVec;//��ũ�� ���� ����?
//    private float angleRU;//������� �밢�� ����
//    private float screenHalfHeight = 0.5f;//ȭ�� ���� ����
//    private float screenHalfWidt = 0.5f;//ȭ�� �� ����


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
