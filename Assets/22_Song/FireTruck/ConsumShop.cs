using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ConsumShop : MonoBehaviour
{
    public TextMeshProUGUI gold;

    private void Update()
    {
        gold.text = $"°ñµå : {GameData.userData.gold}";
    }

}
