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
    public TrainingStat hp = new TrainingStat(CharacterStatType.Hp);
    public TrainingStat str = new TrainingStat(CharacterStatType.Str);
    public TrainingStat lung = new TrainingStat(CharacterStatType.Lung);
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
[System.Serializable]
public class TrainingStat
{
    public int stat;
    public int exp;
    public int level;
    //public int statIncrease = 2;

    //public int maxExp = 100;
    private int maxLevel = 99;

    public StatTableDataBase statData;
    public CharacterStatType statType;

    public TrainingStat(CharacterStatType type)
    {
        switch (type)
        {
            case CharacterStatType.Hp:
                statData = MyDataTableMgr.hpStatTable.GetTable(level);
                statType = type;
                break;
            case CharacterStatType.Str:
                statData = MyDataTableMgr.strStatTable.GetTable(level);
                statType = type;
                break;
            case CharacterStatType.Lung:
                statData = MyDataTableMgr.lungStatTable.GetTable(level);
                statType = type;
                break;
            default:
                break;
        }
    }

    //����ġ�� ������� ȣ��ž��ҰͰ���
    public void IncreaseExp(int expValue)
    {

        exp += expValue;
        CheakExp();
    }
    public void CheakExp()
    {
        //exp�� ���� �ʿ��� ����ġ���� ũ�ٸ� �� ������
        while (exp >= statData.maxexp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        //exp ����
        exp = exp - statData.maxexp;

        // level �� �ִ뷹���̸� ������ x
        if (level == maxLevel)
        {
            return;
        }

        //level up ���� ���� ����
        level++;
        level = Mathf.Clamp(level, 0, maxLevel);
        stat += statData.increaseStat;
        switch (statType)
        {
            case CharacterStatType.Hp:
                statData = MyDataTableMgr.hpStatTable.GetTable(level);
                break;
            case CharacterStatType.Str:
                statData = MyDataTableMgr.strStatTable.GetTable(level);
                break;
            case CharacterStatType.Lung:
                statData = MyDataTableMgr.lungStatTable.GetTable(level);
                break;
            default:
                break;
        }

    }
}

public class CharacterData
{
    public bool isHire;

    public bool isSelected;

    public CharacterStat baseStats = new CharacterStat();

    public CharacterStat totalStats = new CharacterStat();

    public CharacterStat resultStats = new CharacterStat();

    public int hp;

    public int oxygen;

    public int addWeight;

    public int grade;


    public int weight
    {
        get
        {
            var totalWeight = 0;
            if(hose != null)
            {
                totalWeight += hose.hoseData.weight;
            }
            if(bunkerGear != null)
            {
                totalWeight += bunkerGear.bunkerGearData.weight;
            }
            if(oxygenTank != null)
            {
                totalWeight += oxygenTank.oxygenTankData.weight;
            }
            if(consum1 != null)
            {
                totalWeight += consum1.itemData.weight * consum1.count;
            }
            if (consum2 != null)
            {
                totalWeight += consum2.itemData.weight * consum2.count;
            }
            totalWeight += addWeight;
            return totalStats.str.stat - totalWeight;
        }
    }

    public int stress;

    //public Personality personality;

    //public WeaponData weapon;
    public HoseData hose;
    public BunkerGearData bunkerGear;
    public OxygenTankData oxygenTank;

    public ConsumableItemData consum1;
    public ConsumableItemData consum2;

    public string characterName;
    public string characterClass;
    public string characterGrade;

    public bool isFireAble;
    public int tiredScore;
    public TiredType tiredType;

    public bool isRest;
    public int restCount;

    public bool isTraining;

    public List<Buff> buff = new List<Buff>();
    

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
        resultStats = baseStats.DeepCopy();
        switch (tiredType)
        {
            case TiredType.Normal:
                break;
            case TiredType.Tired:
                totalStats.lung.stat -= baseStats.lung.stat / 3;
                break;
            case TiredType.BigTired:
                totalStats.lung.stat = baseStats.lung.stat / 3;
                break;
            default:
                break;
        }
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
    //public void LevelUp()
    //{
    //    //exp ����
    //    exp = exp - MyDataTableMgr.levelUpTable.GetTable(level - 1).exp;
        
    //    // level �� �ִ뷹���̸� ������ x
    //    if(level == maxLevel)
    //    {
    //        return;
    //    }

    //    //level up ���� ���� ����
    //    level++;
    //    level = Mathf.Clamp(level, 0, maxLevel);
    //    var levelUpStat = MyDataTableMgr.chaStatTable.GetTable("LEVELUPSTAT");
    //    var statPoints = Random.Range(levelUpStat.min, levelUpStat.max);
    //    var levelUpStatList = new List<int>();
    //    var deficientStatList = new List<int>();

