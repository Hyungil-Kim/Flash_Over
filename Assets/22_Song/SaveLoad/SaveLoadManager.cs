using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveLoadManager : MonoBehaviour
{
    public void OnSave(int slot)
    {
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
        playdata.gsd = new GMSaveData();
        if (GameManager.instance.targetPlayer != null)
        {
            playdata.gsd.targetIndex = GameManager.instance.targetPlayer.index;
        }
        playdata.gsd.turnCount = GameManager.instance.turnCount;
        PlaySaveSystem.SaveInPlay(playdata, slot);
    }
    public void OnLoad(int slot)
    {
        PlaySaveSystem.LoadInPlay(slot);
        Turn.OnDestroy();
        AllTile.SaveTile.Clear();
        AllTile.allTile.Clear();
        AllTile.visionTile.Clear();
        AllTile.prevVisionTile.Clear();
        AllTile.wallTile.Clear();
        SceneManager.LoadScene(PlaySaveSystem.ps.sceanName);
    }
}
