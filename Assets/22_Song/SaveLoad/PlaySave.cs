using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySave : MySaveData
{
    public string sceanName;

    public Dictionary<int, TileSaveData> tsd = new Dictionary<int, TileSaveData>();
    public Dictionary<int, PlayerSaveData> psd = new Dictionary<int, PlayerSaveData>();
    public Dictionary<int, ClaimantSaveData> csd = new Dictionary<int, ClaimantSaveData>();
    public GMSaveData gsd = new GMSaveData();

    public UserData ud = GameData.userData;

    public void Init()
    {

    }
}
