using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndUI : MonoBehaviour
{
    public EndScene manager;
    public GameObject content;
    public GameObject endInfoPrefab;
    public TextMeshProUGUI stageName;
    public bool tutorial;
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

        //for (int i = 0; i < 3; i++)
        //{
        //    int index = i;
        //    var newGo = Instantiate(endInfoPrefab, content.transform);
        //    var cd = new CharacterData();
        //    cd.NewSetCharacter();
        //    newGo.GetComponent<EndInfo>().Init(cd, index);
        //}

        stageName.text = $"{ GameData.userData.stageName}";
        //Turn.turnCount = 0;
    }
    public void Init()
    {
        stageName.text = $"{ GameData.userData.stageName}";
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
