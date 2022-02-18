using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadUI : MonoBehaviour
{
    public GameObject slotUI;
    public GameObject option;
    public Button continueButton;
    public void Start()
    {
        //if (MySaveLoadSystem.CheckSaveFile())
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
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
