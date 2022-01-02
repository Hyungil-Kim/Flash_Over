using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCharacter : MonoBehaviour
{
    public TrainingRoom trainingRoom;
    public CharacterData curCharacter;
    public void OnChaIcon()
    {
        trainingRoom.gameObject.SetActive(true);
    }
    public void OnExitRoom()
    {
        trainingRoom.Back();
        trainingRoom.gameObject.SetActive(false);
    }
}
