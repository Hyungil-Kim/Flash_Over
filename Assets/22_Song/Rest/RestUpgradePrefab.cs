using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestUpgradePrefab : MonoBehaviour
{
    public int index;
    public Button button;
    public Text restName;
    public Text restDesc;
    public Text restUpgrade;
    public void Init()
    {
        var upgradeIndex = (RestUpgrade)index;
        switch (upgradeIndex)
        {
            case RestUpgrade.Count:
                restUpgrade.text = $"업그레이드\n비용 : 1000";
                if(GameData.userData.restShopData.count >= 2)
                {
                    button.interactable = false;
                    restUpgrade.text = $"완료";
                }

                break;
            case RestUpgrade.Remove:
                restUpgrade.text = $"업그레이드\n비용 : 600";
                break;
            default:
                break;
        }   
    }
}
