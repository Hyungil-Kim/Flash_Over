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
        //exp 차감
        exp = exp - MyDataTableMgr.levelUpTable.GetTable(level - 1).exp;
        
        // level 이 최대레밸이면 레벨업 x
        if(level == maxLevel)
        {
            return;
        }

        //level up 렌덤 스텟 증가
        level++;
        level = Mathf.Clamp(level, 0, maxLevel);
        var levelUpStat = MyDataTableMgr.chaStatTable.GetTable("LEVELUPSTAT");
        var statPoints = Random.Range(levelUpStat.min, levelUpStat.max);
        var levelUpStatList = new List<int>();
        var deficientStatList = new List<int>();

        //스텟별로 max 증가량이 있기 때문에 비교해주기 위해 리스트 생성 ...
        for (int i = 0; i < (int)CharacterStatType.Move; i++)
        {
            levelUpStatList.Add(0);
            deficientStatList.Add(i);
        }

        // statPoint가 있으면 랜덤으로 스탯 증가
        // 스텟이 max 값만큼 올랐으면 그 스텟을 리스트에서 제거해줌 
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
        //스탯 최신화
        StatInit();
    }

    //경험치를 얻었을때 호출돼야할것같음
    public void CheakExp()
    {
        //exp가 현재 필요한 경험치보다 크다면 쭉 레벨업
        while(exp >= MyDataTableMgr.levelUpTable.GetTable(level-1).exp)
        {
            LevelUp();
        }
    }

    //처음 세팅 init이라고 봐야할듯 ?
    public void SetCharacter()
    {
        //일단 이름, 등급, 직업의 테이블도 없고 정해진것도 없어서 임시로 넣어줌
        characterName = "아무개";
        characterGrade = "평범한";
        characterClass = "소방관";



        //변수선언
        //list : 랜덤하게 뽑아올려고 만듬..맨 위 스탯부터 랜덤배정되면 아래 스탯은 비교적 적게 낮을 확률이 늘어날것같아서
        //deficientStatList : 부족한 스탯 확인하려고 만듬
        //total : 들어갈 능력치의 총합
        level = 1;

        var list = new List<int>();
        var deficientStatList = new List<int>();
        var total = Random.Range(MyDataTableMgr.chaStatTable.GetTable("Total").min,
            MyDataTableMgr.chaStatTable.GetTable("Total").max);
        //최솟값 토탈에서 빼주기
        for (int i = 0; i < (int)CharacterStatType.Move; i++)
        {
            total -= MyDataTableMgr.chaStatTable.GetTable(i).min;
            AddStat((CharacterStatType)i, MyDataTableMgr.chaStatTable.GetTable(i).min, baseStats);
            list.Add(i);
            deficientStatList.Add(i);
        }
        //토탈에 들어가지 않는 능력치 설정
        for (int i = (int)CharacterStatType.Move; i < (int)CharacterStatType.Dmg; i++)
        {
            var type = (CharacterStatType)i;
            var statTable = MyDataTableMgr.chaStatTable.GetTable(type.ToString());
            var statValue = Random.Range(statTable.min, statTable.max);
            AddStat((CharacterStatType)i, statValue, baseStats);
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

        //성격 부여
        personality = new Personality();
        personality.SetPersonality(MyDataTableMgr.chaStatTable.GetTable("Personality").min,
            MyDataTableMgr.chaStatTable.GetTable("Personality").max);

        //스텟 최신화
        StatInit();
    }
    //스텟 추가
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

    //스텟 불러오기
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

}