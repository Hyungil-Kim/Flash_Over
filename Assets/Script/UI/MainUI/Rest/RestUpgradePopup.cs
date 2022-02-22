using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestUpgradePopup : MonoBehaviour
{
    public MainRest mainRest;
    public TextMeshProUGUI desc;
    private void OnEnable()
    {
        desc.text = $"확장에 필요한비용은 1000골드입니다.\n정말 확장하시겠습니까?";
    }
    public void Upgrade()
    {
        mainRest.Upgrade();
        gameObject.SetActive(false);
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
