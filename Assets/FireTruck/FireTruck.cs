using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FireTruck : MonoBehaviour
{
    public int curFiremanIndex;
    public GameObject characterList;
    public GameObject prefab;
    public FireManInfo fireManInfo;
    public Transform content;
    public int maxFireMan;

    private List<GameObject> fireManList = new List<GameObject>();
    private Dictionary<int, CharacterData> outFireMan = new Dictionary<int, CharacterData>();
    private void OnEnable()
    {
        outFireMan.Clear();
        for (int i = 0; i < maxFireMan; i++)
        {
            var index = i;
            var newGo = Instantiate(prefab, content);
            var button = newGo.GetComponent<Button>();
            button.onClick.AddListener(() => SetCurFireMan(index));
            newGo.SetActive(false);
            fireManList.Add(newGo);
        }

        for (int i = 0; i < GameData.userData.maxFireMan; i++)
        {
            fireManList[i].SetActive(true);
        }

    }

    public void OnChaIcon(CharacterData cd)
    {
        for (int i = 0; i < GameData.userData.maxFireMan; i++)
        {
            if (fireManList[i].GetComponent<FireMan>().cd == cd)
            {
                var firemanCheak = fireManList[i].GetComponent<FireMan>();
                firemanCheak.cd = null;
                firemanCheak.Init();
            }
        }
        var fireman = fireManList[curFiremanIndex].GetComponent<FireMan>();
        fireman.cd = cd;
        cd.isSelected = true;
        fireman.Init();

        if (outFireMan.ContainsValue(cd))
        {
            var keyNumber = outFireMan.FirstOrDefault((x) => x.Value == cd).Key;
            outFireMan.Remove(keyNumber);
        }   
        outFireMan.Add(curFiremanIndex,cd);

        characterList.SetActive(false);
    }
    public void OnRelease()
    {
        if(outFireMan.ContainsKey(curFiremanIndex))
        {
            outFireMan[curFiremanIndex].isSelected = false;
            outFireMan.Remove(curFiremanIndex);
        }
        characterList.SetActive(false);
        fireManList[curFiremanIndex].GetComponent<FireMan>().cd = null;
        fireManList[curFiremanIndex].GetComponent<FireMan>().Init();
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
    public void SetCurFireMan(int index)
    {
        curFiremanIndex = index;
        characterList.SetActive(true);
    }
    public void OnReady()
    {
        GameData.userData.fireManList = outFireMan;
        fireManInfo.gameObject.SetActive(true);
    }
}
