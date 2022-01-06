using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimantIdleState : State
{
	Claimant fsm;
	public ClaimantIdleState(Claimant _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		
	}
	public override void Update()
	{

	}
	public override void Exit()
	{

	}
}
public class ClaimantMeetState : State
{
	Claimant fsm;
	public ClaimantMeetState(Claimant _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{

	}
	public override void Update()
	{

	}
	public override void Exit()
	{
		
	}
}
public class ClaimantResuceState : State
{
	Claimant fsm;
	public ClaimantResuceState(Claimant _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
	
	}
	public override void Update()
	{

	}
	public override void Exit()
	{

	}
}
public class ClaimantEndState : State
{
	Claimant fsm;
	public ClaimantEndState(Claimant _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		
	}
	public override void Update()
	{

	}
	public override void Exit()
	{

	}
}