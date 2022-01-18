using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadUI : MonoBehaviour
{
    public GameObject slotUI;
    
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
}
