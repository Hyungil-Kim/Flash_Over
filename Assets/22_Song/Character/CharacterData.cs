using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AdvancedPeopleSystem;
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

    //경험치를 얻었을때 호출돼야할것같음
    public void IncreaseExp(int expValue)
    {

        exp += expValue;
        CheakExp();
    }
    public void CheakExp()
    {
        //exp가 현재 필요한 경험치보다 크다면 쭉 레벨업
        while (exp >= statData.maxexp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        //exp 차감
        exp = exp - statData.maxexp;

        // level 이 최대레밸이면 레벨업 x
        if (level == maxLevel)
        {
            return;
        }

        //level up 렌덤 스텟 증가
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

public class CharacteristicCheck
{
    //시야범위내에 동료가 계속 있으면
    public bool friendShip;
    //분사타입을 하나만 쓰면 이게 가능한가..
    public bool stubborn;
    //요구조자들고있을때요구조자가 죽으면..
    public bool mute;

    public float amoutOfDamage;
    public float amoutOfTakeDamage;
    public float amoutOfHeal;
    public float amoutOfTakeHeal;


}

public class CharacterData
{
    public CharacterCustomizationSetup setupModel;

    public string iconName = "testicon";

    public bool isHire;

    public bool isSelected;

    public CharacterStat baseStats = new CharacterStat();

    public CharacterStat totalStats = new CharacterStat();

    public CharacterStat resultStats = new CharacterStat();

    public int maxhp;
    public int hp;

    public int maxoxygen;
    public int oxygen;

    public int oxygenCount;

    public int addWeight;

    public int grade;

    public string prefabName = "male";

    public List<CharacteristicList> addCharacteristics = new List<CharacteristicList>();

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
    public List<Buff> badCharacteristics = new List<Buff>();
    public List<Buff> characteristics = new List<Buff>();

    public CharacteristicCheck check = new CharacteristicCheck();

    public int saveClaimantCount;

    public float benefit;
    public float penalty;

    public int area;


    public string state
    {
        get
        {
            if(isRest)
            {
                return "휴식중";
            }
            else if(tiredType == TiredType.Tired)
            {
                return "피곤함";
            }
            else if(tiredType == TiredType.Tired)
            {
                return "못나감";
            }
            else if(isSelected)
            {
                return "출동준비완료";
            }
            else
            {
                return "출동대기중";
            }
        }
    }
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
    //    //exp 차감
    //    exp = exp - MyDataTableMgr.levelUpTable.GetTable(level - 1).exp;
        
    //    // level 이 최대레밸이면 레벨업 x
    //    if(level == maxLevel)
    //    {
    //        return;
    //    }

    //    //level up 렌덤 스텟 증가
    //    level++;
    //    level = Mathf.Clamp(level, 0, maxLevel);
    //    var levelUpStat = MyDataTableMgr.chaStatTable.GetTable("LEVELUPSTAT");
    //    var statPoints = Random.Range(levelUpStat.min, levelUpStat.max);
    //    var levelUpStatList = new List<int>();
    //    var deficientStatList = new List<int>();

    //    //스텟별로 max 증가량이 있기 때문에 비교해주기 위해 리스트 생성 ...
    //    for (int i = 0; i < (int)CharacterStatType.Move; i++)
    //    {
    //        levelUpStatList.Add(0);
    //        deficientStatList.Add(i);
    //    }

    //    // statPoint가 있으면 랜덤으로 스탯 증가
    //    // 스텟이 max 값만큼 올랐으면 그 스텟을 리스트에서 제거해줌 
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
    //    //스탯 최신화
    //    StatInit();
    //}

    ////경험치를 얻었을때 호출돼야할것같음
    //public void CheakExp()
    //{
    //    //exp가 현재 필요한 경험치보다 크다면 쭉 레벨업
    //    while(exp >= MyDataTableMgr.levelUpTable.GetTable(level-1).exp)
    //    {
    //        LevelUp();
    //    }
    //}

    //처음 세팅 init이라고 봐야할듯 ?
    public void NewSetCharacter(int gradeIndex = 0)
    {
        grade = gradeIndex;

        characterName = "아무개";
        characterGrade = "평범한";
        characterClass = "소방관";

        //변수선언
        //list : 랜덤하게 뽑아올려고 만듬..맨 위 스탯부터 랜덤배정되면 아래 스탯은 비교적 적게 낮을 확률이 늘어날것같아서
        //deficientStatList : 부족한 스탯 확인하려고 만듬
        //total : 들어갈 능력치의 총합
        //level = 1;

        var list = new List<int>();
        var deficientStatList = new List<int>();
        var statDict = new Dictionary<string, int>();
        var statTable = MyDataTableMgr.chaStatTable.GetTable(grade);
        var total = Random.Range(statTable.totalmin,statTable.totalmax);

        //베이스 스탯 
        AddStat(CharacterStatType.Hp, statTable.basehp, baseStats);
        AddStat(CharacterStatType.Str, statTable.basestr, baseStats);
        AddStat(CharacterStatType.Lung, statTable.baselung, baseStats);

        //최솟값 토탈에서 빼주기
        for (int i = 0; i < (int)CharacterStatType.Move; i++)
        {
            list.Add(i);
            deficientStatList.Add(i);
        }

        //토탈에 들어가지 않는 능력치 설정
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

        //랜덤범위로 능력치들 설정
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

        //능력치가 남는다면 부족한 곳에 뿌리기
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

        //성격 부여
        //personality = new Personality();
        //personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("Personality").min,
        //    MyDataTableMgr.chaStatTable.GetTable("Personality").max);

        //임시로 나중에 가중치로 바뀌게 될지도 몰라 !
        //baseStats.str.stat *= 5;
        //baseStats.hp.stat *= 2;

        

        //버프 디버프.. 일케 넣어주는게 맞을까..
        //buff.Add(new testBuff(this));
        buff.Add(new HeavyWeight(this));
        buff.Add(new SaveClaimant(this));

        //스텟 최신화
        StatInit();

        //디포트모델
        var go = Resources.Load<GameObject>($"Prefabs/Character/{prefabName}");
        var custom = go.GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
        setupModel = custom.GetSetup();
    }
    //public void SetCharacter()
    //{
    //    //일단 이름, 등급, 직업의 테이블도 없고 정해진것도 없어서 임시로 넣어줌
    //    characterName = "아무개";
    //    characterGrade = "평범한";
    //    characterClass = "소방관";

    //    //변수선언
    //    //list : 랜덤하게 뽑아올려고 만듬..맨 위 스탯부터 랜덤배정되면 아래 스탯은 비교적 적게 낮을 확률이 늘어날것같아서
    //    //deficientStatList : 부족한 스탯 확인하려고 만듬
    //    //total : 들어갈 능력치의 총합
    //    //level = 1;

    //    var list = new List<int>();
    //    var deficientStatList = new List<int>();
    //    var total = Random.Range(MyDataTableMgr.chaStatTable.GetTable("Total").min,
    //        MyDataTableMgr.chaStatTable.GetTable("Total").max);
    //    //최솟값 토탈에서 빼주기
    //    for (int i = 0; i < (int)CharacterStatType.Move; i++)
    //    {
    //        total -= MyDataTableMgr.chaStatTable.GetTable(i).min;
    //        AddStat((CharacterStatType)i, MyDataTableMgr.chaStatTable.GetTable(i).min, baseStats);
    //        list.Add(i);
    //        deficientStatList.Add(i);
    //    }
    //    //토탈에 들어가지 않는 능력치 설정
    //    for (int i = (int)CharacterStatType.Move; i < (int)CharacterStatType.Dmg; i++)
    //    {
    //        var type = (CharacterStatType)i;
    //        var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
    //        var statValue = Random.Range(statTable.min, statTable.max);
    //        AddStat((CharacterStatType)i, statValue, baseStats);
    //    }
    //    //랜덤범위로 능력치들 설정
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

    //    //능력치가 남는다면 부족한 곳에 뿌리기
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

    //    //성격 부여
    //    //personality = new Personality();
    //    //personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("Personality").min,
    //    //    MyDataTableMgr.chaStatTable.GetTable("Personality").max);

    //    //임시로 나중에 가중치로 바뀌게 될지도 몰라 !
    //    baseStats.str.stat *= 5;
    //    baseStats.hp.stat *= 2;

    //    //버프 디버프.. 일케 넣어주는게 맞을까..
    //    //buff.Add(new testBuff(this));
    //    buff.Add(new HeavyWeight(this));
    //    buff.Add(new SaveClaimant(this));

    //    //스텟 최신화
    //    StatInit();
    //}
    public void SettingFixCharacter(int hp, int lung, int str, int move, int vision)
    {
        characterName = "아무개";
        characterGrade = "평범한";
        characterClass = "소방관";
        AddStat(CharacterStatType.Hp, hp, baseStats);
        AddStat(CharacterStatType.Lung, lung, baseStats);
        AddStat(CharacterStatType.Move, move, baseStats);
        AddStat(CharacterStatType.Str, str, baseStats);
        AddStat(CharacterStatType.Vision, vision, baseStats);

        buff.Add(new HeavyWeight(this));
        buff.Add(new SaveClaimant(this));

        //스텟 최신화
        StatInit();

        //장비
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

        //디포트모델
        var go = Resources.Load<GameObject>($"Prefabs/Character/{prefabName}");
        var custom = go.GetComponent<AdvancedPeopleSystem.CharacterCustomization>();
        setupModel = custom.GetSetup();

        GameData.userData.characterList.Add(this);

    }
    //스텟 추가
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

    //스텟 불러오기
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

    //장비 착용
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
    public void StartStatInit()
    {
        maxhp = totalStats.hp.stat;
        hp = maxhp;
        maxoxygen = totalStats.lung.stat;
        oxygen = maxoxygen;
        addWeight = 0;
        oxygenCount = totalStats.sta;
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
    public void BuildCharacteristic(CharacteristicList type)
    {
        var characteristic = new Buff();
        switch (type)
        {
            case CharacteristicList.HeavyWeight:
                characteristic = new HeavyWeight(this);
                break;
            case CharacteristicList.SaveClamant:
                characteristic = new HeavyWeight(this);
                break;
            case CharacteristicList.Haughtiness:
                characteristic = new Haughtiness(this);
                break;
            case CharacteristicList.StrongMind:
                characteristic = new StrongMind(this);
                break;
            case CharacteristicList.Boldness:
                characteristic = new Boldness(this);
                break;
            case CharacteristicList.WidePersPective:
                characteristic = new WidePersPective(this);
                break;
            case CharacteristicList.FriendShip:
                characteristic = new FriendShip(this);
                break;
            case CharacteristicList.MasterOfWeapon:
                characteristic = new MasterOfWeapon(this);
                break;
            case CharacteristicList.QuickHealing:
                characteristic = new QuickHealing(this);
                break;
            case CharacteristicList.Hearing:
                characteristic = new Hearing(this);
                break;
            case CharacteristicList.Resilience:
                characteristic = new Resilience(this);
                break;
            case CharacteristicList.Coward:
                characteristic = new Coward(this);
                break;
            case CharacteristicList.Exaggerating:
                characteristic = new Exaggerating(this);
                break;
            case CharacteristicList.Laziness:
                characteristic = new Laziness(this);
                break;
            case CharacteristicList.Stubborn:
                characteristic = new Stubborn(this);
                break;
            case CharacteristicList.Mute:
                //characteristic = new Mute(this);
                break;
            case CharacteristicList.TooMuchStress:
                characteristic = new TooMuchStress(this);
                break;
            case CharacteristicList.LowSelfEsteem:
                characteristic = new LowSelfEsteem(this);
                break;
            case CharacteristicList.Heroism:
                characteristic = new Heroism(this);
                break;
            case CharacteristicList.Intelligent:
                characteristic = new Intelligent(this);
                break;
            case CharacteristicList.Nimble:
                characteristic = new Nimble(this);
                break;
            case CharacteristicList.Inside:
                characteristic = new Inside(this);
                break;
            case CharacteristicList.FireTolerance:
                characteristic = new FireTolerance(this);
                break;
            case CharacteristicList.Stronger:
                characteristic = new Stronger(this);
                break;
            case CharacteristicList.Berserker:
                characteristic = new Berserker(this);
                break;
            default:
                break;
        }
        buff.Add(characteristic);
        if(characteristic.isCharacteristic)
        {
            characteristics.Add(characteristic);
        }
        if(characteristic.isBadCharacteristic)
        {
            badCharacteristics.Add(characteristic);
        }
    }

    public void LoadCd()
    {
        foreach (var buf in buff)
        {
            buf.SetCharacter(this);
        }
    }
    public void RemoveBad(float probability)
    {
        var random = Random.value;
        if(random <= probability)
        {
            var index = Random.Range(0, badCharacteristics.Count);
            var removeBuff = badCharacteristics[index];
            buff.Remove(removeBuff);
            badCharacteristics.Remove(removeBuff);
        }
    }

    public void AddCharacterisic(CharacteristicList type)
    {
        addCharacteristics.Add(type);
    }
    public void StartCheckingCharacteristic()
    {
        if (tiredScore >= 50)
        {
            if (0.3f + penalty >= Random.value)
            {
                AddCharacterisic(CharacteristicList.FriendShip);
            }
        }
    }

    public void EndCheckingCharacteristic()
    {
        if(check.friendShip)
        {
            if (0.03f + benefit >= Random.value)
            {
                AddCharacterisic(CharacteristicList.FriendShip);
            }
        }
        if(check.mute)
        {
            if (0.4f + penalty >= Random.value)
            {
                AddCharacterisic(CharacteristicList.Mute);
            }
        }
        if(check.stubborn)
        {
            if(0.1f + penalty >= Random.value)
            {
                AddCharacterisic(CharacteristicList.Stubborn);
            }
        }
        if(check.amoutOfDamage >= 50)
        {
            if(0.01f + benefit >= Random.value)
            {
                AddCharacterisic(CharacteristicList.MasterOfWeapon);
            }
        }
        if(check.amoutOfTakeDamage >= 20)
        {
            if(0.2f + penalty >= Random.value)
            {
                AddCharacterisic(CharacteristicList.Exaggerating);
            }
        }
        if(check.amoutOfHeal >= 50)
        {
            if(0.05f + benefit>= Random.value)
            {
                AddCharacterisic(CharacteristicList.Inside);
            }
        }
        if(check.amoutOfTakeHeal>=50)
        {
            if(0.05f + benefit >= Random.value)
            {
                AddCharacterisic(CharacteristicList.Resilience);
            }
        }
        if(hp == 1)
        {
            if(0.03f + benefit >= Random.value)
            {
                AddCharacterisic(CharacteristicList.Berserker);
            }
        }
        if(saveClaimantCount >= 30)
        {
            if(0.5f + benefit>= Random.value)
            {
                AddCharacterisic(CharacteristicList.Heroism);
            }
        }
    }

    public void StartStage()
    {
        StartCheckingCharacteristic();
        StartStatInit();
    }
    public void EndStage()
    {
        EndCheckingCharacteristic();
        foreach (var characteristic in addCharacteristics)
        {
            BuildCharacteristic(characteristic);
        }
    }
}