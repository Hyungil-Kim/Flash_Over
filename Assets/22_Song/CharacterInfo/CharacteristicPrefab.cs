using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacteristicPrefab : MonoBehaviour
{
    public TextMeshProUGUI characteristicName;
    public void Init(Buff characteristic)
    {
        characteristicName.text = characteristic.name;
        
    }

}
