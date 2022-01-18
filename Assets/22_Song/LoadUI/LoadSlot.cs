using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    //public TextMeshProUGUI slot;
    public int slot;
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI userName;
    public TextMeshProUGUI saveTime;
    private void Start()
    {
        //test?
        Init();
    }
    public void Init()
    {
        var ps = MySaveLoadSystem<PlaySave>.Load(SaveDataType.Play, slot);
        if(ps != null)
        {
            stageName.text = $"{ps.inGameTime}/{ps.stageName}";
            userName.text = $"{ps.userName}";
            saveTime.text = $"{ps.dateTime}";
        }
    }
}
