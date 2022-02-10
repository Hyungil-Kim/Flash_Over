using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpbarData : MonoBehaviour
{
    public TextMeshProUGUI maxFireman;
    public TextMeshProUGUI gold;

    private void Update()
    {
        maxFireman.text = $"{GameData.userData.characterList.Count} / {GameData.userData.maxCharacter}";
        gold.text = $"{GameData.userData.gold}";
    }
}
