using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestPrefab : MonoBehaviour
{
    public RawImage icon;
    public TextMeshProUGUI text;

    public Texture baseSprite;

    public GameObject restbutton;
    public GameObject canclebutton;
    public GameObject backbutton;
    public GameObject upgradebutton;

    private CharacterData restcd;
    private int restIndex;

    public TextMeshProUGUI tired;
    public TextMeshProUGUI physical;
    public TextMeshProUGUI psycholosical;

    private void Start()
    {
        if (icon != null)
        {
            //baseSprite = icon.sprite;
        }
    }


    public void init(CharacterData cd, bool isActive, int index)
    {
        physical.text = $"�ܻ�ġ�� {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Physical}���";
        psycholosical.text = $"�ɸ�ġ�� {MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Psychological}���";

        var button = icon.GetComponent<Button>();
        if (!isActive)
        {
            button.interactable = false;
            icon.texture = baseSprite;
            text.text = "";
            upgradebutton.SetActive(true);
        }
        else
        {
            upgradebutton.SetActive(false);
            button.onClick.AddListener(() => OnClick(index));
            if (cd != null)
            {
                var iconIndex = GetComponentInParent<Rest>().curCharacterIndex;
                icon.texture = Resources.Load<RenderTexture>($"Icon/icon {iconIndex}");
                restcd = cd;
                restIndex = index;
                switch (cd.admission)
                {
                    case Admission.Rest:
                        text.text = $"{cd.characterName}��(��)\n{cd.restCount}���� �޽����Դϴ�";
                        CharacterRest();
                        break;
                    case Admission.Hospital:
                        text.text = $"{cd.characterName}��(��)\n{cd.restCount}���� ���� ġ�����Դϴ�";
                        CharacterRest();
                        break;
                    case Admission.Phycho:
                        text.text = $"{cd.characterName}��(��)\n{cd.restCount}���� ��� ġ�����Դϴ�";
                        CharacterRest();
                        break;
                    case Admission.None:
                        text.text = $"{cd.characterName}";
                        CanRest();
                        break;
                    default:
                        break;
                }
                button.interactable = false;
                
                //icon.sprite
                //text.text = $"{cd.characterName}��(��)\n{cd.restCount}�� �޽����Դϴ�";
            }
            else
            {
                button.interactable = true;
                icon.texture = baseSprite;
                text.text = $"�޽��� �����\n������ �ּ���.";
                restbutton.SetActive(false);
                backbutton.SetActive(false);
            }
        }
    }
    public void Release()
    {
        GameData.userData.restList[restIndex].isFireAble = true;
        GameData.userData.restList.Remove(restIndex);
        
        canclebutton.SetActive(false);
        init(null, true, restIndex);

    }
    public void UpgradePopUp()
    {
        GetComponentInParent<Rest>().OnUpgradePopup();
    }
    public void CanRest()
    {
        restbutton.SetActive(true);
        canclebutton.SetActive(true);
        backbutton.SetActive(false);
    }
    public void CharacterRest()
    {
        restbutton.SetActive(false);
        canclebutton.SetActive(false);
        backbutton.SetActive(true);

    }
    public void OnClick(int index)
    {
        var rest = GetComponentInParent<Rest>();
        rest.CurIndex = index;
        rest.OnClickRestRoom();
    }

    public void GoRest()
    {
        restcd.admission = Admission.Rest;
        init(restcd, true, restIndex);
    }
    public void OnRest(int index)
    {
        var rest = GetComponentInParent<Rest>();
        var type = (RestType)index;
        switch (type)
        {
            case RestType.Physical:
                if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.physical).RS3Physical)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                    return;
                }
                break;
            case RestType.Psycholosical:
                if (GameData.userData.gold < MyDataTableMgr.menuTable.GetTable(GameData.userData.restShopData.psychological).RS2Psychological)
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                    return;
                }
                break;
            default:
                break;
        }
        rest.restType = type;
        rest.OnPopUp();
    }

    public void GoHospital()
    {
        restcd.admission = Admission.Hospital;
        init(restcd, true, restIndex);
    }
    public void GoWhiteHouse()
    {
        restcd.admission = Admission.Phycho;
        init(restcd, true, restIndex);
    }
    public void CancleRest()
    {
        restcd.admission = Admission.None;
        GameData.userData.restList.Remove(restIndex);
        init(null, true, restIndex);
    }
}
