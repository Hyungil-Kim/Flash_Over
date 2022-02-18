using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TutorialPlay : MonoBehaviour
{
    public GameObject[] tuto;
    private float totalTime;
    public float delay= 1.5f;
    private int index = 0;

    public int tutoNumber;

    public bool next;
    public bool moveNext;
    private TutorialObject tutoObject;
    //public bool isEmpty;
    //public TutorialPanel[] panals;

    private void OnEnable()
    {
        tuto[0].SetActive(true);

        tutoObject = tuto[index].GetComponent<TutorialObject>();
    }
    public void CheckNext()
    {
        if (totalTime>delay)
        {
            next = tutoObject.CheckNext();
        }
    }
    private void Update()
    {
        if (UIOnOff.instance.ontouch)
        {
            CheckNext();
        }
        if (totalTime > delay)
        {
            //tutoObject = tuto[index].GetComponent<TutorialObject>();
            if (tutoObject.hidepanals != null)
            {
                tutoObject.Open();
            }
        }
        if (next == true)
        {
            if(!UIOnOff.instance.ontouch)
            {
                moveNext = true;
                next = false;
                
            }
        }
        totalTime += Time.deltaTime;
        if(totalTime > delay && moveNext)
        {
            moveNext = false;
            tuto[index].SetActive(false);
            index = Mathf.Clamp(index + 1, 0, tuto.Length);
            tutoObject.HideObj();
            

            if (index >= tuto.Length)
            {
                gameObject.SetActive(false);
                switch (tutoNumber)
                {
                    case 0:
                        GameData.userData.DaewonTuto = true;
                        break;
                    case 1:
                        GameData.userData.HireTuto = true;
                        break;
                    case 2:
                        GameData.userData.TrainingTuto = true;
                        break;
                    case 3:
                        GameData.userData.RestTuto = true;
                        break;
                    case 4:
                        GameData.userData.ShopTuto = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                tuto[index].SetActive(true);
                tutoObject = tuto[index].GetComponent<TutorialObject>();
                if (tutoObject.hidepanals != null)
                {
                    tutoObject.Hide();
                }
                //tuto[index].GetComponent<TutorialObject>().Hide();
                totalTime = 0f;
            }
        }
    }

}