    //    //���ݺ��� max �������� �ֱ� ������ �����ֱ� ���� ����Ʈ ���� ...
    //    for (int i = 0; i < (int)CharacterStatType.Move; i++)
    //    {
    //        levelUpStatList.Add(0);
    //        deficientStatList.Add(i);
    //    }

    //    // statPoint�� ������ �������� ���� ����
    //    // ������ max ����ŭ �ö����� �� ������ ����Ʈ���� �������� 
    //    while (statPoints > 0)
    //    {
    //        var randomIndex = Random.Range(0, deficientStatList.Count);
    //        var index = deficientStatList[randomIndex];
    //        var type = (CharacterStatType)index;
    //        var statTable = MyDataTableMgr.chaStatTable.GetTable($"LEVELUP{type}");
            
    //        AddStat(type, 1, baseStats);
    //        levelUpStatList[randomIndex] += 1;
            
    //        if (levelUpStatList[randomIndex] == statTable.max)
    //        {
    //            deficientStatList.RemoveAt(randomIndex);
    //        }
    //        statPoints--;
    //    }
    //    //���� �ֽ�ȭ
    //    StatInit();
    //}

    ////����ġ�� ������� ȣ��ž��ҰͰ���
    //public void CheakExp()
    //{
    //    //exp�� ���� �ʿ��� ����ġ���� ũ�ٸ� �� ������
    //    while(exp >= MyDataTableMgr.levelUpTable.GetTable(level-1).exp)
    //    {
    //        LevelUp();
    //    }
    //}

    //ó�� ���� init�̶�� �����ҵ� ?
    public void NewSetCharacter(int gradeIndex = 0)
    {
        grade = gradeIndex;

        characterName = "�ƹ���";
        characterGrade = "�����";
        characterClass = "�ҹ��";

        //��������
        //list : �����ϰ� �̾ƿ÷��� ����..�� �� ���Ⱥ��� ���������Ǹ� �Ʒ� ������ ���� ���� ���� Ȯ���� �þ�Ͱ��Ƽ�
        //deficientStatList : ������ ���� Ȯ���Ϸ��� ����
        //total : �� �ɷ�ġ�� ����
        //level = 1;

        var list = new List<int>();
        var deficientStatList = new List<int>();
        var statDict = new Dictionary<string, int>();
        var statTable = MyDataTableMgr.chaStatTable.GetTable(grade);
        var total = Random.Range(statTable.totalmin,statTable.totalmax);

        //���̽� ���� 
        AddStat(CharacterStatType.Hp, statTable.basehp, baseStats);
        AddStat(CharacterStatType.Str, statTable.basestr, baseStats);
        AddStat(CharacterStatType.Lung, statTable.baselung, baseStats);

        //�ּڰ� ��Ż���� ���ֱ�
        for (int i = 0; i < (int)CharacterStatType.Move; i++)
        {
            list.Add(i);
            deficientStatList.Add(i);
        }

        //��Ż�� ���� �ʴ� �ɷ�ġ ����
        for (int i = (int)CharacterStatType.Move; i < (int)CharacterStatType.Dmg; i++)
        {
            var type = (CharacterStatType)i;
            var statValue = 0;
            switch (type)
            {
                case CharacterStatType.Move:
                    statValue = Random.Range(statTable.movemin, statTable.movemax);
                    break;
                case CharacterStatType.Vision:
                    statValue = Random.Range(statTable.visionmin, statTable.visionmax);
                    break;
                default:
                    break;
            }
            //var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
            //var statValue = Random.Range(statTable.min, statTable.max);
            AddStat((CharacterStatType)i, statValue, baseStats);
        }

        //���������� �ɷ�ġ�� ����
        while (list.Count > 0)
        {
            var randomIndex = Random.Range(0, list.Count);
            var index = list[randomIndex];
            
            var type = (CharacterStatType)index;
            var statValue = 0;
            switch (type)
            {
                case CharacterStatType.Hp:
                    
                    statValue = Random.Range(0, statTable.hpmax);
                    statValue = Mathf.Clamp(statValue, 0, total);
                    for (int i = 0; i < statValue; i++)
                    {
                        baseStats.hp.IncreaseExp(statTable.exp);
                    }
                    total = total - statValue;
                    if (statValue == statTable.hpmax)
                    {
                        deficientStatList.RemoveAt(randomIndex);
                    }
                    statDict.Add("hp", statValue);

                    list.RemoveAt(randomIndex);
                    break;
                case CharacterStatType.Str:
                    statValue = Random.Range(0, statTable.strmax);
                    statValue = Mathf.Clamp(statValue, 0, total);
                    for (int i = 0; i < statValue; i++)
                    {
                        baseStats.str.IncreaseExp(statTable.exp);
                    }
                    total = total - statValue;
                    if (statValue == statTable.strmax)
                    {
                        deficientStatList.RemoveAt(randomIndex);
                    }
                    statDict.Add("str", statValue);
                    list.RemoveAt(randomIndex);
                    break;
                case CharacterStatType.Lung:
                    statValue = Random.Range(0, statTable.lungmax);
                    statValue = Mathf.Clamp(statValue, 0, total);
                    for (int i = 0; i < statValue; i++)
                    {
                        baseStats.lung.IncreaseExp(statTable.exp);
                    }
                    total = total - statValue;
                    if (statValue == statTable.lungmax)
                    {
                        deficientStatList.RemoveAt(randomIndex);
                    }
                    statDict.Add("lung", statValue);
                    list.RemoveAt(randomIndex);
                    break;
                default:
                    break;
            }
        }

        //�ɷ�ġ�� ���´ٸ� ������ ���� �Ѹ���
        while (total > 0)
        {
            var randomIndex = Random.Range(0, deficientStatList.Count);
            var index = deficientStatList[randomIndex];
            var type = (CharacterStatType)index;
            switch (type)
            {
                case CharacterStatType.Hp:
                    baseStats.hp.IncreaseExp(statTable.exp);
                    if (statDict["hp"] == statTable.hpmax)
                    {
                        deficientStatList.RemoveAt(randomIndex);
                    }
                    break;
                case CharacterStatType.Str:
                    baseStats.str.IncreaseExp(statTable.exp);
                    if (statDict["str"] == statTable.strmax)
                    {
                        deficientStatList.RemoveAt(randomIndex);
                    }
                    break;
                case CharacterStatType.Lung:
                    baseStats.lung.IncreaseExp(statTable.exp);
                    if (statDict["lung"] == statTable.lungmax)
                    {
                        deficientStatList.RemoveAt(randomIndex);
                    }
                    break;
                default:
                    break;
            }
            total--;
        }

        //���� �ο�
        //personality = new Personality();
        //personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("Personality").min,
        //    MyDataTableMgr.chaStatTable.GetTable("Personality").max);

        //�ӽ÷� ���߿� ����ġ�� �ٲ�� ������ ���� !
        //baseStats.str.stat *= 5;
        //baseStats.hp.stat *= 2;

        

        //���� �����.. ���� �־��ִ°� ������..
        //buff.Add(new testBuff(this));
        buff.Add(new HeavyWeight(this));
        buff.Add(new SaveClaimant(this));

        //���� �ֽ�ȭ
        StatInit();
    }
    //public void SetCharacter()
    //{
    //    //�ϴ� �̸�, ���, ������ ���̺� ���� �������͵� ��� �ӽ÷� �־���
    //    characterName = "�ƹ���";
    //    characterGrade = "�����";
    //    characterClass = "�ҹ��";



