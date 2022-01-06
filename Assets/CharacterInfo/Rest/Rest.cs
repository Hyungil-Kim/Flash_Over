using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rest : MonoBehaviour
{
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

    public MainRest mainRest;
    public RestCharacter restCharacter;
    public GameObject popUp;
    public GameObject restOver;

    private void OnEnable()
    {
        
        mainRest.test();
        if (GameData.userData.restEndList.Count > 0)
        {
            OnRestOver();
        }
    }


    public void OnClickRestRoom()
    {
        mainRest.gameObject.SetActive(false);
        restCharacter.gameObject.SetActive(true);
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
        mainRest.gameObject.SetActive(true);
    }
    public void OnClickRest()
    {
        curCd.restCount = 1;
        GameData.userData.restList.Add(curIndex, curCd);
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
}
