using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float offsetDistance = 1.5f;
    [SerializeField] private float offsetDuration = 0.5f;
    [SerializeField] private float jumpPower = 1.1f;
    [SerializeField] private float movingSmooth = 60f;

    private Rigidbody _rb;

    private Animator animator;
    private AnimationClip slideAnimationClip;

    //задел на повышение сложности по мере игры
    [HideInInspector] public float gameDifficultyCoefficient = 1;

    private float currentTrackPosition = 1; //0 - left, 1 - central, 2 - right

    private bool isPlayerGrounded;
    private bool canOffset = true;
    private bool canJump = true;
    private bool canSlide = true;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        slideAnimationClip = FindAnimationClipInAnimator(animator, "RunningRoll");
    }

    
    void Update()
    {
        isPlayerGrounded = IsPlayerGrounded();
        
        OffsetCondition();
        JumpAndSlideCondition();

        FallingAnimationLogic();

        //задел на изменение сложности игры со временем
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.speed += 0.01f;
            gameDifficultyCoefficient += 0.1f;
        }
    }

    private void FixedUpdate()
    {
 
    }

    private void OffsetCondition()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0 && canOffset)
        {
            currentTrackPosition += direction;

            if (0 <= currentTrackPosition && currentTrackPosition <= 2)
            {
                canOffset = false;

                StartCoroutine(OffsetLogic(direction));
                StartCoroutine(OffsetAnimationLogic(direction));
            }

            else
            {
                currentTrackPosition -= direction;
            }
            
        }
    }

    private void JumpAndSlideCondition()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput == 1 && isPlayerGrounded && canJump)
        {
            canJump = false;
            StartCoroutine(JumpLogic());
        }

        if (verticalInput == -1 && canSlide)
        {
            canSlide = false;
            StartCoroutine(SlideLogic());
        }
    }

    IEnumerator OffsetLogic(float direction)
    {
        float distance = offsetDistance / movingSmooth;

        for (int i = 0; i < movingSmooth; i++)
        {
            _rb.transform.position += (Vector3.right * distance * direction);

            yield return new WaitForSeconds((offsetDuration / gameDifficultyCoefficient) / movingSmooth);
        }

        canOffset = true;
    }

    IEnumerator OffsetAnimationLogic(float direction)
    {
  
        float angleOfTurn = (30f / 10) * direction;
        float quarterTurnDuration = (0.25f * (offsetDuration / gameDifficultyCoefficient));


        for (int i = 0; i < 10; i++)
        {
            _rb.transform.Rotate(0f, angleOfTurn, 0f);

            yield return new WaitForSeconds(quarterTurnDuration / 10);
        }

        yield return new WaitForSeconds(2 * quarterTurnDuration);

        for (int i = 0; i < 10; i++)
        {
            _rb.transform.Rotate(0f, -angleOfTurn, 0f);

            yield return new WaitForSeconds(quarterTurnDuration / 10);
        }

    }

    IEnumerator JumpLogic()
    {
        animator.SetTrigger("JumpClick");

        canSlide = true;

        _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);

        canJump = true;
    }

    IEnumerator SlideLogic()
    {
        animator.SetTrigger("SlideClick");

        if (!isPlayerGrounded)
        {
            _rb.velocity = Vector3.zero;

            _rb.AddForce(Vector3.down * jumpPower, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(slideAnimationClip.length);

        canSlide = true;

    }

    private void FallingAnimationLogic()
    {
        if (isPlayerGrounded)
        {
            animator.SetBool("IsInTheAir", false);
        }
        else
        {
            animator.SetBool("IsInTheAir", true);
        }
    }

    private bool IsPlayerGrounded()
    {
        if (Physics.Raycast(_rb.transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f))
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Возвращает анимацию по названию из заданного аниматора
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="nameOfAnimationClip"></param>
    /// <returns></returns>
    public static AnimationClip FindAnimationClipInAnimator(Animator animator, string nameOfAnimationClip)
    {
        AnimationClip[] animClipsArray = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in animClipsArray)
        {
            if (clip.name == nameOfAnimationClip)
            {
                return clip;
            }
        }

        Debug.Log($"Method FindAnimationClipInAnimator() can't find {nameOfAnimationClip} in Animator");
        return null;
    }
}
