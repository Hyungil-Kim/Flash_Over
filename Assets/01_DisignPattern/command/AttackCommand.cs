using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : CommandBase
{
    public override void Excute()
    {
        Debug.Log("공격");
       
    }
    public override void Undo()
    {
        Debug.Log("공격 취소");
    }
}
