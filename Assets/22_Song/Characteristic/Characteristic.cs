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
        name = "�Ÿ�";
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
        name = "������ ����";
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
        name = "��㼺";
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
        name = "���� �þ�";
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
        name = "����";
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
        Debug.Log("���� ����");
    }

    public override void StartBuff()
    {
        Debug.Log("���� ��ŸƮ");
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
        name = "������ ����";
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
        Debug.Log("������ ���� ����");
    }

    public override void StartBuff()
    {
        Debug.Log("������ ���� ��ŸƮ");
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

        name = "���� ġ��";
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
        name = "û��";
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
        name = "ȸ����";
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
        name = "������";
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
        name = "���뿡 ����";
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
        name = "������";
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
        name = "����";
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
        name = "���";
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
        name = "��Ʈ������ ����";
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
        name = "�ڱ�å��";
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
        name = "������";
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
        name = "��⸸��";
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
        name = "���ڵ���";
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
        name = "������ ����Ŀ";
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
        name = "��������";
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
        name = "����";
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
        name = "������";
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

