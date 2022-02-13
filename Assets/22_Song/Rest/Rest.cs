using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum RestType
{
    Tired,
    Physical,
    Psycholosical
}
public enum RestUpgrade
{
    Count,
    Remove
}
public class RestUpgradeData
{
    public int count;
    public int tired;
    public int psychological;
    public int physical;

}
public class Rest : MonoBehaviour
{
    public UIOnOff ui;
    public int curIndex;
    public int CurIndex
    {
        get {return curIndex; }
        set { curIndex = value; }
    }
    public CharacterData curCd;
    public CharacterData CurCd
    {
        get { return curCd; }
        set { curCd = value; }
    }
    public int curCharacterIndex;

    public MainRest mainRest;
    public RestCharacter restCharacter;
    public GameObject popUp;
    public GameObject restOver;
    public GameObject restUpgrade;
    public GameObject upgradePopup;

    public GameObject daewonList;
    public CharacterInfoList characterInfoList;
    public CharacterInfoStat characterInfoStat;

    public RestType restType;
    private void OnEnable()
    {
        
        //mainRest.test();
        if (GameData.userData.restEndList.Count > 0)
        {
            OnRestOver();
        }
    }

    public void OnChaIcon()
    {
        //trainingRoom.gameObject.SetActive(true);
        characterInfoStat.Init();
        characterInfoStat.gameObject.SetActive(true);
    }
    public void OnClickRestRoom()
    {
        mainRest.gameObject.SetActive(false);
        //restCharacter.gameObject.SetActive(true);
        daewonList.SetActive(true);
        //characterInfoList.gameObject.SetActive(true);
    }
    public void ExitRestRoom()
    {
        mainRest.gameObject.SetActive(false);
    }
    public void OnPopUp()
    {
        popUp.SetActive(true);
    }
    public void OnClickExitCharacter()
    {
        restCharacter.gameObject.SetActive(false);

        //characterInfoList.gameObject.SetActive(false);
        
        characterInfoStat.gameObject.SetActive(false);
        daewonList.SetActive(false);

        mainRest.gameObject.SetActive(true);
    }
    public void OnUpgradePopup()
    {
        upgradePopup.SetActive(true);
    }
    public void OnClickCharacter()
    {
        GameData.userData.restList.Add(curIndex, curCd);
        curCd.isFireAble = false;
        OnClickunRest();
    }
    public void OnClickRest()
    {
        //curCd.restCount = 1;
        switch (restType)
        {
            case RestType.Tired:
                curCd.admission = Admission.Rest;
                break;
            case RestType.Physical:
                curCd.admission = Admission.Hospital;
                GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Physical;
                break;
            case RestType.Psycholosical:
                curCd.admission = Admission.Phycho;
                GameData.userData.gold -= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Psychological;
                break;
            default:
                break;
        }
        //curCd.isRest = true;
        mainRest.Init();
        popUp.SetActive(false);
        
        
        //OnClickunRest();
    }
    
    public void OnClickunRest()
    {
        popUp.SetActive(false);
        OnClickExitCharacter();
    }
    public void OnRestOver()
    {
        restOver.SetActive(true);
        var name = "";
        foreach (var cd in GameData.userData.restEndList)
        {
            name = name.Insert(name.Length, $", {cd.characterName}");
            name = name.Remove(0, 1);
        }
        restOver.GetComponentInChildren<TextMeshProUGUI>().text = $"{name} ÀÇ ÈÞ½ÄÀÌ ³¡³µ½À´Ï´Ù.";
        GameData.userData.restEndList.Clear();
    }
    public void OnRestOverExit()
    {
        restOver.SetActive(false);
    }
    public void OnRestUpgrade()
    {
        restUpgrade.SetActive(true);
    }
    public void OffRestUpgrade()
    {
        restUpgrade.SetActive(false);
    }

}
