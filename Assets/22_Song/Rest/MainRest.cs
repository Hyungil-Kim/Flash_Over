using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainRest : MonoBehaviour
{
    public int maxSlot = 4;
    public int baseSlot = 2;
    public GameObject restPrefab;

    List<GameObject> restPrefabList = new List<GameObject>();
    List<CharacterData> restCharacterList = new List<CharacterData>();

    Rest rest;
    List<RestUpgradePrefab> restUpgradePrefab = new List<RestUpgradePrefab>();



    private void Start()
    {
        rest = GetComponentInParent<Rest>();
        var prefabs = rest.restUpgrade.GetComponentsInChildren<RestUpgradePrefab>();
        
        foreach (var prefab in prefabs)
        {
            restUpgradePrefab.Add(prefab);
        }
    }
    private void OnEnable()
    {
        if(restPrefabList.Count == 0)
        {
            for (int i = 0; i < maxSlot; i++)
            {
                var index = i;
                var newGo = Instantiate(restPrefab, transform);
                newGo.GetComponent<RestPrefab>().init(null, false, index);
                restPrefabList.Add(newGo);
            }
        }
        
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < baseSlot + GameData.userData.restShopData.count; i++)
        {
            var index = i;
            GameData.userData.restList.TryGetValue(i, out var cd);
            restPrefabList[i].GetComponent<RestPrefab>().init(cd, true, index);
        }
    }
    public void RestRoomLevelUp()
    {
        GameData.userData.restRoomLevel = Mathf.Clamp(
            GameData.userData.restRoomLevel + 1, 0, maxSlot-baseSlot);
        Init();
    }

    public void EndStage()
    {
        List<int> keyList = new List<int>();
        foreach (var cd in GameData.userData.restList)
        {
            cd.Value.restCount++;
            if(cd.Value.restCount == 3)
            {
                cd.Value.isRest = false;
                cd.Value.tiredScore = 0;
                keyList.Add(cd.Key);
                GameData.userData.restEndList.Add(cd.Value);
            }
        }
        foreach (var key in keyList)
        {
            GameData.userData.restList.Remove(key);
        }
    }

    public void test()
    {
        EndStage();
        //Init();
    }


    public void Upgrade(int index)
    {
        var upgradeIndex = (RestUpgrade)index;
        switch (upgradeIndex)
        {
            case RestUpgrade.Count:
                if (GameData.userData.gold >= 1000)
                {
                    GameData.userData.restShopData.count = Mathf.Clamp(GameData.userData.restShopData.count + 1, 0, maxSlot - baseSlot);
                    Init();
                }
                else
                {
                    rest.ui.OnNotEnoughMoney();
                }
                break;
            case RestUpgrade.Remove:
                GameData.userData.restShopData.remove = Mathf.Clamp(GameData.userData.restShopData.remove + 1, 0, maxSlot - baseSlot);
                Init();
                break;
            default:
                break;
        }
        foreach (var prefab in restUpgradePrefab)
        {
            prefab.Init();
        }
    }
}
