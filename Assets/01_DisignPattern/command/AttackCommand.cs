using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : CommandBase
{
    public override void Excute()
    {
        Debug.Log("����");
       
    }
    public override void Undo()
    {
        Debug.Log("���� ���");
    }
}
