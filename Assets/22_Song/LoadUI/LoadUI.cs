using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class LoadUI : MonoBehaviour
{
    public GameObject slotUI;
    public GameObject option;
    public Button continueButton;

    public void Start()
    {
        //if (MySaveLoadSystem.CheckSaveFile())
        //if (continueButton != null)
        //{
        //    continueButton.interactable = MySaveLoadSystem<PlaySave>.CheckSaveFile(SaveDataType.PlayerData,0);
        //}
        if (continueButton != null)
        {
            continueButton.interactable = MySaveLoadSystem<PlaySave>.CheckSaveFile(SaveDataType.PlayerData, 0);
        }
    }
    public void OnEnable()
    {
        if (continueButton != null)
        {
            continueButton.interactable = MySaveLoadSystem<PlaySave>.CheckSaveFile(SaveDataType.PlayerData, 0);
        }
        //continueButton.interactable = MySaveLoadSystem<UserData>.CheckSaveFile(SaveDataType.PlayerData);
    }
    
	public void NewGame()
    {
        SceneManager.LoadScene("BattleTutorial");
    }
    
    public void OnSlot()
    {
        slotUI.SetActive(true);
    }
    public void OffSlot()
    {
        slotUI.SetActive(false);
    }
    public void OnOption()
    {
        option.SetActive(true);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
