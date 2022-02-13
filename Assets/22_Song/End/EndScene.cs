using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScene : MonoBehaviour
{
    public UICharacter[] endCharacters;
    public EndUI endUI;
    public int turnCount;
    public void EndGame()
    {
        foreach (var fireman in GameData.userData.fireManList)
        {
            fireman.Value.EndStage(Turn.turnCount);
            GameData.userData.AddExp(50);
            GameData.userData.gold += 900;
        }
        GameData.userData.StageClear();
    }
    private void Start()
    {
        var count = 0;
        if (GameData.userData.fireManList != null)
        {
            foreach (var character in GameData.userData.fireManList)
            {
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
    public void BackHome()
    {
        SceneManager.LoadScene("MainScene");
    }
}
