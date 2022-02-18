using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadUI : MonoBehaviour
{
    public GameObject slotUI;
    public GameObject option;
    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
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
