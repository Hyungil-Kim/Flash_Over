using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    private int maxCharacter = GameData.userData.maxCharacter;

    public AlreadyEquip alreadyEquip;
    public InventoryItemList currentInventory;
    public GameObject characterIconPrepab;
    public GameObject content;

    private List<GameObject> characterIcons = new List<GameObject>();

    private void OnEnable()
    {
        if (characterIcons.Count < GameData.userData.maxCharacter)
        {
            var count = characterIcons.Count;
            for (int i = 0; i < GameData.userData.maxCharacter - count; i++)
            {
                var go = Instantiate(characterIconPrepab, content.transform);
                go.SetActive(false);
                characterIcons.Add(go);
            }
        }
    }
    private void Start()
    {
        if(characterIcons.Count == 0)
        {
            for (int i = 0; i < GameData.userData.maxCharacter; i++)
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
        //int count = 0;
        //foreach (var character in GameData.userData.characterList)
        //{

        //    var go = characterIcons[count];
        //    go.SetActive(true);
        //    var button = go.GetComponent<Button>();
        //    var image = go.GetComponent<Image>();
        //    button.onClick.AddListener(() => EquipItem(character));
        //    count++;
        //}
        //왜 for문으로 돌리면 안됨 ??허허
        //고거슨 이벤트안에 들어갈때 변수가 저거 돼서 그럼 저저 뭐시기야 저 .. 네 ..
        for (int i = 0; i < GameData.userData.characterList.Count; i++)
        {
            var index = i;
            var go = characterIcons[index];
            go.SetActive(true);
            var button = go.GetComponent<Button>();
            var image = go.GetComponent<Image>();
            button.onClick.AddListener(() => OnEquipItem(GameData.userData.characterList[index]));
        }
    }

    public void OnEquipItem(CharacterData character)
    {
        var itemObject = currentInventory.SelectItem.GetComponent<ItemPrefab>();
        if (itemObject.itemData != null && itemObject.itemData.owner != null)
        {
            alreadyEquip.Init(character, itemObject.itemData, currentInventory.CurrentItemType);
            alreadyEquip.gameObject.SetActive(true);
            alreadyEquip.exit += (() => gameObject.SetActive(false));
            alreadyEquip.exit = currentInventory.Init;
            return;
        }
        character.EquipItem(itemObject.itemData, currentInventory.CurrentItemType);
        currentInventory.Init();
        //switch (currentInventory.CurrentItemType)
        //{
        //    case ItemType.Consumable:
        //        break;
        //    case ItemType.Weapon:
        //        var itemObject = currentInventory.SelectItem.GetComponent<ItemPrefab>();
        //        character.EquipWeapon(itemObject.itemData as WeaponData);
        //        break;
        //    default:
        //        break;
        //}
        gameObject.SetActive(false);
    }


}
