using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class UIOnOff : MonoBehaviour
{
    //터치아무데나해도 ui창 사라지는거 하고 싶어서
    public MoveControlor mousePoint;

    public GameObject[] uiArray;
    private GameObject returnButton;
    private GameObject stage;
    private GameObject stageName;

    public GameObject notEnoughMoney;

    //private GameObject stageSelsction;
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    private StageSelect stageSelect;

    public static UIOnOff instance;
    private void Start()
    {
        instance = this;
        foreach (var ui in uiArray)
        {
            uiDict.Add(ui.name, ui);
        }
        //GameData.userData.LoadUserData(1);
        if(GameData.userData.characterList.Count == 0)
        {
            var cd = new CharacterData();
            cd.SettingFixCharacter(10, 10, 10, 5, 5);
        }
        returnButton = uiArray[11].transform.gameObject;
        stage= uiArray[13].transform.gameObject;
        //stageSelsction = uiArray[11].transform.gameObject;

        //mousePoint = new MoveControlor();
        //mousePoint.Mouse.TestTouch.started += val => OffNotEnoughMoney();
        //mousePoint.Enable();//mouse포지션 실행\
        //EnhancedTouchSupport.Enable();
        if (PlaySaveSystem.ps != null)
        {
            PlaySaveSystem.ps.isPlay = false;
        }
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

    public void OnNotEnoughMoney()
    {
        notEnoughMoney.SetActive(true);
    }
    public void OffNotEnoughMoney()
    {
        notEnoughMoney.SetActive(false);
    }
    
}
