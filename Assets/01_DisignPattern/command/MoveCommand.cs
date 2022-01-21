using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : CommandBase
{
    public override void Excute()
    {
        Debug.Log("이동");
    }
    public override void Undo()
    {
        Debug.Log("이동취소");
    }
}
