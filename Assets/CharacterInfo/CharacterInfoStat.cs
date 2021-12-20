using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoStat : MonoBehaviour
{
    public Image weapon;
    public Image armor;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI move;
    public TextMeshProUGUI str;
    public TextMeshProUGUI def;
    private CharacterInfo characterInfo;
    private CharacterData curCharacter;
    //private void Start()
    //{
    //    Init();
    //}
    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        characterInfo = GetComponentInParent<CharacterInfo>();
        var index = characterInfo.currentCharacterIndex;
        curCharacter = GameData.userData.characterList[index];
        if (curCharacter.weapon != null)
        {
            weapon.sprite = curCharacter.weapon.dataTable.iconSprite;
        }
        hp.text = $"Hp : {curCharacter.totalStats.hp}";
        move.text = $"Move : {curCharacter.totalStats.move}";
        str.text = $"Str : {curCharacter.totalStats.str}";
        def.text = $"Def : {curCharacter.totalStats.def}";
    }

    public void OnWeaponButton()
    {
        characterInfo.currentItemType = ItemType.Weapon;
        characterInfo.OnInventory();
    }
}
