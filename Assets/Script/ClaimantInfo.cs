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
        first.text = $"���� : {claimant.weight} ü�� : {claimant.hp}";
        second.text = $"��� : {claimant.airGauge}";
    }
}
