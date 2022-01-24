using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
public class StageSelect : MonoBehaviour
{

    private GameObject[] buildings;
    private GameManager gameManager;
    private UIOnOff uiOnOff;
    private Vector3 prevPos;
    private Vector3 mousePos;
    public GameObject hits;
    public List<GameObject> levels;
    public List<GameObject> level1;
    public List<GameObject> level2;
    public List<GameObject> level3;
    public int random;
    private bool mapDrag;

    // Start is called before the first frame updatez
    private void Awake()
    {
        buildings = GameObject.FindGameObjectsWithTag("Building");

        levels = new List<GameObject>(buildings);
        DivideByLevel();
        RandomSelect();
    }
    void Start()
    {
        uiOnOff = GameObject.Find("UIOnOff").GetComponent<UIOnOff>();
        //gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
       

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LateUpdate()
    {
        if (mapDrag)
        {
            CameraMove();
        }
    }
    public void CameraMove()
    {
        var currPos = mousePos;
        currPos.z = 50f;
        var pos1 = Camera.main.ScreenToWorldPoint(currPos);
        prevPos.z = 50f;
        var pos2 = Camera.main.ScreenToWorldPoint(prevPos);

        var delta = pos2 - pos1;

        delta.y = 0f;

        Camera.main.transform.position = Camera.main.transform.position + delta;

        prevPos = currPos;
    }

    public void ScreenDrag(Vector2 callBack)
    {
        prevPos = callBack;
        mapDrag = true;
    }

    public void ScreenMove(Vector2 callBack)
    {
        mousePos = callBack;
    }

    public void ScreenTouch(Vector2 callBack)
    {

        //RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(callBack);
        //    int layerMask = 1 << LayerMask.NameToLayer("UI");
        //    layerMask = ~layerMask;
        //    if (Physics.Raycast(ray, out hit, float.PositiveInfinity,layerMask))
        //    {
        //        hits = hit.transform.gameObject;

        //        if (hits != null&&hits.gameObject.GetComponentInChildren<VisualEffect>().enabled==true)
        //        {
        //            uiOnOff.uiArray[12].SetActive(true);
        //            uiOnOff.uiArray[13].SetActive(false);
        //        }
        //    }
    }

    private void DivideByLevel()
    {

        foreach (var level in levels)
        {
            if (level.GetComponent<Buildings>().level == 1)
            {
                level1.Add(level);
            }
            if (level.GetComponent<Buildings>().level == 2)
            {
                level2.Add(level);
            }
            if (level.GetComponent<Buildings>().level == 3)
            {
                level3.Add(level);
            }
        }
    }

    public void RandomSelect()
    
    {
        random = Random.Range(0, 6);
        level1[random].GetComponentInChildren<VisualEffect>().enabled = true;
        level2[random].GetComponentInChildren<VisualEffect>().enabled = true;
        level3[random].GetComponentInChildren<VisualEffect>().enabled = true;

    }

    public void GetMousePosition(Vector2 mousePosition)
    {
        mousePos = mousePosition;

    }

}
