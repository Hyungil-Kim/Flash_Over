using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClaimantInfo : MonoBehaviour
{
    public TextMeshProUGUI first;
    public TextMeshProUGUI second;

    public void Init(Claimant claimant)
    {
        first.text = $"무게 : {claimant.weight} 체력 : {claimant.hp}";
        second.text = $"산소 : {claimant.airGauge}";
    }
}
