using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoList : MonoBehaviour
{
    public ChaButton[] chalist;
    private void Start()
    {
        for (int i = 0; i < GameData.userData.characterList.Count; i++)
        {
            chalist[i].Init(GameData.userData.characterList[i]);
        }
    }
}
