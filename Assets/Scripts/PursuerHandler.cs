using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateBus;

public class PursuerHandler : MonoBehaviour
{
    [Header("Catch Up & Leave options")]
    [SerializeField] private float catchUpDistance= 1f;
    [SerializeField] private float catchUpTimeInSec = 1f;
    [SerializeField] private float leaveTimeInSec = 2f;
    [Header("Beat Player options")]
    [SerializeField] private float beatMoveToPlayerDuration = 0.1f;
    [Header("Links")]
    [SerializeField] private Renderer[] meshes;
    

    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Animations.SetBool("IsGrounded", animator, true);
        SetMeshActive(false);
    }

    private void Update()
    {
        if (Input_Vertical == 1 && Player_IsGrounded) DoJump();
        if (Input_Vertical == -1) DoRoll();

        if (Player_ChangedFlagIsGrounded) Animations.SetBool("IsGrounded", animator, Player_IsGrounded);
        if (GlobalState_GameOver)
        {
            StopAllCoroutines();
            SetMeshActive(true);
            DoBeat();
        }

        if (Pursuer_CatchUp) StartCoroutine(DoCatchUp());

        FollowToPlayer_xPos();
    }

    private IEnumerator DoCatchUp()
    {
        //sound
        SetMeshActive(true);
        StartCoroutine(Actions.MoveTo_Z(rb, catchUpDistance, catchUpTimeInSec));
        yield return new WaitForSeconds(Player_Data.SideClashRecovery - leaveTimeInSec);
        StartCoroutine(Actions.MoveTo_Z(rb, -catchUpDistance, leaveTimeInSec));
        yield return new WaitForSeconds(leaveTimeInSec);
        SetMeshActive(false);
    }


    private void DoJump()
    {
        StartCoroutine(Actions.Jump(rb, Player_Data.JumpPower));
        Animations.SetTrigger("Jump", animator);
    }

    private void DoRoll()
    {
        StartCoroutine(Actions.Roll(rb, Player_Data.RollAnimationDuration, Player_Data.JumpPower));
        Animations.SetTrigger("Roll", animator);
    }

    private void DoBeat()
    {
        //sound
        Animations.SetTrigger("Beat", animator);
        GetComponent<MoveToTarget_zPos>().enabled = true;
        //rb.isKinematic = true;
        //GetComponent<SphereCollider>().isTrigger = true;
    }

    private void FollowToPlayer_xPos()
    {
        Vector3 curPos = transform.position;
        transform.position = new Vector3(Player_Transform.position.x, curPos.y, curPos.z);
    }

    private void SetMeshActive(bool isActive)
    {
        foreach (var item in meshes)
        {
            item.enabled = isActive;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MoveToTarget_zPos>().enabled = false;
        }
    }
}
