using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class CharacterInfoList : MonoBehaviour
{
    public int maxChaCount;
    private List<GameObject> chaList = new List<GameObject>();
    public GameObject chaPrefab;
    public GameObject content;
    public TMP_Dropdown optionDropdown;
    private int sortIndex;
    public CharacterInfoStat info;
    private bool isReverse;
    private List<CharacterData> sortUserCharacter
        = new List<CharacterData>();
    private void Awake()
    {
        for (int i = 0; i < maxChaCount; i++)
        {
            var character = Instantiate(chaPrefab, content.transform);
            character.SetActive(false);
            var chaButton = character.GetComponent<ChaButton>();
            //chaButton.Init(i);
            chaList.Add(character);
        }
        DropDownInit();
    }
    private void OnEnable()
    {
        sortIndex = 0;
        Init();
    }
    public void Init()
    {
        var userCharacterList = GameData.userData.characterList;
        foreach (var cha in chaList)
        {
            cha.SetActive(false);
        }

        var characterSort = (CharacterOrder)sortIndex;
        sortUserCharacter.Clear();
        switch (characterSort)
        {
            case CharacterOrder.Default:
                sortUserCharacter = userCharacterList.Select((x) =>x).ToList();
                break;
            case CharacterOrder.Str:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.baseStats.str).ToList();
                break;
            case CharacterOrder.Name:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.characterName).ToList();
                break;
            case CharacterOrder.Hp:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.baseStats.hp).ToList();
                break;
            default:
                break;
        }
        if(isReverse)
        {
            sortUserCharacter.Reverse();
        }
        for (int i = 0; i < sortUserCharacter.Count; i++)
        {
            var index = i;
            var chaButton = chaList[index].GetComponent<ChaButton>();
            chaButton.Init(sortUserCharacter[index]);
            chaButton.button.onClick.AddListener(() =>OnChaButton(index));
            chaList[index].SetActive(true);
        }
    }
    public void DropDownInit()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        var enumNames = System.Enum.GetNames(typeof(CharacterOrder));
        foreach (var enumName in enumNames)
        {
            options.Add(new TMP_Dropdown.OptionData(enumName));
        }
        optionDropdown.options = options;
    }
    public void OnChaButton(int index)
    {
        var parent = GetComponentInParent<CharacterInfo>();
        parent.curCharacter = sortUserCharacter[index];
        parent.OnChaIcon();
    }
    public void OptionSort(int index)
    {
        sortIndex = index;
        Init();
    }
    public void SortReverse(bool istrue)
    {
        isReverse = istrue;
        Init();
    }
}
