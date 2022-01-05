using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
