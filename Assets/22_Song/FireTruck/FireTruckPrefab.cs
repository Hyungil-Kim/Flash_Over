using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class FireTruckPrefab : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI state;
    public Button button;

    private FireTruck fireTruck;
    private CharacterData curCd;
    private void Start()
    {
        fireTruck = GetComponentInParent<FireTruck>();
    }
    public void Init(CharacterData cd, int sortindex)
    {
        curCd = cd;
        var orderType = (CharacterOrder)sortindex;
        switch (orderType)
        {
            case CharacterOrder.Default:
                chaName.text = cd.characterName;
                break;
            case CharacterOrder.Str:
                chaName.text = cd.totalStats.str.stat.ToString();
                break;
            case CharacterOrder.Hp:
                chaName.text = cd.totalStats.hp.stat.ToString();
                break;
            case CharacterOrder.Lung:
                chaName.text = cd.totalStats.lung.stat.ToString();
                break;
            case CharacterOrder.Name:
                chaName.text = cd.characterName;
                break;
            default:
                break;
        }
        //icon.sprite = cd.
        state.text = cd.state;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        fireTruck.OnIcon(icon.sprite, curCd);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        fireTruck.IconUpdate();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        fireTruck.OffIcon();

    }


}
