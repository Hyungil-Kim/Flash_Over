using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TrainingList : MonoBehaviour
{
    public GameObject chaPrefab;
    public GameObject content;
    public TMP_Dropdown optionDropdown;

    private int sortIndex;
    private int maxChaCount;
    private List<GameObject> chaList = new List<GameObject>();
    private bool isReverse;
    private List<CharacterData> sortUserCharacter;

    private void Awake()
    {
        maxChaCount = MyDataTableMgr.menuTable.GetTable(GameData.userData.maxCharacter).MC1Count;
        for (int i = 0; i < maxChaCount; i++)
        {
            var character = Instantiate(chaPrefab, content.transform);
            character.SetActive(false);
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
        var characterSort = (CharacterOrder)sortIndex;
        switch (characterSort)
        {
            case CharacterOrder.Default:
                sortUserCharacter = userCharacterList.Select((x) => x).ToList();
                break;
            case CharacterOrder.Str:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.baseStats.str.stat).ToList();
                break;
            case CharacterOrder.Hp:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.baseStats.hp.stat).ToList();
                break;
            case CharacterOrder.Lung:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.baseStats.lung.stat).ToList();
                break;
            case CharacterOrder.Name:
                sortUserCharacter = userCharacterList.OrderBy((x) => x.characterName).ToList();
                break;

            default:
                break;
        }
        if (isReverse)
        {
            sortUserCharacter.Reverse();
        }
        for (int i = 0; i < sortUserCharacter.Count; i++)
        {
            var index = i;
            var chaButton = chaList[index].GetComponent<ChaButton>();
            chaButton.Init(sortUserCharacter[index],sortIndex, index);
            chaButton.button.onClick.AddListener(() => OnChaButton(index));
            chaList[index].SetActive(true);

            //var uiChaList = UIOnOff.instance.uiCharacterList;
            //var model = uiChaList[i].GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
            //sortUserCharacter[index].setupModel.ApplyToCharacter(model);
            //uiChaList[i].GetComponent<UICharacter>().Init(i);
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
        var parent = GetComponentInParent<TrainingCharacter>();
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
