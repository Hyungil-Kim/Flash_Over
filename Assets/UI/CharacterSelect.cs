using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public int maxCharacter = 20;

    public InventoryItemList currentInventory;
    public GameObject characterIconPrepab;
    public GameObject content;

    public List<GameObject> characterIcons = new List<GameObject>();

    private void Start()
    {
        if(characterIcons.Count == 0)
        {
            for (int i = 0; i < maxCharacter; i++)
            {
                var go = Instantiate(characterIconPrepab, content.transform);
                go.SetActive(false);
                characterIcons.Add(go);
            }
        }
        foreach (var icon in characterIcons)
        {
            icon.SetActive(false);
        }
        int count = 0;
        foreach (var character in GameData.userData.characterList)
        {
            var go = characterIcons[count];
            go.SetActive(true);
            var button = go.GetComponent<Button>();
            var image = go.GetComponent<Image>();
            button.onClick.AddListener(() => EquipItem(character));
            count++;
        }
        //왜 for문으로 돌리면 안됨 ??허허

        //for (int i = 0; i < GameData.userData.characterList.Count; i++)
        //{
        //    var go = characterIcons[i];
        //    go.SetActive(true);
        //    var button = go.GetComponent<Button>();
        //    var image = go.GetComponent<Image>();
        //    button.onClick.AddListener(() => EquipItem(GameData.userData.characterList[i]));
        //}
    }

    public void EquipItem(CharacterData character)
    {
        Debug.Log(character.GetStat(CharacterStatType.STR));
        switch (currentInventory.CurrentItemType)
        {
            case ItemType.Consumable:
                break;
            case ItemType.Weapon:
                var itemObject = currentInventory.SelectItem.GetComponent<ItemPrefab>();
                character.EquipWeapon(itemObject.itemData as WeaponData);
                break;
            default:
                break;
        }
        Debug.Log(character.GetStat(CharacterStatType.STR));
        gameObject.SetActive(false);
    }
}
