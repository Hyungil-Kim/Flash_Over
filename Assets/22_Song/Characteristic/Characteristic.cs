using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacteristicTest : Buff
{
    private int prevStat = 0;
    public CharacteristicTest(CharacterData characterData)
    {
        data = MyDataTableMgr.characteristicTable.GetTable(0);
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
    }
    public override bool Check(Player player)
    {
        return true;
    }
    
    public override void EndBuff()
    {
    }
    
    public override void StartBuff()
    {
    }
    
    public override void WhileBuff()
    {
        if (cd.hp <= data.checkValue && prevStat == 0)
        {
            prevStat = cd.totalStats.str.stat;
            cd.totalStats.str.stat += Mathf.RoundToInt(data.increaseStat);
        }
        else if (cd.hp > data.checkValue && prevStat != 0)
        {
            cd.totalStats.str.stat = prevStat;
            prevStat = 0;
        }
    }
}

public class HeavyWeight : Buff
{
    public int prevValue;
    public HeavyWeight(CharacterData characterdata)
    {
        cd = characterdata;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
        
    }
    public override bool Check(Player player)
    {
        check = cd.weight < 20;

        base.Check(player);
        return ing;

    }

    public override void StartBuff()
    {
        if (!ing)
        {
            prevValue = cd.totalStats.move;
            cd.totalStats.move -= 1;
        }
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        if (!ing)
        {
            return;
        }
        base.WhileBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.move = prevValue;
        base.EndBuff();
    }

}

public class SaveClaimant : Buff
{
    public int prevValue;
    public SaveClaimant(CharacterData characterdata)
    {
        data = MyDataTableMgr.characteristicTable.GetTable(5);
        cd = characterdata;
        Init();
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
        MyDataTableMgr.eventTable.GetTable(0);
    }

    public override bool Check(Player player)
    {
        check = checkingCondition.InRangeClaimant;
        base.Check(player);
        return ing;
    }
    public override void StartBuff()
    {
        if(!ing)
        {
            prevValue = cd.totalStats.str.stat;
            cd.totalStats.str.stat += Mathf.RoundToInt(increaseValue);
        }
        base.StartBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.str.stat = prevValue;
        base.EndBuff();
    }
}

public class Friendship : Buff
{
    public Friendship(CharacterData characterdata)
    {
        data = MyDataTableMgr.characteristicTable.GetTable("Friendship");
        cd = characterdata;
        Init();
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
    }
    public override bool Check(Player player)
    {
        check = checkingCondition.InRangeFireman;
        base.Check(player);
        return ing;
    }
    public override void StartBuff()
    {
        if (!ing)
        {
            cd.totalStats.str.stat += Mathf.RoundToInt( increaseValue);
        }
        base.StartBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.str.stat -= Mathf.RoundToInt(increaseValue);
        base.EndBuff();
    }
}

public class Excited : Buff
{
    public Excited(CharacterData characterdata)
    {
        data = MyDataTableMgr.characteristicTable.GetTable("Excited");
        cd = characterdata;
        Init();
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnEnd);
    }
    public override bool Check(Player player)
    {
        check = cd.tiredScore >= increaseValue;
        base.Check(player);
        return ing;
    }
    public override void StartBuff()
    {
        cd.tiredScore -= Mathf.RoundToInt(increaseValue);
        base.StartBuff();
    }
    public override void EndBuff()
    {
        base.EndBuff();
    }
}

public class Movefaster : Buff
{
    public Movefaster(CharacterData characterdata)
    {
        data = MyDataTableMgr.characteristicTable.GetTable("Movefaster");
        cd = characterdata;
        Init();
        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
    }
    public override bool Check(Player player)
    {
        check = checkingCondition.OnGoing;
        base.Check(player);
        return ing;
    }
    public override void StartBuff()
    {
        cd.totalStats.move += Mathf.RoundToInt(increaseValue);
        base.StartBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.move -= Mathf.RoundToInt(increaseValue);
        base.EndBuff();
    }
}

public class Stressfull : Buff
{
    public Stressfull(CharacterData characterdata)
    {
        data = MyDataTableMgr.characteristicTable.GetTable("Stressfull");
        cd = characterdata;
        Init();
        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
    }
    public override bool Check(Player player)
    {
        check = checkingCondition.OnGoing;
        base.Check(player);
        return ing;
    }
    public override void StartBuff()
    {
        cd.totalStats.move -= Mathf.RoundToInt(decreaseValue);
        cd.totalStats.vision -= Mathf.RoundToInt(decreaseValue);
        base.StartBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.move += Mathf.RoundToInt(decreaseValue);
        cd.totalStats.vision += Mathf.RoundToInt(decreaseValue);
        base.EndBuff();
    }
}

