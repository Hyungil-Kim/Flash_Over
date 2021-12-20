using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI description;
    private ItemDataBase itemData;
    private ItemType itemType;

    public void Init(ItemDataBase data, ItemType type)
    {
        itemData = data;
        itemType = type;
        switch (type)
        {
            case ItemType.Consumable:
                break;
            case ItemType.Weapon:
                var weapon = data as WeaponData;
                icon.sprite = data.dataTable.iconSprite;
                stat.text = $"Str : {weapon.weaponData.str}";
                stat.text = stat.text.Insert(stat.text.Length, $"\nGrade : {weapon.weaponData.grade}");
                break;
            case ItemType.max:
                break;
            default:
                break;
        }
    }
    public void OnEquipButton()
    {
        var parent = GetComponentInParent<CharacterInfo>();
        var index = parent.currentCharacterIndex;
        var character = GameData.userData.characterList[index];
        switch (itemType)
        {
            case ItemType.Consumable:
                break;
            case ItemType.Weapon:
                character.EquipWeapon(itemData as WeaponData);
                break;
            case ItemType.max:
                break;
            default:
                break;
        }
    }
}
