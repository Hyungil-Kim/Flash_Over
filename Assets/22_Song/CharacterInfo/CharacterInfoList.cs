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
    List<CharacterData> sortUserCharacter = new List<CharacterData>();

    public InfoType type;
    //List<GameObject> uiCharacterList = new List<GameObject>();
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
        if(isReverse)
        {
            sortUserCharacter.Reverse();
        }
        for (int i = 0; i < sortUserCharacter.Count; i++)
        {
            var index = i;
            var chaButton = chaList[index].GetComponent<ChaButton>();
            chaButton.Init(sortUserCharacter[index],sortIndex,i);
            chaButton.button.onClick.AddListener(() =>OnChaButton(index));
            chaList[index].SetActive(true);

            var uiChaList = UIOnOff.instance.uiCharacterList;
            var model = uiChaList[i].GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
            sortUserCharacter[index].setupModel.ApplyToCharacter(model);
            uiChaList[i].GetComponent<UICharacter>().Init(i);
        }
    }
    //public void UIChaInit()
    //{
    //    var uiChaList = UIOnOff.instance.uiCharacterList;
    //    var model = uiChaList[i].GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
    //    sortUserCharacter[index].setupModel.ApplyToCharacter(model);
    //    uiChaList[i].GetComponent<UICharacter>().Init(i);
    //}
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
        switch (type)
        {
            case InfoType.Character:
                var characterInfo = GetComponentInParent<CharacterInfo>();
                characterInfo.curCharacter = sortUserCharacter[index];
                characterInfo.currentIndex = index;
                characterInfo.OnChaIcon();
                break;
            case InfoType.Training:
                var trainingInfo = GetComponentInParent<TrainingCharacter>();
                trainingInfo.curCharacter = sortUserCharacter[index];
                trainingInfo.curIndex = index;
                trainingInfo.OnChaIcon();
                break;
            case InfoType.Rest:
                var restInfo = GetComponentInParent<Rest>();
                restInfo.curCd = sortUserCharacter[index];
                restInfo.curCharacterIndex = index;
                restInfo.OnChaIcon();
                break;
            case InfoType.Truck:
                var truckInfo = GetComponentInParent<FireTruck>();
                truckInfo.curcharacter = sortUserCharacter[index];
                truckInfo.curCharacterIndex = index;
                truckInfo.OnChaIcon();
                break;
            default:
                break;
        }

        //var parent = GetComponentInParent<CharacterInfo>();
        //parent.curCharacter = sortUserCharacter[index];
        //parent.OnChaIcon();
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

    public void SetCharacter(CharacterData cd, int index = 0)
    {
        switch (index)
        {
            case 1:
                if (cd.consum1 != null)
                {
                    GameData.userData.gold += cd.consum1.itemData.cost;
                    cd.UseConsumItem(1);
                }
                break;
            case 2:
                if (cd.consum2 != null)
                {
                    GameData.userData.gold += cd.consum2.itemData.cost;
                    cd.UseConsumItem(2);
                }
                break;
            default:
                break;
        }
        Init();
    }
}
