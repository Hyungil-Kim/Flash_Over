using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatType
{
    Hp,
    Str,
    Lung,
    Move,
    Vision,
    Dmg,
    Def,
    Sta
}
[System.Serializable]
public class CharacterStat
{
    public int hp;
    public int str;
    public int lung;
    public int move;
    public int vision;
    public int dmg;
    public int def;
    public int sta;

    public CharacterStat DeepCopy()
    {
        var copy = new CharacterStat();
        copy.hp = hp;
        copy.str = str;
        copy.lung = lung;
        copy.move = move;
        copy.vision = vision;
        copy.dmg = dmg;
        copy.def = def;
        copy.sta = sta;
        return copy;
    }
}

public class CharacterData
{
    public bool isHire;

    public int level;

    private int maxLevel = 99;

    public double exp;

    public CharacterStat baseStats = new CharacterStat();

    public CharacterStat totalStats = new CharacterStat();

    public int stress;

    public Personality personality;

    //public WeaponData weapon;
    public HoseData hose;
    public BunkerGearData bunkerGear;
    public OxygenTankData oxygenTank;

    public string characterName;
    public string characterClass;
    public string characterGrade;
    public void ApplyItemStat(ItemDataBase item)
    {
        item.owner = this;
        foreach (var typeValue in System.Enum.GetValues(typeof(CharacterStatType)))
        {
            var statType = (CharacterStatType)typeValue;
            switch (statType)
            {
                case CharacterStatType.Hp:
                    AddStat(statType, item.dataTable.hp, totalStats);
                    break;
                case CharacterStatType.Str:
                    AddStat(statType, item.dataTable.str, totalStats);
                    break;
                case CharacterStatType.Lung:
                    AddStat(statType, item.dataTable.lung, totalStats);
                    break;
                case CharacterStatType.Move:
                    AddStat(statType, item.dataTable.move, totalStats);
                    break;
                case CharacterStatType.Vision:
                    AddStat(statType, item.dataTable.vision, totalStats);
                    break;
                case CharacterStatType.Dmg:
                    AddStat(statType, item.dataTable.dmg, totalStats);
                    break;
                case CharacterStatType.Def:
                    AddStat(statType, item.dataTable.def, totalStats);
                    break;
                case CharacterStatType.Sta:
                    AddStat(statType, item.dataTable.sta, totalStats);
                    break;
                default:
                    break;
            }
        }
    }
    public void StatInit()
    {
        totalStats = baseStats.DeepCopy();
        if (hose != null)
        {
            ApplyItemStat(hose);
        }
        if (bunkerGear != null)
        {
            ApplyItemStat(bunkerGear);
        }
        if (oxygenTank != null)
        {
            ApplyItemStat(oxygenTank);
        }
    }
    public void LevelUp()
    {
        //exp ����
        exp = exp - MyDataTableMgr.levelUpTable.GetTable(level - 1).exp;
        
        // level �� �ִ뷹���̸� ������ x
        if(level == maxLevel)
        {
            return;
        }

        //level up ���� ���� ����
        level++;
        level = Mathf.Clamp(level, 0, maxLevel);
        var levelUpStat = MyDataTableMgr.chaStatTable.GetTable("LEVELUPSTAT");
        var statPoints = Random.Range(levelUpStat.min, levelUpStat.max);
        var levelUpStatList = new List<int>();
        var deficientStatList = new List<int>();

        //���ݺ��� max �������� �ֱ� ������ �����ֱ� ���� ����Ʈ ���� ...
        for (int i = 0; i < (int)CharacterStatType.Move; i++)
        {
            levelUpStatList.Add(0);
            deficientStatList.Add(i);
        }

        // statPoint�� ������ �������� ���� ����
        // ������ max ����ŭ �ö����� �� ������ ����Ʈ���� �������� 
        while (statPoints > 0)
        {
            var randomIndex = Random.Range(0, deficientStatList.Count);
            var index = deficientStatList[randomIndex];
            var type = (CharacterStatType)index;
            var statTable = MyDataTableMgr.chaStatTable.GetTable($"LEVELUP{type}");
            
            AddStat(type, 1, baseStats);
            levelUpStatList[randomIndex] += 1;
            
            if (levelUpStatList[randomIndex] == statTable.max)
            {
                deficientStatList.RemoveAt(randomIndex);
            }
            statPoints--;
        }
        //���� �ֽ�ȭ
        StatInit();
    }

    //����ġ�� ������� ȣ��ž��ҰͰ���
    public void CheakExp()
    {
        //exp�� ���� �ʿ��� ����ġ���� ũ�ٸ� �� ������
        while(exp >= MyDataTableMgr.levelUpTable.GetTable(level-1).exp)
        {
            LevelUp();
        }
    }

