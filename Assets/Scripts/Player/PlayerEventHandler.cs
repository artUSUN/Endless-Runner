using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateBus;

public class PlayerEventHandler : MonoBehaviour
{
    private void Start()
    {
        Animations.SetBool("IsGrounded", Player_Data.Animator, Player_IsGrounded);
    }

    private void Update()
    {
        if (Input_Vertical == 1 && Player_IsGrounded) DoJump();
        if (Input_Vertical == -1) DoRoll();
        if (Input_Horizontal != 0) DoShift(Input_Horizontal);
        if (Player_SideImpact) DoReturnBack();
        if (Player_ChangedFlagIsGrounded) Animations.SetBool("IsGrounded", Player_Data.Animator, Player_IsGrounded);
    }

    private void DoJump()
    {
        StartCoroutine(Actions.Jump(Player_Data.Rigidbody, Player_Data.JumpPower));
        Animations.SetTrigger("Jump", Player_Data.Animator);
        //PlayJumpSound
    }

    private void DoRoll()
    {
        StartCoroutine(Actions.Roll(Player_Data.Rigidbody, Player_Data.RollAnimationDuration, Player_Data.JumpPower));
        Animations.SetTrigger("Roll", Player_Data.Animator);
        //PlayRollSound
    }

    private void DoShift(int direction)
    {
        StartCoroutine(Actions.Shift(Player_Data.Rigidbody, Player_Data.Collider, direction, Player_Data.ShiftDuration));
        StartCoroutine(Animations.TurnTo(Player_Data.Rigidbody, Player_Data.ShiftAnimationAngleOfTurn, direction));
        //PlayShiftSound
    }

    private void DoReturnBack()
    {
        StartCoroutine(Actions.ReturnBackToLine(Player_Data.Rigidbody, Player_Data.ReturnSpeedIfFailShift));
        //animation
        //particle
        //sound
    }
}
