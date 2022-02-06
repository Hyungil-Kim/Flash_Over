using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    public GameObject characterPrefab;
    public int characterIndex;
    private GameObject character;
    public GameObject Character
    {
        get
        {
            return character;
        }
    }
    private void Start()
    {

    }
    public void Create(CharacterData cd)
    {
        if (cd != null)
        {
            character = Instantiate(characterPrefab, transform);
            character.GetComponent<Player>().cd = cd;
            character.GetComponent<Player>().index = characterIndex;
            character.GetComponent<Player>().cd.StartStage();
            var model = character.GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
            cd.setupModel.ApplyToCharacter(model);

        }
        else
        {
            //test
            character = Instantiate(characterPrefab, transform);
            CharacterData test = new CharacterData();
            //test.NewSetCharacter();
            test.SettingFixCharacter(20, 10, 50, 8, 8);
            test.EquipItem(new HoseData(MyDataTableMgr.hoseTable.GetTable(1)), ItemType.Hose);
            test.EquipItem(new BunkerGearData(MyDataTableMgr.bunkerGearTable.GetTable(1)), ItemType.BunkerGear);
            test.EquipItem(new OxygenTankData(MyDataTableMgr.oxygenTankTable.GetTable(1)), ItemType.OxygenTank);

            character.GetComponent<Player>().cd = test;
            character.GetComponent<Player>().cd.StartStage();
            character.GetComponent<Player>().index = characterIndex;

            var model = character.GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
            test.setupModel.ApplyToCharacter(model);
        }
    }
    public void ChangeCharacter(CharacterData cd)
    {
        
        if (cd != null)
        {
            DeleteCharacter();
        
            character = Instantiate(characterPrefab, transform);
            character.GetComponent<Player>().index = characterIndex;
            character.GetComponent<Player>().cd = cd;
        }
    }
    public void DeleteCharacter()
    {
        if( character != null)
        {
            Destroy(character);
        }
    }
}
