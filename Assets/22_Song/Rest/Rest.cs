using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public CharacterInfoList characterInfoList;
    public CharacterInfoStat characterInfoStat;
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
        characterInfoStat.gameObject.SetActive(true);
    }
    public void OnClickRestRoom()
    {
        mainRest.gameObject.SetActive(false);
        //restCharacter.gameObject.SetActive(true);
        characterInfoList.gameObject.SetActive(true);
    }
    public void ExitRestRoom()
    {
        mainRest.gameObject.SetActive(false);
    }
    public void OnClickCharacter()
    {
        popUp.SetActive(true);
    }
    public void OnClickExitCharacter()
    {
        restCharacter.gameObject.SetActive(false);

        characterInfoList.gameObject.SetActive(false);
        characterInfoStat.gameObject.SetActive(false);

        mainRest.gameObject.SetActive(true);
    }
    public void OnUpgradePopup()
    {
        upgradePopup.SetActive(true);
    }
    public void OnClickRest()
    {
        //curCd.restCount = 1;
        GameData.userData.restList.Add(curIndex, curCd);
        //curCd.isRest = true;
        OnClickunRest();
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
