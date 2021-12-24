using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> : MonoBehaviour
{
    private Dictionary<T, State> DState = new Dictionary<T, State>();
    private State curState;

    public T curStateName;
    public T prevStateName;

    public void AddState(T stateName, State _state)
    {
        DState.Add(stateName, _state);
    }
    public void ChangeState(T stateName)
    {
        prevStateName = curStateName;
        curState.Exit();
        SetState(stateName);
    }
    public void SetState(T stateName)
    {
        if(DState.ContainsKey(stateName))
        {
            curStateName = stateName;
            curState = DState[stateName];   
            curState.Enter();
        }
    }
    public void FSMUpdate()
    {
        curState.Update();
    }

}
