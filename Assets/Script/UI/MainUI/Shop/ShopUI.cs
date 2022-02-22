using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject shopBuy;
    public GameObject shopSell;
    public GameObject sellItemPanel;
    public void OnBuyUI()
    {
        shopBuy.SetActive(true);
        shopSell.SetActive(false);
    }
    public void OnSellUI()
    {
        shopBuy.SetActive(false);
        shopSell.SetActive(true);
    }
    public void ItemSell()
    {
        sellItemPanel.SetActive(true);
    }
}
