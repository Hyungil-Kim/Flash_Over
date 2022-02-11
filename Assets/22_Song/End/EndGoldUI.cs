using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGoldUI : MonoBehaviour
{
    public TextMeshProUGUI gold;
    public TextMeshProUGUI clearGold;
    public TextMeshProUGUI saveClaimant;
    public TextMeshProUGUI turnGold;

    public int testGold = 900;

    public GameObject detailInfo;

    public void Start()
    {
        gold.text = $"보상 : {testGold}";
        clearGold.text = $"클리어 보상 : {500}";
        saveClaimant.text = $"구조한 사람의 수 : {3} => {300}";
        turnGold.text = $"클리어한 턴 수 : {15} => {100}";
    }
    public void Detail(bool onoff)
    {
        if(onoff)
        {
            detailInfo.SetActive(true);
        }
        else
        {
            detailInfo.SetActive(false);
        }
    }
}
