using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOnOff : MonoBehaviour
{
    public GameObject mainLobby;
    public GameObject chiefRoom;
    public GameObject shop;
    public GameObject shopBuy;
    public GameObject shopSell;
    public GameObject hireFireMan;
    public GameObject fireManInfo;
    public GameObject daewonList;

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
