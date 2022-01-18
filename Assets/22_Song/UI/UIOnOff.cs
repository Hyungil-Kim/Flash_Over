using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class UIOnOff : MonoBehaviour
{
    public GameObject[] uiArray;
    private GameObject returnButton;
    private GameObject stage;
    private GameObject stageName;
    
    //private GameObject stageSelsction;
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    private StageSelect stageSelect;
    private void Start()
    {
        foreach (var ui in uiArray)
        {
            uiDict.Add(ui.name, ui);
        }
        GameData.userData.LoadUserData(1);
        returnButton = uiArray[11].transform.gameObject;
        stage= uiArray[13].transform.gameObject;
        //stageSelsction = uiArray[11].transform.gameObject;
    }
    private void Update()
    {
       

    }
    public void Open(string uiName)
    {
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
        }
        uiDict[uiName].SetActive(true);
    }

    public void Closed()
    {
        uiArray[11].SetActive(false);
        uiArray[13].SetActive(true);

    }

    public void OnStart()
    {
        var name= stage.GetComponent<StageSelect>().hits.name;
        Debug.Log(name);
        SceneManager.LoadScene(name);

    }

    public void StageSelect()
    {
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
        }
        returnButton.SetActive(true);
        stage.SetActive(true);

    }

    public void ReturnMainLobby(string uiName)
    {
        
            foreach (var ui in uiArray)
            {
                ui.SetActive(false);
            }
            uiDict[uiName].SetActive(true);
            returnButton.SetActive(false);

    }
    #region scean...babo
    //public void OnInventoryButton()
    //{
    //    SceneManager.LoadScene("InventoryScene");
    //}
    //public void OnCharacterInfoButton()
    //{
    //    SceneManager.LoadScene("CharacterInfoScene");
    //}
    //public void OnCharacterHireButton()
    //{
    //    SceneManager.LoadScene("CharacterHireScene");
    //}
    //public void OnShopButton()
    //{
    //    SceneManager.LoadScene("ShopScene");
    //}
    //public void OnExitButton()
    //{
    //    SceneManager.LoadScene("testMainScene");
    //}
    #endregion
   
}