public class Ptsd : Buff
{
    public int prevMove;
    public int prevVision;
    public Ptsd(CharacterData characterdata)
    {
        data = MyDataTableMgr.characteristicTable.GetTable("Ptsd");
        cd = characterdata;
        Init();
        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
    }
    public override bool Check(Player player)
    {
        check = checkingCondition.OnGoing;
        base.Check(player);
        return ing;
    }
    public override void StartBuff()
    {
        prevMove = cd.totalStats.move;
        prevVision = cd.totalStats.vision;
        cd.totalStats.move = Mathf.RoundToInt(decreaseValue);
        cd.totalStats.vision = Mathf.RoundToInt(decreaseValue);
        base.StartBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.move = prevMove;
        cd.totalStats.vision = prevVision;
        base.EndBuff();
    }
}

//public class Haughtiness : Buff
//{
//    public Haughtiness(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "거만";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//    }
//    public override bool Check(Player player)
//    {
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class StrongMind : Buff
//{
//    public StrongMind(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "강인한 정신";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        cd.benefit = 0;
//    }

//    public override void StartBuff()
//    {
//        cd.benefit = 0.01f;
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Boldness : Buff
//{
//    public Boldness(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "대담성";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//    }
//    public override bool Check(Player player)
//    {
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class WidePersPective : Buff
//{
//    public int prevValue;
//    public int increaseValue;
//    public WidePersPective(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
//        name = "넓은 시야";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        cd.totalStats.vision -= increaseValue;
//    }

//    public override void StartBuff()
//    {
//        prevValue = cd.totalStats.vision;
//        cd.totalStats.vision += increaseValue;

//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}


//public class FriendShip : Buff
//{
//    public FriendShip(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "동료";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.InRangeFireman;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        Debug.Log("동료 종료");
//    }

//    public override void StartBuff()
//    {
//        Debug.Log("동료 스타트");
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class MasterOfWeapon : Buff
//{
//    public MasterOfWeapon(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
//        name = "무기의 달인";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        Debug.Log("무기의 달인 종료");
//    }

//    public override void StartBuff()
//    {
//        Debug.Log("무기의 달인 스타트");
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class QuickHealing : Buff
//{
//    public QuickHealing(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.Main);

//        name = "빠른 치유";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Hearing : Buff
//{
//    public Hearing(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
//        name = "청각";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//    }
//    public override bool Check(Player player)
//    {
//        //check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}
//public class Resilience : Buff
//{
//    public Resilience(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "회복력";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = false;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        Debug.Log("회복력 종료");
//    }

//    public override void StartBuff()
//    {
//        Debug.Log("회복력 시작");
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}
//public class Coward : Buff
//{
//    public Coward(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnEnd);
//        name = "겁쟁이";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.InRangeFireman;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}
//public class Exaggerating : Buff
//{
//    int checkHp;
//    public Exaggerating(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnEnd);
//        name = "고통에 약함";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//        isInnate = false;
//    }
//    public override bool Check(Player player)
//    {
//        if (checkHp != 0 && checkHp > cd.hp)
//        {
//            check = true;
//        }
//        else
//        {
//            check = false;
//        }
//        checkHp = cd.hp;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}
//public class Laziness : Buff
//{
//    public Laziness(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.Main);
//        name = "게으름";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {

//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Stubborn : Buff
//{
//    public Stubborn(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "고집";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//        isInnate = false;
//    }
//    public override bool Check(Player player)
//    {
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

////public class Mute : Buff
////{
////    public Mute(CharacterData characterData)
////    {
////        cd = characterData;
////        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
////        name = "벙어리";
////        isCharacteristic = true;
////        isBadCharacteristic = false;
////    }
////    public override bool Check()
////    {
////        return base.Check();
////    }

////    public override void EndBuff()
////    {
////        base.EndBuff();
////    }

////    public override void StartBuff()
////    {
////        base.StartBuff();
////    }

////    public override void WhileBuff()
////    {
////        base.WhileBuff();
////    }
////}

//public class TooMuchStress : Buff
//{
//    int prevStat;
//    int decreseStat;
//    public TooMuchStress(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
//        name = "스트레스성 퇴행";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//        isInnate = false;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        cd.totalStats.str.stat += decreseStat;
//    }

