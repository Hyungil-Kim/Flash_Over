using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class CharacteristicPrefab : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject descPanel;
    public TextMeshProUGUI characteristicName;
    public TextMeshProUGUI characteristicDesc;
    public void Init(Buff characteristic)
    {
        characteristicName.text = characteristic.name;
        characteristicDesc.text = characteristic.desc;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descPanel.SetActive(false);
    }
}
