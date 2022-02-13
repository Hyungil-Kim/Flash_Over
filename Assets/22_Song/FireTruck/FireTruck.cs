using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FireTruck : MonoBehaviour
{
    public GameObject notReady;
    public int curFiremanIndex;
    public GameObject prefab;
    public Transform content;
    public int maxFireMan;

    public NewFireTruckList fireTruckList;
    //public FireTruckInventory inventory;
    public FireManCharacter firemanCharacter;
    public ConsumShop consumShop;


    public List<GameObject> fireManList = new List<GameObject>();
    public Dictionary<int, CharacterData> outFireMan = new Dictionary<int, CharacterData>();

    public CharacterData curcharacter;
    public int curCharacterIndex;
    public int curIndex;

    public Image icon;
    public CharacterData iconCharacter;

    public GameObject daewonList;
    public CharacterInfoList characterInfoList;
    public CharacterInfoStat characterInfoStat;
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
        //inventory.gameObject.SetActive(false);
        consumShop.gameObject.SetActive(false);
    }
    public void OnInventory(ItemType type)
    {
        consumShop.gameObject.SetActive(false);
        //inventory.gameObject.SetActive(true);
        //inventory.Init(type);
    }
    public void OnShop()
    {
        //inventory.gameObject.SetActive(false);
        consumShop.gameObject.SetActive(true);
    }
    public void SetCharacter(CharacterData cd,int index = 0)
    {
        //fireTruckList.SetCharacter(cd, index);
        characterInfoList.SetCharacter(cd, index);
        
        curcharacter = cd;

        var fireman = fireManList[curIndex].GetComponent<FireManInfoPrefab>();
        fireman.Init(curcharacter);
    }
    public void OnBack()
    {
        //inventory.gameObject.SetActive(false);
        consumShop.gameObject.SetActive(false);
    }

    public void OnChaIcon()
    {
        characterInfoStat.Init();
        characterInfoStat.gameObject.SetActive(true);
    }
    public void OnInfoList()
    {
        firemanCharacter.gameObject.SetActive(false);
        //characterInfoList.gameObject.SetActive(true);
        daewonList.SetActive(true);
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
    public void SetCharacter()
    {
        if (curcharacter.Ready)
        {
            if (outFireMan.ContainsKey(curIndex))
            {
                outFireMan.Remove(curIndex);
            }
            if (outFireMan.ContainsValue(curcharacter))
            {
                var keyNumber = outFireMan.FirstOrDefault((x) => x.Value == curcharacter).Key;
                outFireMan.Remove(keyNumber);
            }
            outFireMan.Add(curIndex, curcharacter);

            var fireman = fireManList[curIndex].GetComponent<FireManInfoPrefab>();
            curcharacter.isSelected = true;
            fireman.Init(curcharacter);
            //fireTruckList.Init();


            firemanCharacter.gameObject.SetActive(true);
            //characterInfoList.gameObject.SetActive(false);
            daewonList.SetActive(false);
            characterInfoStat.gameObject.SetActive(false);
        }
        else
        {
            notReady.SetActive(true);
        }
    }
    public void OnExitNotReady()
    {
        notReady.SetActive(false);
    }
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
            if (outFireMan[index].consum1 != null)
            {
                GameData.userData.gold += outFireMan[index].consum1.count * outFireMan[index].consum1.itemData.cost;
                outFireMan[index].consum1 = null;
            }
            if (outFireMan[index].consum2 != null)
            {
                GameData.userData.gold += outFireMan[index].consum2.count * outFireMan[index].consum2.itemData.cost;
                outFireMan[index].consum2 = null;
            }
            outFireMan.Remove(index);
            //fireTruckList.Init();
        }

        //characterList.SetActive(false);
        //fireManList[curFiremanIndex].GetComponent<FireManInfoPrefab>().cd = null;
        consumShop.gameObject.SetActive(false);
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
        OnExitConsumShop();
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
