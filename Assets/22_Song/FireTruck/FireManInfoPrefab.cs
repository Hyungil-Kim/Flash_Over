using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using TMPro;
using System.Text;
using AdvancedPeopleSystem;

public class FireManInfoPrefab : MonoBehaviour, IDropHandler
{
    public RawImage icon;
    public FireManItem hose;
    public FireManItem bunkerGear;
    public FireManItem oxygenTank;
    public TextMeshProUGUI info;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI weight;

    public FireManItem consum1;
    public FireManItem consum2;

    public Button releasebutton;

    public int index;
    private FireTruck fireTruck;

    public GameObject selectButton;
    public GameObject infoUI;
    private void Start()
    {
        fireTruck = GetComponentInParent<FireTruck>();
    }
    //public TextMeshProUGUI personality;
    public void Init(CharacterData cd)
    {
        if (cd != null)
        {
            var model = cd.setupModel;
            var custom = UIOnOff.instance.uiCharacterList[index].GetComponent<CharacterCustomization>();
            var uicha = UIOnOff.instance.uiCharacterList[index].GetComponent<UICharacter>();

            var customInfo = GameData.userData.characterList[index].setupModel;
            customInfo.ApplyToCharacter(custom);
            uicha.Init(index);
            icon.texture = Resources.Load<RenderTexture>($"Icon/icon {index}");

            infoUI.SetActive(true);
            selectButton.SetActive(false);

            hose.Init(cd.hose, ItemType.Hose);
            bunkerGear.Init(cd.bunkerGear, ItemType.BunkerGear);
            oxygenTank.Init(cd.oxygenTank, ItemType.OxygenTank);
            consum1.Init(cd.consum1, ItemType.Consumable);
            consum2.Init(cd.consum2, ItemType.Consumable);


            info.text = $"{cd.characterClass}\n{cd.characterGrade}  {cd.characterName}";


            var statSB = new StringBuilder();
            statSB.Append(string.Format($"체력 : {cd.totalStats.hp.stat}\n"));
            statSB.Append(string.Format($"폐활량 : {cd.totalStats.lung.stat}\n"));
            statSB.Append(string.Format($"힘 : {cd.totalStats.str.stat}\n"));
            statSB.Append(string.Format($"이동력 : {cd.totalStats.move}\n"));
            statSB.Append(string.Format($"시야 : {cd.totalStats.vision}\n"));
            statSB.Append(string.Format($"데미지 : {cd.totalStats.dmg}\n"));
            statSB.Append(string.Format($"방어력 : {cd.totalStats.def}\n"));
            statSB.Append(string.Format($"산소통 : {cd.totalStats.sta}"));
            stat.text = statSB.ToString();
            var firemanInfo = GetComponentInParent<FireTruck>();
            weight.text = $"남은 무게 : {cd.weight}";

            hose.GetComponent<Button>().onClick.AddListener(() => firemanInfo.SetCharacter(cd));
            bunkerGear.GetComponent<Button>().onClick.AddListener(() => firemanInfo.SetCharacter(cd));
            oxygenTank.GetComponent<Button>().onClick.AddListener(() => firemanInfo.SetCharacter(cd));
            consum1.GetComponent<Button>().onClick.AddListener(() => firemanInfo.SetCharacter(cd, 1));
            consum2.GetComponent<Button>().onClick.AddListener(() => firemanInfo.SetCharacter(cd, 2));


        }
        else
        {
            infoUI.SetActive(false);
            selectButton.SetActive(true);

            hose.Init(null, ItemType.Hose);
            bunkerGear.Init(null, ItemType.BunkerGear);
            oxygenTank.Init(null, ItemType.OxygenTank);
            consum1.Init(null, ItemType.Consumable);
            consum2.Init(null, ItemType.Consumable);


            info.text = $"";

            stat.text = "";
            weight.text = $"";
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        fireTruck.OnCharacter(index);
    }

    public void OnClickIcon()
    {
        GetComponentInParent<FireTruck>().OnInfoList();
        GetComponentInParent<FireTruck>().curIndex = index;
    }
}
