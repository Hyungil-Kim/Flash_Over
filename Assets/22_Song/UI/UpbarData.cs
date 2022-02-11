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
        maxFireman.text = $"{GameData.userData.characterList.Count} / {MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Count}";
        gold.text = $"{GameData.userData.gold}";
    }
}
