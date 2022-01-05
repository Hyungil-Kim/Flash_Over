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
        for (int i = 0; i < baseSlot + GameData.userData.restRoomLevel; i++)
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
                cd.Value.tiredScore = 0;
                keyList.Add(cd.Key);
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
        Init();
    }
}
