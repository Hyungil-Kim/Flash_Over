using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristic
{
}
public class HeavyWeight : Buff
{
    public int prevValue;
    public HeavyWeight(CharacterData characterdata)
    {
        cd = characterdata;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
        
    }
    public override bool Check()
    {
        check = cd.weight < 20;

        base.Check();
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
        cd = characterdata;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
    }

    public override bool Check()
    {
        check = checkingCondition.InRangeClaimant;
        base.Check();
        return ing;
    }



    public override void StartBuff()
    {
        if(!ing)
        {
            prevValue = cd.totalStats.str.stat;
            cd.totalStats.str.stat += 50;
        }
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.str.stat = prevValue;
        base.EndBuff();
    }
}

public class Haughtiness : Buff
{
    public Haughtiness(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "거만";
        isCharacteristic = true;
        isBadCharacteristic = true;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class StrongMind : Buff
{
    public StrongMind(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "강인한 정신";
        isCharacteristic = true;
        isBadCharacteristic = false;
        isInnate = true;
    }
    public override bool Check()
    {
        check = checkingCondition.OnGoing;
        return base.Check();
    }

    public override void EndBuff()
    {
        cd.benefit = 0;
    }

    public override void StartBuff()
    {
        cd.benefit = 0.01f;
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Boldness : Buff
{
    public Boldness(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "대담성";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class WidePersPective : Buff
{
    public int prevValue;
    public int increaseValue;
    public WidePersPective(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
        name = "넓은 시야";
        isCharacteristic = true;
        isBadCharacteristic = false;
        isInnate = true;
    }
    public override bool Check()
    {
        check = checkingCondition.OnGoing;
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        prevValue = cd.totalStats.vision;
        cd.totalStats.vision += increaseValue;
        
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}


public class FriendShip : Buff
{
    public FriendShip(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "동료";
        isCharacteristic = true;
        isBadCharacteristic = false;
        isInnate = true;
    }
    public override bool Check()
    {
        check = checkingCondition.InRangeFireman;
        return base.Check();
    }

    public override void EndBuff()
    {
        Debug.Log("동료 종료");
    }

    public override void StartBuff()
    {
        Debug.Log("동료 스타트");
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class MasterOfWeapon : Buff
{
    public MasterOfWeapon(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.GameStart);
        name = "무기의 달인";
        isCharacteristic = true;
        isBadCharacteristic = false;
        isInnate = true;
    }
    public override bool Check()
    {
        check = checkingCondition.OnGoing;
        return base.Check();
    }

    public override void EndBuff()
    {
        Debug.Log("무기의 달인 종료");
    }

    public override void StartBuff()
    {
        Debug.Log("무기의 달인 스타트");
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class QuickHealing : Buff
{
    public QuickHealing(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Main);

        name = "빠른 치유";
        isCharacteristic = true;
        isBadCharacteristic = false;
        isInnate = true;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Hearing : Buff
{
    public Hearing(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "청각";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}
public class Resilience : Buff
{
    public Resilience(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "회복력";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}
public class Coward : Buff
{
    public Coward(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "겁쟁이";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}
public class Exaggerating : Buff
{
    public Exaggerating(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "고통에 약함";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}
public class Laziness : Buff
{
    public Laziness(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "게으름";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Stubborn : Buff
{
    public Stubborn(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "고집";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Mute : Buff
{
    public Mute(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "벙어리";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class TooMuchStress : Buff
{
    public TooMuchStress(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "스트레스성 퇴행";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class LowSelfEsteem : Buff
{
    public LowSelfEsteem(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "자기책망";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Heroism : Buff
{
    public Heroism(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "영웅심";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Intelligent : Buff
{
    public Intelligent(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "대기만성";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}
public class Nimble : Buff
{
    public Nimble(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "날쌘돌이";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Inside : Buff
{
    public Inside(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "분위기 메이커";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}
public class FireTolerance : Buff
{
    public FireTolerance(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "수족냉증";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Stronger : Buff
{
    public Stronger(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "뻠형";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

public class Berserker : Buff
{
    public Berserker(CharacterData characterData)
    {
        cd = characterData;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.TurnStart);
        name = "광전사";
        isCharacteristic = true;
        isBadCharacteristic = false;
    }
    public override bool Check()
    {
        return base.Check();
    }

    public override void EndBuff()
    {
        base.EndBuff();
    }

    public override void StartBuff()
    {
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
}

