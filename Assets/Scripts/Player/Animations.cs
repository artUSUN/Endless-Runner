using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Animations
{
    public static void SetTrigger(string name, Animator animator)
    {
        animator.SetTrigger(name);
    }

    public static void SetBool(string name, Animator animator, bool state)
    {
        animator.SetBool(name, state);
    }

    public static IEnumerator TurnTo(Rigidbody rb, float angleOfTurn, float direction)
    {
        float turnAngle = angleOfTurn * direction;
        float quarterTurnDuration = 0.25f * (StateBus.Player_Data.ShiftDuration / StateBus.World_DifficultyCoefficient);

        for (float i = 0; i <= quarterTurnDuration + Time.fixedDeltaTime; i += Time.fixedDeltaTime)
        {
            rb.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(0, turnAngle, i / quarterTurnDuration), Vector3.up);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(quarterTurnDuration);
        for (float i = 0; i <= quarterTurnDuration + Time.fixedDeltaTime; i += Time.fixedDeltaTime)
        {
            rb.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(turnAngle, 0, i / quarterTurnDuration), Vector3.up);
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// Возвращает анимацию по названию из заданного аниматора
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="nameOfAnimation"></param>
    /// <returns></returns>
    public static AnimationClip FindAnimationInAnimator(Animator animator, string nameOfAnimation)
    {
        AnimationClip[] animClipsArray = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in animClipsArray)
        {
            if (clip.name == nameOfAnimation)
            {
                return clip;
            }
        }

        Debug.Log($"Method FindAnimationClipInAnimator() can't find {nameOfAnimation} in Animator");
        return null;
    }
}
