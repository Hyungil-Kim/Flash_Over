using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum cvosState
{
    Idle,
    Move
}
public class cvosFSM : FSM<cvosState>
{

    public float speed;

    private void Start()
    {
        AddState(cvosState.Idle, new cvosIdle<cvosFSM>(this));
        AddState(cvosState.Move, new cvosMove(this));
        SetState(cvosState.Idle);
    }
    private void Update()
    {
        FSMUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(curStateName);
            Debug.Log(prevStateName);
        }
    }
}
