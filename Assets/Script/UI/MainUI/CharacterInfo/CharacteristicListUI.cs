using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacteristicListUI : MonoBehaviour
{
    public GameObject detailUI;
    public TextMeshProUGUI characterisicName;
    public TextMeshProUGUI characterisicDesc;

    public void Init(Buff characteristic)
    {
        detailUI.SetActive(true);
        characterisicName.text = characteristic.data.name;
        characterisicDesc.text = characteristic.data.desc;
    }
    public void OffDetailUI()
    {
        detailUI.SetActive(false);
    }
}
