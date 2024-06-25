using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSideState);
        }
        if(player.IsGroundedDetected())
            stateMachine.ChangeState(player.idleState);
        if(xInput != 0)
        {
            player.SetVelocity(xInput* .8f * player.moveSpeed, rb.velocity.y);
        }
    }
}
