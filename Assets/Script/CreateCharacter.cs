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

        //if(cd != null)
        //{
        //    character = Instantiate(characterPrefab, transform);
        //    character.GetComponent<Player>().cd = cd;
        //    character.GetComponent<Player>().index = characterIndex;
        //    character.GetComponent<Player>().cd.GameStart();
        //}
        character = Instantiate(characterPrefab, transform);
        CharacterData test = new CharacterData();
        test.SetCharacter();
        test.EquipItem(new HoseData(MyDataTableMgr.hoseTable.GetTable(1)), ItemType.Hose);
        test.EquipItem(new HoseData(MyDataTableMgr.bunkerGearTable.GetTable(1)), ItemType.BunkerGear);
        test.EquipItem(new HoseData(MyDataTableMgr.oxygenTankTable.GetTable(1)), ItemType.OxygenTank);

        character.GetComponent<Player>().cd = test;
        character.GetComponent<Player>().cd.GameStart();
        character.GetComponent<Player>().index = characterIndex;
    }
    public void ChangeCharacter(Player player)
    {
        if (player != null)
        {
            DeleteCharacter();
        
            character = Instantiate(characterPrefab, transform);
            character.GetComponent<Player>().index = characterIndex;
            character.GetComponent<Player>().cd = player.cd;
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
