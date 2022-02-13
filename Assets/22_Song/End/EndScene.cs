using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScene : MonoBehaviour
{
    public EndCharacter[] endCharacters;
    public void EndGame()
    {
        foreach (var fireman in GameData.userData.fireManList)
        {
            fireman.Value.EndStage(Turn.turnCount);
            GameData.userData.AddExp(50);

            GameData.userData.gold += 900;
        }
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


    }

    public void resultSound()
    {
        
    }
    public void BackHome()
    {
        SceneManager.LoadScene("MainScene");
    }
}
