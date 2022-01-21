using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlreadyEquip : MonoBehaviour
{
    public Image chaIcon;
    public TextMeshProUGUI description;

    private CharacterData cd;
    private ItemDataBase id;
    private ItemType it;
    public delegate void isExit();
    public isExit exit;
    public void Init(CharacterData character, ItemDataBase itemData, ItemType itemType)
    {
        exit = null;
        cd = character;
        id = itemData;
        it = itemType;
        //chaIcon.sprite = character.
        description.text = $"'{cd.characterName}'이(가) 착용중입니다.\n해제하고 장착하시겠습니까?";
    }
    public void OnYes()
    {
        //임시로 처리 나중에 UI추가해야할듯 ?
        if (cd.weight > id.dataTable.weight)
        {
            cd.EquipItem(id, it);
        }
        if(exit != null)
        {
            exit();
        }
        gameObject.SetActive(false);
    }
    public void OnNo()
    {

        gameObject.SetActive(false);
    }
}