    //ó�� ���� init�̶�� �����ҵ� ?
    public void SetCharacter()
    {
        //�ϴ� �̸�, ���, ������ ���̺� ���� �������͵� ��� �ӽ÷� �־���
        characterName = "�ƹ���";
        characterGrade = "�����";
        characterClass = "�ҹ��";



        //��������
        //list : �����ϰ� �̾ƿ÷��� ����..�� �� ���Ⱥ��� ���������Ǹ� �Ʒ� ������ ���� ���� ���� Ȯ���� �þ�Ͱ��Ƽ�
        //deficientStatList : ������ ���� Ȯ���Ϸ��� ����
        //total : �� �ɷ�ġ�� ����
        level = 1;

        var list = new List<int>();
        var deficientStatList = new List<int>();
        var total = Random.Range(MyDataTableMgr.chaStatTable.GetTable("Total").min,
            MyDataTableMgr.chaStatTable.GetTable("Total").max);
        //�ּڰ� ��Ż���� ���ֱ�
        for (int i = 0; i < (int)CharacterStatType.Move; i++)
        {
            total -= MyDataTableMgr.chaStatTable.GetTable(i).min;
            AddStat((CharacterStatType)i, MyDataTableMgr.chaStatTable.GetTable(i).min, baseStats);
            list.Add(i);
            deficientStatList.Add(i);
        }
        //��Ż�� ���� �ʴ� �ɷ�ġ ����
        for (int i = (int)CharacterStatType.Move; i < (int)CharacterStatType.Dmg; i++)
        {
            var type = (CharacterStatType)i;
            var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
            var statValue = Random.Range(statTable.min, statTable.max);
            AddStat((CharacterStatType)i, statValue, baseStats);
        }
        //���������� �ɷ�ġ�� ����
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

        //�ɷ�ġ�� ���´ٸ� ������ ���� �Ѹ���
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

        //���� �ο�
        personality = new Personality();
        personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("Personality").min,
            MyDataTableMgr.chaStatTable.GetTable("Personality").max);

        //���� �ֽ�ȭ
        StatInit();
    }
    //���� �߰�
    public void AddStat(CharacterStatType statType, float statValue, CharacterStat stats)
    {
        switch (statType)
        {
            case CharacterStatType.Hp:
                stats.hp += (int)statValue;
                break;
            case CharacterStatType.Str:
                stats.str += (int)statValue;
                break;
            case CharacterStatType.Lung:
                stats.lung += (int)statValue;
                break;
            case CharacterStatType.Move:
                stats.move += (int)statValue;
                break;
            case CharacterStatType.Vision:
                stats.vision += (int)statValue;
                break;
            case CharacterStatType.Dmg:
                stats.dmg += (int)statValue;
                break;
            case CharacterStatType.Def:
                stats.def += (int)statValue;
                break;
            case CharacterStatType.Sta:
                stats.sta += (int)statValue;
                break;
            default:
                break;
        }
    }

    //���� �ҷ�����
    public float GetStat(CharacterStatType statType, bool isBase = false)
    {
        switch (statType)
        {
            case CharacterStatType.Hp:
                var hp = isBase ? baseStats.hp : totalStats.hp;
                return hp;
            case CharacterStatType.Str:
                var str = isBase ? baseStats.str : totalStats.str;
                return str;
            case CharacterStatType.Lung:
                var lung = isBase ? baseStats.lung : totalStats.lung;
                return lung;
            case CharacterStatType.Move:
                var move = isBase ? baseStats.move : totalStats.move;
                return move;
            case CharacterStatType.Vision:
                var vision = isBase ? baseStats.vision : totalStats.vision;
                return vision;
            case CharacterStatType.Dmg:
                var dmg = isBase ? baseStats.dmg : totalStats.dmg;
                return dmg;
            case CharacterStatType.Def:
                var def = isBase ? baseStats.def : totalStats.def;
                return def;
            case CharacterStatType.Sta:
                var sta = isBase ? baseStats.sta : totalStats.sta;
                return sta;
        }
        return 0;
    }

    //��� ����
    public void EquipItem(ItemDataBase itemData, ItemType itemType)
    {
        
        if (itemData != null)
        {
            if (itemData.owner != null)
            {
                itemData.owner.DisarmItem(itemType);
            }
            itemData.owner = this;
            DisarmItem(itemType);
            switch (itemType)
            {
                case ItemType.Hose:
                    hose = itemData as HoseData;
                    break;
                case ItemType.BunkerGear:
                    bunkerGear = itemData as BunkerGearData;
                    break;
                case ItemType.OxygenTank:
                    oxygenTank = itemData as OxygenTankData;
                    break;
                default:
                    break;
            }
        }
        StatInit();
    }
    public void DisarmItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Hose:
                if(hose != null)
                {
                    hose.owner = null;
                    hose = null;
                }
                break;
            case ItemType.BunkerGear:
                if (bunkerGear != null)
                {
                    bunkerGear.owner = null;
                    bunkerGear = null;
                }
                break;
            case ItemType.OxygenTank:
                if (oxygenTank != null)
                {
                    oxygenTank.owner = null;
                    oxygenTank = null;
                }
                break;
            //case ItemType.Weapon:
            //    if (weapon != null)
            //    {
            //        weapon.owner = null;
            //        weapon = null;
            //    }
            //    break;
            default:
                break;
        }
        StatInit();
    }

}