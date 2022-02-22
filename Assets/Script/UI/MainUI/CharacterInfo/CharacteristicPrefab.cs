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
    public Buff characteriticData;
    public void Init(Buff characteristic)
    {
        characteristicName.text = characteristic.data.name;
        characteristicDesc.text = characteristic.data.desc;
        characteriticData = characteristic;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //descPanel.SetActive(true);
        var listUi = GetComponentInParent<CharacteristicListUI>();
        listUi.Init(characteriticData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //descPanel.SetActive(false);
        var listUi = GetComponentInParent<CharacteristicListUI>();
        listUi.OffDetailUI();
    }
}