    //    //��������
    //    //list : �����ϰ� �̾ƿ÷��� ����..�� �� ���Ⱥ��� ���������Ǹ� �Ʒ� ������ ���� ���� ���� Ȯ���� �þ�Ͱ��Ƽ�
    //    //deficientStatList : ������ ���� Ȯ���Ϸ��� ����
    //    //total : �� �ɷ�ġ�� ����
    //    //level = 1;

    //    var list = new List<int>();
    //    var deficientStatList = new List<int>();
    //    var total = Random.Range(MyDataTableMgr.chaStatTable.GetTable("Total").min,
    //        MyDataTableMgr.chaStatTable.GetTable("Total").max);
    //    //�ּڰ� ��Ż���� ���ֱ�
    //    for (int i = 0; i < (int)CharacterStatType.Move; i++)
    //    {
    //        total -= MyDataTableMgr.chaStatTable.GetTable(i).min;
    //        AddStat((CharacterStatType)i, MyDataTableMgr.chaStatTable.GetTable(i).min, baseStats);
    //        list.Add(i);
    //        deficientStatList.Add(i);
    //    }
    //    //��Ż�� ���� �ʴ� �ɷ�ġ ����
    //    for (int i = (int)CharacterStatType.Move; i < (int)CharacterStatType.Dmg; i++)
    //    {
    //        var type = (CharacterStatType)i;
    //        var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
    //        var statValue = Random.Range(statTable.min, statTable.max);
    //        AddStat((CharacterStatType)i, statValue, baseStats);
    //    }
    //    //���������� �ɷ�ġ�� ����
    //    while (list.Count > 0)
    //    {
    //        var randomIndex = Random.Range(0, list.Count);
    //        var index = list[randomIndex];
    //        var type = (CharacterStatType)index;
    //        var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
    //        if (total >= statTable.min)
    //        {
    //            var statValue = Random.Range(statTable.min, statTable.max);
    //            statValue = Mathf.Clamp(statValue, statTable.min, total);
    //            AddStat(type, statValue - statTable.min, baseStats);
    //            total = total - statValue + statTable.min;
    //            if(statValue == statTable.max)
    //            {
    //                deficientStatList.RemoveAt(randomIndex);
    //            }
    //        }
    //        list.RemoveAt(randomIndex);
    //    }

