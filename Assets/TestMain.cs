using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMain : MonoBehaviour
{
    public void OnInventoryButton()
    {
        SceneManager.LoadScene("InventoryScene");
    }
    public void OnCharacterInfoButton()
    {
        SceneManager.LoadScene("CharacterInfoScene");
    }
    public void OnCharacterHireButton()
    {
        SceneManager.LoadScene("CharacterHireScene");
    }
    public void OnShopButton()
    {
        SceneManager.LoadScene("ShopScene");
    }
    public void OnExitButton()
    {
        SceneManager.LoadScene("testMainScene");
    }
}
