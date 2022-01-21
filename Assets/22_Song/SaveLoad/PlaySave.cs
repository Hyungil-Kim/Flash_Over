using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySave : MySaveData
{
    public bool isPlay;

    public string sceanName;

    
    public string stageName;
    public string userName;
    public string inGameTime;
    public string dateTime;


    public Dictionary<int, TileSaveData> tsd = new Dictionary<int, TileSaveData>();
    public Dictionary<int, PlayerSaveData> psd = new Dictionary<int, PlayerSaveData>();
    public Dictionary<int, ClaimantSaveData> csd = new Dictionary<int, ClaimantSaveData>();
    public GMSaveData gsd = new GMSaveData();

    public UserData ud;
    //public UserData ud = GameData.userData;

    public void Init()
    {
        foreach (var player in psd)
        {
            player.Value.cd.LoadCd();
            
        }
        GameData.userData = ud;
    }
}
