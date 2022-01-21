using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;
public class UIOnOff : MonoBehaviour
{
    public GameObject[] uiArray;
    private GameObject returnButton;
    private GameObject stage;
    private GameObject stageName;
    private OffScreenIndicator offScreen;

    [SerializeField]
    private TextMeshProUGUI text;

    //private GameObject stageSelsction;
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    private StageSelect stageSelect;

    private Indicator Indicator;
    private string mapName;
    private void Start()
    {
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
        text.text = $"현장: {MyDataTableMgr.stageInfoTable.GetTable(level-1).sceneOfFire}\n"+
           $"난이도: {MyDataTableMgr.stageInfoTable.GetTable(level-1).level}\n"+
           $"요구조자: {MyDataTableMgr.stageInfoTable.GetTable(level-1).survivor}명\n"+
           $"구조자: {MyDataTableMgr.stageInfoTable.GetTable(level-1).rescuer}명\n"+
           $"현장설명: {MyDataTableMgr.stageInfoTable.GetTable(level-1).descreption}";
           mapName = MyDataTableMgr.stageInfoTable.GetTable(level - 1).map;
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
