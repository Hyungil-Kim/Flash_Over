using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FireTruck : MonoBehaviour
{
    public int curFiremanIndex;
    public GameObject prefab;
    public Transform content;
    public int maxFireMan;

    public NewFireTruckList fireTruckList;
    public FireTruckInventory inventory;
    public FireManCharacter firemanCharacter;
    public ConsumShop consumShop;


    private List<GameObject> fireManList = new List<GameObject>();
    private Dictionary<int, CharacterData> outFireMan = new Dictionary<int, CharacterData>();

    public CharacterData curcharacter;

    public Image icon;
    public CharacterData iconCharacter;

    private void OnEnable()
    {
        outFireMan.Clear();
        for (int i = 0; i < GameData.userData.gofireman; i++)
        {
            var index = i;
            var newGo = Instantiate(prefab, content);
            var firemanprefab = newGo.GetComponent<FireManInfoPrefab>();
            firemanprefab.releasebutton.onClick.AddListener(() => OnRelease(index));
            firemanprefab.index = index;
            //newGo.SetActive(false);
            fireManList.Add(newGo);
        }

        for (int i = 0; i < GameData.userData.gofireman; i++)
        {
            fireManList[i].SetActive(true);
        }
        inventory.gameObject.SetActive(false);
        consumShop.gameObject.SetActive(false);
    }
    public void OnInventory(ItemType type)
    {
        consumShop.gameObject.SetActive(false);
        inventory.gameObject.SetActive(true);
        inventory.Init(type);
    }
    public void OnShop()
    {
        inventory.gameObject.SetActive(false);
        consumShop.gameObject.SetActive(true);
    }
    public void SetCharacter(CharacterData cd,int index = 0)
    {
        fireTruckList.SetCharacter(cd, index);
        curcharacter = cd;
    }
    public void OnBack()
    {
        inventory.gameObject.SetActive(false);
        consumShop.gameObject.SetActive(false);
    }    
    //public void OnChaIcon(CharacterData cd)
    //{
    //    for (int i = 0; i < GameData.userData.maxFireMan; i++)
    //    {
    //        if (fireManList[i].GetComponent<FireMan>().cd == cd)
    //        {
    //            var firemanCheak = fireManList[i].GetComponent<FireMan>();
    //            firemanCheak.cd = null;
    //            firemanCheak.Init();
    //        }
    //    }
    //    var fireman = fireManList[curFiremanIndex].GetComponent<FireMan>();
    //    fireman.cd = cd;
    //    cd.isSelected = true;
    //    fireman.Init();

    //    if (outFireMan.ContainsValue(cd))
    //    {
    //        var keyNumber = outFireMan.FirstOrDefault((x) => x.Value == cd).Key;
    //        outFireMan.Remove(keyNumber);
    //    }   
    //    outFireMan.Add(curFiremanIndex,cd);

    //    characterList.SetActive(false);
    //}
    public void OnChaIcon(CharacterData cd)
    {
        //for (int i = 0; i < GameData.userData.maxFireMan; i++)
        //{
        //    if (fireManList[i].GetComponent<FireMan>().cd == cd)
        //    {
        //        var firemanCheak = fireManList[i].GetComponent<FireMan>();
        //        firemanCheak.cd = null;
        //        firemanCheak.Init();
        //    }
        //}

        //if (outFireMan.ContainsValue(cd))
        //{
        //    var keyNumber = outFireMan.FirstOrDefault((x) => x.Value == cd).Key;
        //    outFireMan.Remove(keyNumber);
        //}
        if (!outFireMan.ContainsValue(cd))
        {
            var fireman = fireManList[outFireMan.Count].GetComponent<FireManInfoPrefab>();
            outFireMan.Add(outFireMan.Count, cd);
            cd.isSelected = true;
            fireman.Init(cd);
            fireTruckList.Init();
        }

        //characterList.SetActive(false);
    }

    public void OnCharacter(int index)
    {
        if (iconCharacter != null)
        {
            if (!outFireMan.ContainsValue(iconCharacter))
            {
                var fireman = fireManList[index].GetComponent<FireManInfoPrefab>();
                outFireMan.Add(index, iconCharacter);
                iconCharacter.isSelected = true;
                fireman.Init(iconCharacter);
                fireTruckList.Init();
            }
        }
    }
    public void OnRelease(int index)
    {
        if(outFireMan.ContainsKey(index))
        {
            outFireMan[index].isSelected = false;
            outFireMan.Remove(index);
            fireTruckList.Init();
        }

        //characterList.SetActive(false);
        //fireManList[curFiremanIndex].GetComponent<FireManInfoPrefab>().cd = null;
        fireManList[index].GetComponent<FireManInfoPrefab>().Init(null);
    }
    public void OnStart()
    {
        GameData.userData.fireManList = outFireMan;
    }
    public void OnExit()
    {
        foreach (var fireman in outFireMan)
        {
            fireman.Value.isSelected = false;
        }
    }
    public void OnExitConsumShop()
    {
        foreach (var fireman in outFireMan)
        {
            if (fireman.Value.consum1 != null)
            {
                GameData.userData.gold += fireman.Value.consum1.count * fireman.Value.consum1.itemData.cost;
                fireman.Value.consum1 = null;
            }
            if (fireman.Value.consum2 != null)
            {
                GameData.userData.gold += fireman.Value.consum2.count * fireman.Value.consum2.itemData.cost;
                fireman.Value.consum2 = null;
            }
        }
    }

    public void OnIcon(Sprite sprite, CharacterData cd)
    {
        iconCharacter = cd;
        icon.sprite = sprite;
        icon.gameObject.SetActive(true);
    }
    public void IconUpdate()
    {
        var pos = UIOnOff.instance.mousepos;

        icon.GetComponent<RectTransform>().position = pos;
    }
    public void OffIcon()
    {
        iconCharacter = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
    //public void SetCurFireMan(int index)
    //{
    //    curFiremanIndex = index;
    //    characterList.SetActive(true);
    //}
    //public void OnReady()
    //{
    //    GameData.userData.fireManList = outFireMan;
    //    fireManInfo.gameObject.SetActive(true);
    //}
}
