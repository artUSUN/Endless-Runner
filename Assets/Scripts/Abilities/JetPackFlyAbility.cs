using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateBus;

public class JetPackFlyAbility : MonoBehaviour
{
    [SerializeField] private Transform spine;
    [SerializeField] private Transform jetPackObject;
    [SerializeField] private ParticleSystem[] trailFX;
    [SerializeField] private Animation destroyAnimation;

    private void Start()
    {
        //trailFX = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Boost.Value is JetPack) StartCoroutine(Flying());
    }

    private IEnumerator Flying()
    {
        Animations.SetBool("JetPackFly", Player_Data.Animator, true);
        GetDress();
        StartCoroutine(Actions.MoveTo_Y(Player_Data.Rigidbody, Player_Data.JetPackJumpRaiseDistation, Player_Data.JetPackJumpRaiseDuration));
        LockInput(true);
        PlayAllPS(trailFX);
        Player_Data.Rigidbody.useGravity = false;
        Camera_Script.SetDelta_Z(Camera_Script.DistantionToPlayer.z * 2);
        //sound

        yield return new WaitForSeconds(Player_Data.JetPackDuration);
        StopAllPS(trailFX);
        Animations.SetBool("JetPackFly", Player_Data.Animator, false);
        Player_Data.Rigidbody.useGravity = true;
        LockInput(false);
        PlayDestroyAnimation();
        Undress();
        Camera_Script.SetDelta_Z(Camera_Script.DistantionToPlayer.z * 0.75f);

        //stop sound

        yield return new WaitForSeconds(1f);
        destroyAnimation.gameObject.SetActive(false);
        Camera_Script.SetDeltaDefault();
    }

    private void GetDress()
    {
        jetPackObject.gameObject.SetActive(true);
        //jetPackObject.rotation = spine.localRotation;
        //jetPackObject.parent = spine;
    }

    private void Undress()
    {
        //jetPackObject.parent = transform;
        jetPackObject.gameObject.SetActive(false);
    }

    private void PlayDestroyAnimation()
    {
        destroyAnimation.gameObject.SetActive(true);
        destroyAnimation.Play();
    }


    private void LockInput(bool flag)
    {
        Input_IsVerticalWorks = !flag;
        Input_IsHorizontalLocked = flag;
    }

    private void PlayAllPS(ParticleSystem[] particleSystems)
    {
        foreach (var item in particleSystems)
        {
            item.Play();
        }
    }

    private void StopAllPS(ParticleSystem[] particleSystems)
    {
        foreach (var item in particleSystems)
        {
            item.Stop();
        }
    }
}
