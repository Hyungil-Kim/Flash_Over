using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndUI : MonoBehaviour
{
    public GameObject content;
    public GameObject endInfoPrefab;

    private void Start()
    {
        //if (GameData.userData.fireManList != null)
        //{
        //    int count = 0;
        //    foreach (var character in GameData.userData.fireManList)
        //    {
        //        var newGo = Instantiate(endInfoPrefab, content.transform);
        //        newGo.GetComponent<EndInfo>().Init(character.Value, count);
        //        count++;
        //    }
        //}

        for (int i = 0; i < 3; i++)
        {
            int index = i;
            var newGo = Instantiate(endInfoPrefab, content.transform);
            var cd = new CharacterData();
            cd.NewSetCharacter();
            newGo.GetComponent<EndInfo>().Init(cd, index);
        }
    }
    public void Init()
    {
        if (GameData.userData.fireManList != null)
        {
            int count = 0;
            foreach (var character in GameData.userData.fireManList)
            {
                var newGo = Instantiate(endInfoPrefab, content.transform);
                newGo.GetComponent<EndInfo>().Init(character.Value, count);
                count++;
            }
        }
    }
}
