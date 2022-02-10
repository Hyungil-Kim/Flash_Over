using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCharacter : MonoBehaviour
{
    public TrainingRoom trainingRoom;
    public CharacterData curCharacter;
    public int curIndex;

    public CharacterInfoStat characterInfoStat;
    public CharacterInfoList characterInfoList;
    public void OnChaIcon()
    {
        //trainingRoom.gameObject.SetActive(true);
        characterInfoStat.gameObject.SetActive(true);
    }
    public void OnTraining()
    {
        characterInfoList.gameObject.SetActive(false);
        characterInfoStat.gameObject.SetActive(false);

        trainingRoom.gameObject.SetActive(true);
    }
    public void OnExitRoom()
    {
        trainingRoom.Back();
        trainingRoom.gameObject.SetActive(false);

        characterInfoList.gameObject.SetActive(true);
        characterInfoStat.gameObject.SetActive(true);
    }
}
