using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScene : MonoBehaviour
{
    public UICharacter[] endCharacters;
    public EndUI endUI;
    public int turnCount;
    public string stageName;
    public bool tutorial;
    private AudioController audio;
    public void EndGame()
    {
        foreach (var fireman in GameData.userData.fireManList)
        {
            
            
        }
        GameData.userData.AddExp(GameData.userData.stageExp);
        GameData.userData.gold += GameData.userData.stageGold;
        GameData.userData.StageClear();
        resultSound();
    }
    private void Start()
    {
        var count = 0;
        if (GameData.userData.fireManList != null)
        {
            foreach (var character in GameData.userData.fireManList)
            {
                character.Value.EndStage(Turn.turnCount);
                endCharacters[count].Init(character.Value, count);
                count++;
            }
        }
        endUI.Init();
        turnCount = Turn.turnCount;
        Turn.turnCount = 0;
        EndGame();
        Turn.OnDestroy();
        AllTile.OnDestroy();
    }

    public void resultSound()
    {
        if(Turn.win == true)
        {
            audio.ChangeAudioClip(0);
        }
        else if (Turn.lose==true)
        {
            audio.ChangeAudioClip(1);
        }

    }
    public void BackHome()
    {
        if (tutorial)
        {
            SceneManager.LoadScene("SetName");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
