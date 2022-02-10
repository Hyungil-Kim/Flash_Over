using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using TMPro;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;
using AdvancedPeopleSystem;
public class UIOnOff : MonoBehaviour
{
    //��ġ�ƹ������ص� uiâ ������°� �ϰ� �;
    public MoveControlor mousePoint;

    public GameObject[] uiArray;
    private GameObject returnButton;
    private GameObject stage;
    private GameObject stageName;

    public FireStation fireStation;
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
    public Vector2 mousepos;

    public GameObject character;
    public List<GameObject> uiCharacterList = new List<GameObject>();

    public GameObject mainCameraPos;
    public GameObject selectCameraPos;

    public GameObject offscreanIndicatorPanel;
    private void Start()
    {
        Camera.main.transform.position = mainCameraPos.transform.position;
        Camera.main.transform.rotation = mainCameraPos.transform.rotation;
        var uiCharacters = GameObject.FindGameObjectsWithTag("UICharacter");
        foreach (var uicharacter in uiCharacters)
        {
            uiCharacterList.Add(uicharacter);
        }

        instance = this;
        foreach (var ui in uiArray)
        {
            uiDict.Add(ui.name, ui);
        }
        //GameData.userData.LoadUserData(1);
        if(GameData.userData.characterList.Count == 0)
        {
            var cd = new CharacterData();
            cd.SettingFixCharacter(20, 10, 50, 5, 5);
        }
        returnButton = uiArray[8].transform.gameObject;
        stage= uiArray[10].transform.gameObject;
        //stageSelsction = uiArray[11].transform.gameObject;

        mousePoint = new MoveControlor();
        //mousePoint.Mouse.TestTouch.started += val => OffNotEnoughMoney();
        mousePoint.Mouse.Move.performed += val => GetMousePos(val.ReadValue<Vector2>());
        mousePoint.Enable();
        EnhancedTouchSupport.Enable();
        if (PlaySaveSystem.ps != null)
        {
            PlaySaveSystem.ps.isPlay = false;
        }

        //�ӽ÷� �ּ�ó��
        offScreen = offscreanIndicatorPanel.GetComponent<OffScreenIndicator>(); 
        
        //stageSelsction = uiArray[11].transform.gameObject;
    }
    private void Update()
    {
       
    }
    public void GetMousePos(Vector2 pos)
    {
        mousepos = pos;
    }
    public void Open(string uiName)
    {
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
        }
        if (uiName == "MainLobby" || uiName == "FacilityManagement")
        {
            fireStation.OnClick(5);
        }

        uiDict[uiName].SetActive(true);
        
    }
    IEnumerator CoMain()
    {
        yield return new WaitForSeconds(1.5f);
    }
    public void Closed()
    {
        uiArray[8].SetActive(false);
        uiArray[10].SetActive(true);
        //uiArray[15].SetActive(false);
        //character.SetActive(false);
    }

    public void OnStart()
    { 
        SceneManager.LoadScene(mapName);
    }

    public void StageSelect()
    {
        StopAllCoroutines();
        Camera.main.transform.position = selectCameraPos.transform.position;
        Camera.main.transform.rotation = selectCameraPos.transform.rotation;

        
        for (int i = 0; i < uiArray.Length-1; i++)
        {
            uiArray[i].SetActive(false);
        }
        //offscreanIndicatorPanel.SetActive(true);
        stage.SetActive(true);
        returnButton.SetActive(true);

        //character.SetActive(false);

    }

    public void ReturnMainLobby(string uiName)
    {
        offscreanIndicatorPanel.SetActive(false);
        Camera.main.transform.position = mainCameraPos.transform.position;
        Camera.main.transform.rotation = mainCameraPos.transform.rotation;
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
        }
        uiDict[uiName].SetActive(true);
        returnButton.SetActive(false);

        //character.SetActive(true);

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
    
    public void SettingCharacter()
    {
        for (int i = 0; i < GameData.userData.characterList.Count; i++)
        {
            var custom = uiCharacterList[i].GetComponent<CharacterCustomization>();
            var uicha = uiCharacterList[i].GetComponent<UICharacter>();

            var customInfo = GameData.userData.characterList[i].setupModel;
            customInfo.ApplyToCharacter(custom);
            uicha.Init(i);
        }
    }
}