//    public override void StartBuff()
//    {
//        prevStat = cd.totalStats.str.stat;
//        cd.totalStats.str.stat -= decreseStat;
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class LowSelfEsteem : Buff
//{
//    bool isOnce;
//    List<Claimant> prevClaimantList = new List<Claimant>();
//    int prevArea;
//    float penalty;
//    public LowSelfEsteem(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnEnd);
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "자기책망";
//        isCharacteristic = true;
//        isBadCharacteristic = true;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        if (isOnce)
//        {
//            var areaClaimants = Turn.claimants.Where((x) => x.claimantArea == cd.area).ToList();
//            if (areaClaimants.Count < prevClaimantList.Count && prevArea == cd.area)
//            {
//                check = true;
//            }
//            else
//            {
//                check = false;
//            }
//            prevClaimantList = areaClaimants;
//            prevArea = cd.area;
//        }
//        else
//        {
//            check = false;
//        }
//        return base.Check(player);

//    }

//    public override void EndBuff()
//    {
//    }

//    public override void StartBuff()
//    {
//        isOnce = true;
//        cd.penalty += penalty;
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Heroism : Buff
//{
//    public Heroism(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "영웅심";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = false;
//    }
//    public override bool Check(Player player)
//    {
//        var checkHand = player.handList.Where((x) => x.GetComponent<Claimant>() != null).ToList();
//        check = checkHand.Count > 0;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Intelligent : Buff
//{
//    public Intelligent(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.Main);
//        name = "대기만성";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}
//public class Nimble : Buff
//{
//    int prevMove;
//    int increseMove;
//    public Nimble(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
//        name = "날쌘돌이";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        cd.totalStats.move -= increseMove;
//    }

//    public override void StartBuff()
//    {
//        prevMove = cd.totalStats.move;
//        cd.totalStats.move += increseMove;
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Inside : Buff
//{
//    int opportunityValue;
//    int increaseStat;
//    int prevTurn;
//    List<Player> prevPlayer = new List<Player>();
//    public Inside(CharacterData characterData)
//    {
//        cd = characterData;
//        //buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "분위기 메이커";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = false;
//    }
//    public override bool Check(Player player)
//    {
//        check = prevTurn != Turn.turnCount;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        foreach (var player in prevPlayer)
//        {
//            player.cd.totalStats.hp.stat -= increaseStat;
//        }
//    }

//    public override void StartBuff()
//    {
//        foreach (var player in checkingCondition.playerList)
//        {
//            prevPlayer.Add(player);
//            player.cd.totalStats.hp.stat += increaseStat;
//        }
//    }

//    public override void WhileBuff()
//    {
//        if(prevTurn != Turn.turnCount)
//        {
//            prevTurn = Turn.turnCount;

//            foreach (var player in prevPlayer)
//            {
//                player.cd.totalStats.hp.stat -= increaseStat;
//            }
//            prevPlayer.Clear();

//            foreach (var player in checkingCondition.playerList)
//            {
//                prevPlayer.Add(player);
//                player.cd.totalStats.hp.stat += increaseStat;
//            }

//        }
//    }
//}
//public class FireTolerance : Buff
//{
//    int prevStat;
//    int increseStat;
//    public FireTolerance(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
//        name = "수족냉증";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        cd.totalStats.def -= increseStat;
//    }

//    public override void StartBuff()
//    {
//        cd.totalStats.def += increseStat;
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Stronger : Buff
//{
//    int prevStat;
//    int increseStat;
//    public Stronger(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "뻠형";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//        isInnate = true;
//    }
//    public override bool Check(Player player)
//    {
//        check = checkingCondition.OnGoing;
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        cd.totalStats.str.stat -= increseStat;
//    }

//    public override void StartBuff()
//    {
//        prevStat = cd.totalStats.str.stat;
//        cd.totalStats.str.stat += increseStat;
//    }

//    public override void WhileBuff()
//    {
//        base.WhileBuff();
//    }
//}

//public class Berserker : Buff
//{
//    int prevStat;
//    int increseStat;
//    public Berserker(CharacterData characterData)
//    {
//        cd = characterData;
//        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
//        name = "광전사";
//        isCharacteristic = true;
//        isBadCharacteristic = false;
//    }
//    public override bool Check(Player player)
//    {
//        return base.Check(player);
//    }

//    public override void EndBuff()
//    {
//        base.EndBuff();
//    }

//    public override void StartBuff()
//    {
//        base.StartBuff();
//    }

//    public override void WhileBuff()
//    {
//        if(cd.hp <= cd.totalStats.hp.stat /2 && prevStat == 0)
//        {
//            prevStat = cd.totalStats.str.stat;
//            cd.totalStats.str.stat += increseStat;
//        }
//        else if(cd.hp > cd.totalStats.hp.stat/2 && prevStat != 0)
//        {
//            cd.totalStats.str.stat = prevStat;
//            prevStat = 0;
//        }
//    }
//}

