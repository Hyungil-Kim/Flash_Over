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
    public GameObject tutorial;
    public TutorialPlay[] tutorialPlays;
    public TutorialPlay worldmapTuto;

    //터치아무데나해도 ui창 사라지는거 하고 싶어서
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
    public GameObject MainCharacterUi;
    public List<GameObject> fireTruckCharacterList = new List<GameObject>();

    public GameObject mainCameraPos;
    public GameObject selectCameraPos;

    public GameObject offscreanIndicatorPanel;

    public GameObject timeSpend;
    public TextMeshProUGUI timetext;
    private float totalTime;
    public float delayTime= 3f;

    public Shop shop;

    public bool ontouch;

    private int mapLevel;
    private void Start()
    {
        Camera.main.transform.position = mainCameraPos.transform.position;
        Camera.main.transform.rotation = mainCameraPos.transform.rotation;
        var uiCharacters = GameObject.FindGameObjectsWithTag("UICharacter");
        foreach (var uicharacter in uiCharacters)
        {
            uiCharacterList.Add(uicharacter);
        }
        var fireTruckCharacters = GameObject.FindGameObjectsWithTag("FireTruckCharacter");
        foreach (var uicharacter in fireTruckCharacters)
        {
            fireTruckCharacterList.Add(uicharacter);
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
        mousePoint.Mouse.TestTouch.started += val => OnTouch();
        mousePoint.Mouse.TestTouch.canceled += val => OffTouch();
        mousePoint.Mouse.Move.performed += val => GetMousePos(val.ReadValue<Vector2>());
        mousePoint.Enable();
        EnhancedTouchSupport.Enable();
        if (PlaySaveSystem.ps != null)
        {
            PlaySaveSystem.ps.isPlay = false;
        }

        //임시로 주석처리
        offScreen = offscreanIndicatorPanel.GetComponent<OffScreenIndicator>();

        //stageSelsction = uiArray[11].transform.gameObject;

        //timeSpend.SetActive(true);
        timetext.text = $"{ GameData.userData.weekend} 주 차";
        StartCoroutine(CoFadeIn());
        
        if(GameData.userData.stageClear)
        {
            shop.ShopUpdate();
            GameData.userData.stageClear = false;
        }
    }
    public IEnumerator CoFadeIn()
    {
        while (totalTime < 2.5f)
        {
            totalTime += Time.deltaTime;
            timetext.alpha = Mathf.Clamp(totalTime/1.5f, 0, 1f);
            yield return 0;
        }
        timeSpend.SetActive(false);

    }
    private void Update()
    {
        
    }
    public void GetMousePos(Vector2 pos)
    {
        mousepos = pos;
    }
    public void WorldMapTutorial()
    {
        if(!GameData.userData.WorldmapTuto)
        {
            worldmapTuto.gameObject.SetActive(true);
        }
    }
    public void Open(string uiName)
    {
        switch (uiName)
        {
            case "DaewonList":
                if(!GameData.userData.DaewonTuto)
                {
                    tutorialPlays[0].gameObject.SetActive(true);
                }
                break;
            case "HireFireMan":
                if (!GameData.userData.HireTuto)
                {
                    tutorialPlays[3].gameObject.SetActive(true);
                }
                break;
            case "Training":
                if (!GameData.userData.TrainingTuto)
                {
                    tutorialPlays[1].gameObject.SetActive(true);
                }
                break;
            case "Rest":
                if (!GameData.userData.RestTuto)
                {
                    tutorialPlays[2].gameObject.SetActive(true);
                }
                break;
            case "Shop":
                if (!GameData.userData.ShopTuto)
                {
                    tutorialPlays[4].gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
        }
        if (uiName == "MainLobby")
        {
            //fireStation.OnClick(5);
            StartCoroutine(BackMainMenu());
        }
        else if( uiName == "FacilityManagement")
        {
            StartCoroutine(BackFacilityManagement());
        }
        else
        {
            uiDict[uiName].SetActive(true);
        }
        
    }
    IEnumerator BackMainMenu()
    {
        fireStation.StopAllCoroutines();
        yield return StartCoroutine(fireStation.CoMoveCamera(5));

        uiDict["MainLobby"].SetActive(true);
    }
    IEnumerator BackFacilityManagement()
    {
        fireStation.StopAllCoroutines();
        yield return StartCoroutine(fireStation.CoMoveCamera(5));

        uiDict["FacilityManagement"].SetActive(true);
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
        offscreanIndicatorPanel.SetActive(true);
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
       
        text.text = $"● 현장: {MyDataTableMgr.stageInfoTable.GetTable(level).sceneOfFire}\n"+
           $"● 난이도: {DifficultyLevel(level)}\n" +
           $"● 요구조자: {MyDataTableMgr.stageInfoTable.GetTable(level).survivor}명\n"+
           $"● 구조자: {MyDataTableMgr.stageInfoTable.GetTable(level).rescuer}명\n"+
           $"● 현장설명:\n {MyDataTableMgr.stageInfoTable.GetTable(level).descreption}\n"+
           $"● 임무:\n {MyDataTableMgr.stageInfoTable.GetTable(level).mission}";

        mapName = MyDataTableMgr.stageInfoTable.GetTable(level).map;
        mapLevel = MyDataTableMgr.stageInfoTable.GetTable(level).level;
        GameData.userData.gofireman = MyDataTableMgr.stageInfoTable.GetTable(level).rescuer;
        GameData.userData.stageName = MyDataTableMgr.stageInfoTable.GetTable(level).sceneOfFire;
    }

    private string DifficultyLevel(int level)
    {
        if (MyDataTableMgr.stageInfoTable.GetTable(level).level == 1)return "★";
        else if (MyDataTableMgr.stageInfoTable.GetTable(level).level == 2) return "★★";
        else  return "★★★";
    }

    public void OnNotEnoughMoney()
    {
        notEnoughMoney.SetActive(true);
    }
    public void OffNotEnoughMoney()
    {
        notEnoughMoney.SetActive(false);
    }
    public void OnTouch()
    {
        ontouch = true;
        //notEnoughMoney.SetActive(true);
    }
    public void OffTouch()
    {
        ontouch = false;
        //notEnoughMoney.SetActive(false);
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
    public void MainCharacterSetting(CharacterData cd)
    {
        var custom = MainCharacterUi.GetComponent<CharacterCustomization>();
        var customInfo = cd.setupModel;
        customInfo.ApplyToCharacter(custom);
    }
    public void MainCharacterSetting(CharacterData cd, int index)
    {
        var custom = fireTruckCharacterList[index].GetComponent<CharacterCustomization>();
        var customInfo = cd.setupModel;
        customInfo.ApplyToCharacter(custom);
    }
    bool tuto;
    public void NextTutorial(int index)
    {
        //int index = (int)type;
        tutorialPlays[index].moveNext = true;
    }
}
