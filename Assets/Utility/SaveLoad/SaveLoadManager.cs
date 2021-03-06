using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveLoadManager : MonoBehaviour
{
    public void MainSave(int slot)
    {
        var playdata = new PlaySave();
        playdata.isPlay = false;
        playdata.sceanName = SceneManager.GetActiveScene().name;
        playdata.ud = GameData.userData;

        PlaySaveSystem.SaveInPlay(playdata, slot);
    }
    public void OnSave(int slot)
    {
        if(slot == 0)
        {
            GameData.userData.SaveUserData(0);
        }
        var playdata = new PlaySave();
        playdata.sceanName = SceneManager.GetActiveScene().name;
        
        foreach (var tile in AllTile.SaveTile)
        {
            playdata.tsd.Add(tile.index, tile.GetData());
        }
        foreach (var character in Turn.players)
        {
            playdata.psd.Add(character.index, character.GetData());
        }
        foreach (var claimant in Turn.claimants)
        {
            playdata.csd.Add(claimant.index, claimant.GetData());
        }
        foreach (var obstacle in Turn.obstacles)
        {
            playdata.osd.Add(obstacle.index , obstacle.GetData());
        }
        playdata.gsd = new GMSaveData();
        if (GameManager.instance.targetPlayer != null)
        {
            playdata.gsd.targetIndex = GameManager.instance.targetPlayer.index;
        }
        playdata.gsd.turnCount = GameManager.instance.turnCount;

        playdata.inGameTime = $"{GameData.userData.weekend}????";
        playdata.stageName = $"{GameData.userData.stageName}";
        playdata.dateTime = System.DateTime.Now.ToString();
        playdata.userName = $"{GameData.userData.userName}";
        playdata.weekend = GameData.userData.weekend;
        playdata.isPlay = true;

        PlaySaveSystem.SaveInPlay(playdata, slot);
    }
    public void OnLoad(int slot)
    {
        Turn.OnDestroy();
        AllTile.OnDestroy();

        if (slot == 0)
        {
            GameData.userData.LoadUserData(slot);
            SceneManager.LoadScene("MainScene");
            return;
        }
        PlaySaveSystem.LoadInPlay(slot);
        if (PlaySaveSystem.ps != null)
        {
            SceneManager.LoadScene(PlaySaveSystem.ps.sceanName);
        }
    }

    public void BackMainScean()
    {
        SceneManager.LoadScene("MainScene");
    }
}
