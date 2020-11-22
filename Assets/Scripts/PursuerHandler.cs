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
    [SerializeField] private float delayBeforeJump = 0.1f;
    [Header("Links")]
    [SerializeField] private Renderer[] meshes;

    private Animator animator;
    private Rigidbody rb;
    private Vector3 startPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Animations.SetBool("IsGrounded", animator, true);
        SetMeshActive(false);
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input_Vertical == 1 && Player_IsGrounded && Input_IsVerticalWorks) StartCoroutine(DoJump());
        if (Input_Vertical == -1 && Input_IsVerticalWorks) DoRoll();

        if (Player_ChangedFlagIsGrounded) Animations.SetBool("IsGrounded", animator, Player_IsGrounded);
        if (GlobalState_GameOver)
        {
            StopAllCoroutines();
            SetMeshActive(true);
            DoBeat();
        }

        if (Pursuer_CatchUp) StartCoroutine(DoCatchUp());

        if (Boost.Value is JetPack) StartCoroutine(FreezeForSec(Player_Data.JetPackDuration));

        FollowToPlayer_xPos();
    }

    private IEnumerator FreezeForSec(float time)
    {
        Freeze(true);
        yield return new WaitForSeconds(time);
        Freeze(false);
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


    private IEnumerator DoJump()
    {
        yield return new WaitForSeconds(delayBeforeJump / StateBus.World_DifficultyCoefficient);
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
        GetComponent<MoveToTarget_yzPos>().enabled = true;
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

    private void Freeze(bool flag)
    {
        rb.isKinematic = flag;
        GetComponent<SphereCollider>().isTrigger = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MoveToTarget_yzPos>().enabled = false;
        }
    }
}
