using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ItemDataBase
{
    [System.NonSerialized]
    public CharacterData owner;

    public WeaponTableData weaponData;

    public WeaponData(ItemTableDataBase itemDataTable)
    {
        dataTable = itemDataTable;
        weaponData = dataTable as WeaponTableData;
    }
}
