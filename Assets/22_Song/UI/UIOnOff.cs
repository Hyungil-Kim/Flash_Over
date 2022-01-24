using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using TMPro;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class UIOnOff : MonoBehaviour
{
    //��ġ�ƹ������ص� uiâ ������°� �ϰ� �;
    public MoveControlor mousePoint;

    public GameObject[] uiArray;
    private GameObject returnButton;
    private GameObject stage;
    private GameObject stageName;


    public GameObject notEnoughMoney;

    private OffScreenIndicator offScreen;

    [SerializeField]
    private TextMeshProUGUI text;


    //private GameObject stageSelsction;
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    private StageSelect stageSelect;

    public static UIOnOff instance;
    private Indicator Indicator;
    private string mapName;
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
        //mousePoint.Enable();//mouse������ ����\
        //EnhancedTouchSupport.Enable();
        if (PlaySaveSystem.ps != null)
        {
            PlaySaveSystem.ps.isPlay = false;
        }

        //�ӽ÷� �ּ�ó��
        offScreen = uiArray[14].GetComponent<OffScreenIndicator>();
        
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
        SceneManager.LoadScene(mapName);
    }

    public void StageSelect()
    {

        for (int i = 0; i < uiArray.Length-1; i++)
        {
            uiArray[i].SetActive(false);
        }
        stage.SetActive(true);
        returnButton.SetActive(true);
        uiArray[14].SetActive(true);

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


    public void OverviewOftheSite(int level)
    {
        text.text = $"����: {MyDataTableMgr.stageInfoTable.GetTable(level-1).sceneOfFire}\n"+
           $"���̵�: {MyDataTableMgr.stageInfoTable.GetTable(level-1).level}\n"+
           $"�䱸����: {MyDataTableMgr.stageInfoTable.GetTable(level-1).survivor}��\n"+
           $"������: {MyDataTableMgr.stageInfoTable.GetTable(level-1).rescuer}��\n"+
           $"���弳��: {MyDataTableMgr.stageInfoTable.GetTable(level-1).descreption}";
           mapName = MyDataTableMgr.stageInfoTable.GetTable(level - 1).map;
    }

    public void OnNotEnoughMoney()
    {
        notEnoughMoney.SetActive(true);
    }
    public void OffNotEnoughMoney()
    {
        notEnoughMoney.SetActive(false);
    }
    

}
