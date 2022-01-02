using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManCharacter : MonoBehaviour
{
    public FireManInfoPrefab fireMan;
    public Transform content;
    private List<GameObject> prefabList = new List<GameObject>();
    private void Start()
    {

    }
    private void OnEnable()
    {
        if(prefabList.Count < GameData.userData.maxFireMan)
        {
            var listCount = prefabList.Count;
            for (int i = 0; i < GameData.userData.maxFireMan - listCount; i++)
            {
                var newGo = Instantiate(fireMan.gameObject, content);
                newGo.SetActive(false);
                prefabList.Add(newGo);
            }
        }
        int count = 0;
        foreach (var cd in GameData.userData.fireManList)
        {
            prefabList[count].SetActive(true);
            var fireManInfo = prefabList[count].GetComponent<FireManInfoPrefab>();
            fireManInfo.Init(cd.Value);
            count++;
        }
    }
}
