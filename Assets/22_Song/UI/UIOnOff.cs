using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOnOff : MonoBehaviour
{


    public GameObject[] uiArray;
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    private void Start()
    {
        foreach (var ui in uiArray)
        {
            uiDict.Add(ui.name, ui);
        }
        GameData.userData.LoadUserData(1);
    }
    public void Open(string uiName)
    {
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
        }
        uiDict[uiName].SetActive(true);
    }

    public void OnStart()
    {
        SceneManager.LoadScene("SampleTileMap");
    }
    #region scean...babo
    //public void OnInventoryButton()
    //{
    //    SceneManager.LoadScene("InventoryScene");
    //}
    //public void OnCharacterInfoButton()
    //{
    //    SceneManager.LoadScene("CharacterInfoScene");
    //}
    //public void OnCharacterHireButton()
    //{
    //    SceneManager.LoadScene("CharacterHireScene");
    //}
    //public void OnShopButton()
    //{
    //    SceneManager.LoadScene("ShopScene");
    //}
    //public void OnExitButton()
    //{
    //    SceneManager.LoadScene("testMainScene");
    //}
    #endregion

}
