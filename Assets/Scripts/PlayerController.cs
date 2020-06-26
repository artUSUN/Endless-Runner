using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float offsetDistance = 1.55f;
    [SerializeField] private float movingSmooth = 60f;

    private Animator animator;
    private CharacterController characterController;
    private bool canPlayerOffset = true;

    private AnimationClip strafeAnimation;
    private float currentStrafeAnimationDuration;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        strafeAnimation = FindAnimationClipInAnimator(animator, "StrafeLeft"); 
    }

    // Update is called once per frame
    void Update()
    {
        OffsetLogic();
    }

    private void OffsetLogic()
    {
        
        float direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0 && canPlayerOffset)
        {

            canPlayerOffset = false;

            currentStrafeAnimationDuration = strafeAnimation.length / animator.speed;

           
            StartCoroutine(AnimatePlayer(direction));
            StartCoroutine(MovePlayer(direction));
            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.speed += 0.1f;
        }

    }

    IEnumerator MovePlayer(float direction)
    {
        float speed = offsetDistance / movingSmooth;

        for (int i = 0; i < movingSmooth; i++)
        {
            characterController.Move(Vector3.right * speed * direction);

            yield return new WaitForSeconds(currentStrafeAnimationDuration / movingSmooth);
        }

    }

    IEnumerator AnimatePlayer(float direction)
    {
        if (direction < 0)
        {
            animator.SetTrigger("LeftClick");
        }
        if (direction > 0)
        {
            animator.SetTrigger("RightClick");
        }

        yield return new WaitForSeconds(currentStrafeAnimationDuration);

        canPlayerOffset = true;
    }

    public static AnimationClip FindAnimationClipInAnimator(Animator animator, string nameOfClip)
    {
        AnimationClip[] animClipsArray = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in animClipsArray)
        {
            if (clip.name == nameOfClip)
            {
                return clip;
            }
        }

        Debug.Log($"Method FindAnimationClipInAnimator() can't find {nameOfClip} in Animator");
        return null;
    }
}
