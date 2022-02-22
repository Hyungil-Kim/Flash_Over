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

    public GameObject detailInfo;

    public void Start()
    {
        var manager = GetComponentInParent<EndUI>().manager;
        gold.text = $"보상 : {manager.stageGold + manager.saveClaimant * 100 + 100}";
        clearGold.text = $"클리어 보상 : {manager.stageGold}";
        saveClaimant.text = $"구조한 사람의 수 : {manager.saveClaimant} => {manager.saveClaimant * 100}";
        turnGold.text = $"클리어한 턴 수 : {manager.turnCount} => {100}";
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
