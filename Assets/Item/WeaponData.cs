using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponData : ItemDataBase
{
    //���� ������ �������� �ʾƵ� �ɰͰ��ƿ� �ҷ��ö� ����������ҰͰ��ƿ�
    //�̰͵� �����ϸ� �ø���������ҋ� ���ѹݺ� �ؼ� �ȵǴ��󱸿� ?
    //characterdata�ȿ��� weapondata�� �ְ�
    //weapondata�ȿ��� characterdata�� �־
    //�ΰ��� ��� �ݺ��ż� �ȵǴ� �� �����ϴ� ~
    //�׷��� ������ �ε��Ҷ� �߰����̴ٰϴ�.
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
