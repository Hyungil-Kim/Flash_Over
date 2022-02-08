using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManagement : MonoBehaviour
{
    public List<GameObject> uiArray = new List<GameObject>();
    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    public void ShopUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                GameData.userData.itemShopData.maxItem++;
                break;
            case 1:
                GameData.userData.itemShopData.sale++;
                break;
            case 2:
                GameData.userData.itemShopData.durability++;
                break;
            default:
                break;
        }
    }
    public void HireUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                GameData.userData.chaShopData.countUpgrade++;
                break;
            case 1:
                GameData.userData.chaShopData.gradeUpgrade++;
                break;
            case 2:
                GameData.userData.chaShopData.characteristicUpgrade++;
                break;

            default:
                break;
        }
    }
    public void DaewonUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                GameData.userData.maxCharacter++;
                break;
            default:
                break;
        }
    }
    public void RestUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                GameData.userData.restShopData.tired++;
                break;
            case 1:
                GameData.userData.restShopData.psychological++;
                break;
            case 2:
                GameData.userData.restShopData.physical++;
                break;
            default:
                break;
        }
    }
    public void TrainingUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                GameData.userData.traingShopData.str++;
                break;
            case 1:
                GameData.userData.traingShopData.hp++;
                break;
            case 2:
                GameData.userData.traingShopData.lung++;
                break;
            default:
                break;
        }
    }
    public void Open(string uiName)
    {
        foreach (var ui in uiArray)
        {
            ui.SetActive(false);
            if(ui.name == uiName)
            {
                ui.SetActive(true);
            }
        }
    }
}
