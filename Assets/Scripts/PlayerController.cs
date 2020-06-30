using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float offsetDistance = 1.55f;
    [SerializeField] private float offsetDuration = 0.5f;
    [SerializeField] private float jumpPower = 1.1f;
    [SerializeField] private float movingSmooth = 60f;
   

    private Animator animator;
    private CharacterController characterController;
    private AnimationClip strafeAnimation;

    private Vector3 playerVelocity;

    //задел на повышение сложности по мере игры
    [HideInInspector] public float gameDifficultyCoefficient = 1;

    private float currentStrafeAnimationDuration;
    private float currentTrackPosition = 1; //0 - left, 1 - central, 2 - right
    private float gravityValue = -9.81f;
    private float player_zPosition;

    private bool isPlayerGrounded;
    private bool canOffset = true;
    private bool needJump = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        strafeAnimation = FindAnimationClipInAnimator(animator, "RunningLeftTurn");

        player_zPosition = characterController.transform.position.z;
    }

    
    void Update()
    {
        isPlayerGrounded = characterController.isGrounded;
        //isPlayerGrounded = IsPlayerGrounded();

        Debug.Log(characterController.collisionFlags == CollisionFlags.Below);

        //FreezePos_Z();

        OffsetCondition();
        JumpCondition();

        FallingAnimationLogic();
    }

    private void FixedUpdate()
    {
        JumpAndGravityLogic();
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

                //задел для увеличения сложности игры путем изменения скорости
                currentStrafeAnimationDuration = strafeAnimation.length / animator.speed;

                StartCoroutine(OffsetAnimationLogic(direction));
                StartCoroutine(OffsetLogic(direction));
            }

            else
            {
                currentTrackPosition -= direction;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.speed += 0.1f;
        }

    }

    IEnumerator OffsetLogic(float direction)
    {
        float distance = offsetDistance / movingSmooth;

        for (int i = 0; i < movingSmooth; i++)
        {
            characterController.Move(Vector3.right * distance * direction);

            yield return new WaitForSeconds((offsetDuration / gameDifficultyCoefficient) / movingSmooth);
        }

        canOffset = true;
    }

    IEnumerator OffsetAnimationLogic(float direction)
    {
        //if (direction < 0)
        //{
        //    animator.SetTrigger("LeftClick");
        //}
        //if (direction > 0)
        //{
        //    animator.SetTrigger("RightClick");
        //}

        float angleOfTurn = (45f / movingSmooth) * direction;
        float quarterTurnDuration = (0.25f * (offsetDuration / gameDifficultyCoefficient));

        for (int i = 0; i < movingSmooth; i++)
        {
            characterController.transform.Rotate(0f, angleOfTurn, 0f);

            yield return new WaitForSeconds(quarterTurnDuration / movingSmooth);
        }

        //yield return new WaitForSeconds(quarterTurnDuration);

        for (int i = 0; i < movingSmooth; i++)
        {
            characterController.transform.Rotate(0f, -angleOfTurn, 0f);

            yield return new WaitForSeconds(2 * quarterTurnDuration / movingSmooth);
        }

    }

    private void JumpCondition()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {  
             needJump = true;  
        }
    }

    private void JumpAndGravityLogic()
    {

        if (playerVelocity.y < 0)
        {
            if (isPlayerGrounded)
            {
                playerVelocity.y = 0f;
            }
        }

        if (needJump)
        {
            animator.SetTrigger("JumpClick");

            playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravityValue);

            needJump = false;
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        
        characterController.Move(playerVelocity * Time.fixedDeltaTime);
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

    private void FreezePos_Z()
    {
        if (characterController.transform.position.z != player_zPosition)
        {
            Vector3 currentPlayerPosition = new Vector3(characterController.transform.position.x, characterController.transform.position.y, player_zPosition);
            characterController.transform.position = currentPlayerPosition;
        }
    }

    //private bool IsPlayerGrounded()
    //{
    //    if (Physics.Raycast(transform.position, Vector3.down, 0.2f))
    //    {
    //        return true;
    //    }
    //    else return false;
    //}

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
