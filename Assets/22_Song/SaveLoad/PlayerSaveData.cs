using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public int index;

    //public Transform tf;
    //public Vector3 pos;
    public float posx;
    public float posy;
    public float posz;
    public string currentState;
    public CharacterData cd;
    //public List<GameObject> handList;
    public int eventNum = 0;

    public List<int> handListIndex = new List<int>();


}
