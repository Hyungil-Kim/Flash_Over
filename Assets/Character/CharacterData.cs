using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterStatType
{
    HP,
    STR,
    DEF,
    MOVE,
    Max
}
public class CharacterStat
{
    public int hp;
    public int str;
    public int def;
    public int move;

    public CharacterStat DeepCopy()
    {
        var copy = new CharacterStat();
        copy.hp = hp;
        copy.str = str;
        copy.def = def;
        copy.move = move;
        return copy;
    }
}

public class CharacterData
{
    public int level;
    public int exp;

    public CharacterStat baseStats = new CharacterStat();

    [System.NonSerialized]
    public CharacterStat totalStats = new CharacterStat();

    public int statPoints;

    public int stress;

    public Personality personality;

    public WeaponData weapon;

    public void StatInit()
    {
        totalStats = baseStats.DeepCopy();
        if (weapon != null)
        {
            weapon.owner = this;
            AddStat(CharacterStatType.STR, weapon.weaponData.str, totalStats);
        }
    }

    public void SetCharacter()
    {
        //변수선언
        //list : 랜덤하게 뽑아올려고 만듬..맨 위 스탯부터 랜덤배정되면 아래 스탯은 비교적 적게 낮을 확률이 늘어날것같아서
        //deficientStatList : 부족한 스탯 확인하려고 만듬
        //total : 들어갈 능력치의 총합
        var list = new List<int>();
        var deficientStatList = new List<int>();
        var total = Random.Range(MyDataTableMgr.chaStatTable.GetTable("TOTAL").min,
            MyDataTableMgr.chaStatTable.GetTable("TOTAL").max);
        //최솟값 토탈에서 빼주기
        for (int i = 0; i < (int)CharacterStatType.Max; i++)
        {
            total -= MyDataTableMgr.chaStatTable.GetTable(i).min;
            AddStat((CharacterStatType)i, MyDataTableMgr.chaStatTable.GetTable(i).min, baseStats);
            list.Add(i);
            deficientStatList.Add(i);
        }

        //랜덤범위로 능력치들 설정
        while (list.Count > 0)
        {
            var randomIndex = Random.Range(0, list.Count);
            var index = list[randomIndex];
            var type = (CharacterStatType)index;
            var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
            if (total >= statTable.min)
            {
                var statValue = Random.Range(statTable.min, statTable.max);
                statValue = Mathf.Clamp(statValue, statTable.min, total);
                AddStat(type, statValue - statTable.min, baseStats);
                total = total - statValue + statTable.min;
                if(statValue == statTable.max)
                {
                    deficientStatList.RemoveAt(randomIndex);
                }
            }
            list.RemoveAt(randomIndex);
        }

        //능력치가 남는다면 부족한 곳에 뿌리기
        while(total>0)
        {
            var randomIndex = Random.Range(0, deficientStatList.Count);
            var index = deficientStatList[randomIndex];
            var type = (CharacterStatType)index;
            var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());

            AddStat(type, 1, baseStats);
            if(GetStat(type,true) == statTable.max)
            {
                deficientStatList.RemoveAt(randomIndex);
            }
            total--;
        }

        personality = new Personality();
        personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("PERSONALITY").min,
            MyDataTableMgr.chaStatTable.GetTable("PERSONALITY").max);


        StatInit();
    }
    public void AddStat(CharacterStatType statType, float statValue, CharacterStat stats)
    {
        switch (statType)
        {
            case CharacterStatType.HP:
                stats.hp += (int)statValue;
                break;
            case CharacterStatType.STR:
                stats.str += (int)statValue;
                break;
            case CharacterStatType.DEF:
                stats.def += (int)statValue;
                break;
            case CharacterStatType.MOVE:
                stats.move += (int)statValue;
                break;
            default:
                break;
        }
    }
    public float GetStat(CharacterStatType statType, bool isBase = false)
    {
        switch (statType)
        {
            case CharacterStatType.HP:
                var hp = isBase ? baseStats.hp : totalStats.hp;
                return hp;
            case CharacterStatType.STR:
                var str = isBase ? baseStats.str : totalStats.str;
                return str;
            case CharacterStatType.DEF:
                var def = isBase ? baseStats.def : totalStats.def;
                return def;
            case CharacterStatType.MOVE:
                var move = isBase ? baseStats.move : totalStats.move;
                return move;
        }
        return 0;
    }
    public void EquipWeapon(WeaponData weapon)
    {
        if (weapon != null)
        {
            if (weapon.owner != null)
            {
                weapon.owner.DisarmWeapon();
            }
            weapon.owner = this;
            this.weapon = weapon;
        }
        StatInit();
    }
    public void DisarmWeapon()
    {
        weapon = null;
        StatInit();
    }
}