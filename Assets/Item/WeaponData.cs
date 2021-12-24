using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponData : ItemDataBase
{
    //현재 주인은 저장하지 않아도 될것같아요 불러올때 설정해줘야할것같아요
    //이것도 저장하면 시리얼라이즈할떄 무한반복 해서 안되더라구요 ?
    //characterdata안에도 weapondata가 있고
    //weapondata안에도 characterdata가 있어서
    //두개가 계속 반복돼서 안되는 것 같습니다 ~
    //그래서 데이터 로드할때 추가해줄겁니다.
    //[System.NonSerialized]
    //public CharacterData owner;

    public WeaponTableData weaponData;

    public WeaponData(ItemTableDataBase itemDataTable,int itemCount = 1)
    {
        dataTable = itemDataTable;
        count = itemCount;
        weaponData = dataTable as WeaponTableData;
    }
}
