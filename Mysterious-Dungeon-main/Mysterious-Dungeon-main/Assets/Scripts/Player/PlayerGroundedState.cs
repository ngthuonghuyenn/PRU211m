using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.CheckForDashInput();
        if (Input.GetKeyDown(KeyCode.I))
        {
            stateMachine.ChangeState(player.blackHoleState);
        }
        if(Input.GetKeyDown(KeyCode.U) && HasNoSword()) 
        {
            stateMachine.ChangeState(player.aimSwordState);
        }
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            stateMachine.ChangeState(player.counterAttackState);
        }
        if(Input.GetKey(KeyCode.J))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        if (!player.IsGroundedDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
        if(Input.GetKeyDown(KeyCode.K) && player.IsGroundedDetected()) 
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
