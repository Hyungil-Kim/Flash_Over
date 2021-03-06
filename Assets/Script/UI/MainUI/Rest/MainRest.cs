using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MainRest : MonoBehaviour
{
    public int maxSlot = 4;
    public int baseSlot = 2;
    public float probability;
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
        for (int i = 0; i < MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.count).RS4Count; i++)
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
                switch (cd.Value.admission)
                {
                    case Admission.None:
                        break;
                    case Admission.Rest:
                        cd.Value.tiredScore -= MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.tired).RS1Tired;
                        break;
                    case Admission.Hospital:
                        var list = cd.Value.buff.Where((x) => x.isBadCharacteristic && x.isPhysical).ToList();
                        var random = Random.Range(0, list.Count);
                        cd.Value.buff.Remove(list[random]);
                        cd.Value.badCharacteristics.Remove(list[random]);
                        break;
                    case Admission.Phycho:
                        var psycho = cd.Value.buff.Where((x) => x.isBadCharacteristic && x.isPsychological).ToList();
                        var psychorandom = Random.Range(0, psycho.Count);
                        cd.Value.buff.Remove(psycho[psychorandom]);
                        cd.Value.badCharacteristics.Remove(psycho[psychorandom]);
                        break;
                    default:
                        break;
                }
                //cd.Value.tiredScore -= 15 + 5 * GameData.userData.restShopData.tired;
                cd.Value.admission = Admission.None;
                //cd.Value.RemoveBad(probability);
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


    public void Upgrade()
    {
        if (GameData.userData.gold >= 1000)
        {
            GameData.userData.restShopData.count = Mathf.Clamp(GameData.userData.restShopData.count + 1, 0, maxSlot - baseSlot);
            Init();
        }
        else
        {
            rest.ui.OnNotEnoughMoney();
        }
        foreach (var prefab in restUpgradePrefab)
        {
            prefab.Init();
        }
    }
}
