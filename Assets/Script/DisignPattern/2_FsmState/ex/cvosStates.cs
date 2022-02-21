using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cvosIdle<T> : State where T : FSM<cvosState>
{
    T fsm;
    public cvosIdle(T _fsm)
    {
        fsm = _fsm;
    }

    float totalTime;
    public override void Enter()
    {
        totalTime = 0f;
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > 2f)
            fsm.ChangeState(cvosState.Move);
    }
}

public class cvosMove : State
{
    cvosFSM cvos;
    public cvosMove(cvosFSM fsm)
    {
        cvos = fsm;
    }


    float totalTime;
    public override void Enter()
    {
        totalTime = 0f;
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        cvos.transform.position += cvos.transform.forward * Time.deltaTime * cvos.speed;
        totalTime += Time.deltaTime;
        if (totalTime > 2f)
            cvos.ChangeState(cvosState.Idle);
    }
}