    //    //�ɷ�ġ�� ���´ٸ� ������ ���� �Ѹ���
    //    while(total>0)
    //    {
    //        var randomIndex = Random.Range(0, deficientStatList.Count);
    //        var index = deficientStatList[randomIndex];
    //        var type = (CharacterStatType)index;
    //        var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());

    //        AddStat(type, 1, baseStats);
    //        if(GetStat(type,true) == statTable.max)
    //        {
    //            deficientStatList.RemoveAt(randomIndex);
    //        }
    //        total--;
    //    }

    //    //���� �ο�
    //    //personality = new Personality();
    //    //personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("Personality").min,
    //    //    MyDataTableMgr.chaStatTable.GetTable("Personality").max);

    //    //�ӽ÷� ���߿� ����ġ�� �ٲ�� ������ ���� !
    //    baseStats.str.stat *= 5;
    //    baseStats.hp.stat *= 2;

    //    //���� �����.. ���� �־��ִ°� ������..
    //    //buff.Add(new testBuff(this));
    //    buff.Add(new HeavyWeight(this));
    //    buff.Add(new SaveClaimant(this));

    //    //���� �ֽ�ȭ
    //    StatInit();
    //}
    public void SettingFixCharacter(int hp, int lung, int str, int move, int vision)
    {
        characterName = "�ƹ���";
        characterGrade = "�����";
        characterClass = "�ҹ��";
        AddStat(CharacterStatType.Hp, hp, baseStats);
        AddStat(CharacterStatType.Lung, lung, baseStats);
        AddStat(CharacterStatType.Move, move, baseStats);
        AddStat(CharacterStatType.Str, str, baseStats);
        AddStat(CharacterStatType.Vision, vision, baseStats);

        buff.Add(new HeavyWeight(this));
        buff.Add(new SaveClaimant(this));

        //���� �ֽ�ȭ
        StatInit();

        //���
        var oxygenItem = new OxygenTankData(MyDataTableMgr.oxygenTankTable.GetTable(0));
        GameData.userData.oxygenTankList.Add(oxygenItem);
        EquipItem(oxygenItem, ItemType.OxygenTank);
        var bunkergearItem = new BunkerGearData(MyDataTableMgr.bunkerGearTable.GetTable(0));
        GameData.userData.bunkerGearList.Add(bunkergearItem);
        EquipItem(bunkergearItem, ItemType.BunkerGear);
        var hoseItem = new HoseData(MyDataTableMgr.hoseTable.GetTable(0));
        GameData.userData.hoseList.Add(hoseItem);
        EquipItem(hoseItem, ItemType.Hose);
        isHire = true;

        GameData.userData.characterList.Add(this);

    }
    //���� �߰�
    public void AddStat(CharacterStatType statType, float statValue, CharacterStat stats)
    {
        switch (statType)
        {
            case CharacterStatType.Hp:
                stats.hp.stat += (int)statValue;
                break;
            case CharacterStatType.Str:
                stats.str.stat += (int)statValue;
                break;
            case CharacterStatType.Lung:
                stats.lung.stat += (int)statValue;
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
                var hp = isBase ? baseStats.hp.stat : totalStats.hp.stat;
                return hp;
            case CharacterStatType.Str:
                var str = isBase ? baseStats.str.stat : totalStats.str.stat;
                return str;
            case CharacterStatType.Lung:
                var lung = isBase ? baseStats.lung.stat : totalStats.lung.stat;
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
    public void UseConsumItem(int slot)
    {
        
        switch (slot)
        {
            case 1:
                if(consum1 != null)
                {
                    consum1.count--;
                    if(consum1.count == 0)
                    {
                        consum1 = null;
                    }
                }
                break;
            case 2:
                if (consum2 != null)
                {
                    consum2.count--;
                    if (consum2.count == 0)
                    {
                        consum2 = null;
                    }
                }
                break;
            default:
                break;
        }
    }
    public void GameStart()
    {
        hp = totalStats.hp.stat;
        oxygen = totalStats.lung.stat;
        addWeight = 0;
    }
    public void SetStat()
    {
        StatInit();
        totalStats.str.stat *= 5;
        totalStats.hp.stat *= 2;
    }
    public bool GetTired()
    {
        var tired = TiredType.Normal;
        switch (tiredScore / 50)
        {
            case 0:
                break;
            case 1:
                tired = TiredType.Tired;
                break;
            case 2:
                tired = TiredType.BigTired;
                break;
            default:
                break;
        }
        bool istired = tired > tiredType;
        tiredType = tired;
        StatInit();
        return istired;
    }

    public void LoadCd()
    {
        foreach (var buf in buff)
        {
            buf.SetCharacter(this);
        }
    }
}