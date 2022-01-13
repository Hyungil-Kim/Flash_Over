using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StageSelect : MonoBehaviour
{
    //public GameObject[] Map;
    private GameManager gameManager;
    private UIOnOff uiOnOff;
    private Vector2 touchPos;
	private Vector3 mousePos;
    public GameObject maps;

    // Start is called before the first frame update
    void Start()
    {
        uiOnOff = GameObject.Find("UIOnOff").GetComponent<UIOnOff>();
        //gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

       

    }

    
    public void ScreenTouch(Vector2 callBack)
    {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(callBack);
            int layerMask = 1 << LayerMask.NameToLayer("UI");
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                maps = hit.transform.gameObject;
                Debug.Log(maps.name);
                if(maps!=null)
                {
                    uiOnOff.uiArray[11].SetActive(true);
                    uiOnOff.uiArray[13].SetActive(false);   
                }
            }
    }



    public void GetMousePosition(Vector2 mousePosition)
    {
        mousePos = mousePosition;

    }
}
