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
        desc.text = $"Ȯ�忡 �ʿ��Ѻ���� 1000����Դϴ�.\n���� Ȯ���Ͻðڽ��ϱ�?";
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
