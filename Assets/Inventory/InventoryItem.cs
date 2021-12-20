using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject CharacterSelectPanal;
    public void OnEquipButton()
    {
        CharacterSelectPanal.SetActive(true);
    }
}
