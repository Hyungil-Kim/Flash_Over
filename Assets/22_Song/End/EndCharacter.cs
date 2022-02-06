using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedPeopleSystem;
public class EndCharacter : MonoBehaviour
{
    private string prefabName;
    public GameObject go;
    public int index;

    private CharacterData cd;
    private void Start()
    {
        //test
        cd = new CharacterData();
        cd.NewSetCharacter();
        Init(cd, index);
    }
    public void Init(CharacterData cd, int index)
    {
        prefabName = cd.prefabName;
        //var go = Resources.Load<GameObject>($"Prefabs/Character/{prefabName}");
        var newGo = Instantiate(go, transform);
        var custom = newGo.GetComponent<CharacterCustomization>();
        newGo.GetComponent<UICharacter>().Init(index);

        cd.setupModel.ApplyToCharacter(custom);

    }
}
