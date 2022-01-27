using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedPeopleSystem;
public class EndCharacter : MonoBehaviour
{
    private string prefabName;
    private void Start()
    {
        Init(new CharacterData());
    }
    public void Init(CharacterData cd)
    {
        prefabName = cd.prefabName;
        var go = Resources.Load<GameObject>($"Prefabs/Character/{prefabName}");
        var newGo = Instantiate(go, transform);
        var custom = newGo.GetComponent<CharacterCustomization>();
        var testSettings = custom.GetSetup();
        //var settings = new CharacterCustomizationSetup();
        //settings.selectedElements.Accessory = 2;
        //settings.selectedElements.Beard = 2;
        //settings.selectedElements.Hair = 3;
        //settings.HairColor = new float[4] { 1f,1f,1f,1f};

        testSettings.HairColor = new float[4] { 1f, 1f, 1f, 1f };
        testSettings.selectedElements.Hair = Random.Range(0,15);
        testSettings.ApplyToCharacter(custom);

        //custom.SetCharacterSetup(settings);


    }
}
