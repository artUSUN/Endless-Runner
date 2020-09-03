using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float offsetDuration = 0.5f;
    [SerializeField] private float jumpPower = 1.1f;
    [SerializeField] private float offsetAnimationTurnAngle = 60f;

    private Rigidbody rb;
     
    private Animator animator;
    private AnimationClip rollAnimationClip;

    private float currentTrackPosition = 1; //0 - left, 1 - middle, 2 - right

    private bool isPlayerGrounded;
    private bool canOffset = true;
    private bool canJump = true;
    private bool canRoll = true;

    private CapsuleCollider highTriggerCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rollAnimationClip = FindAnimationClipInAnimator(animator, "RunningRoll");

        highTriggerCollider = GetComponent<CapsuleCollider>();

        StateBus.Player_Transform = transform;
    }

    
    void Update()
    {
        isPlayerGrounded = IsPlayerGrounded();

        FallingAnimationLogic();

        //задел на изменение сложности игры со временем
        
        JumpAndSlideCondition();
        OffsetCondition();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("InteractObject"))
    //    {
    //        StartCoroutine(GameOver());
    //    }
    //}

    private void OffsetCondition()
    {
        float direction = StateBus.Input_Horizontal;

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
        float verticalInput = StateBus.Input_Vertical;

        if (verticalInput == 1 && isPlayerGrounded && canJump)
        {
            canJump = false;
            StartCoroutine(JumpLogic());
        }

        if (verticalInput == -1 && canRoll)
        {
            canRoll = false;
            StartCoroutine(RollLogic());
        }
    }

    private IEnumerator OffsetLogic(float direction)
    {
        float speed = StateBus.Treadmill_LineWidht / (offsetDuration / StateBus.World_DifficultyCoefficient);

        float target = rb.transform.position.x + (StateBus.Treadmill_LineWidht * direction);

        while (rb.transform.position.x != target)
        {
            float xCoordinate = Mathf.MoveTowards(rb.transform.position.x, target, speed * Time.fixedDeltaTime);
            rb.transform.position = new Vector3(xCoordinate, rb.transform.position.y, rb.transform.position.z);

            yield return new WaitForFixedUpdate(); 
        }

        canOffset = true;
    }

    private IEnumerator OffsetAnimationLogic(float direction)
    {
  
        float turnAngle = offsetAnimationTurnAngle * direction;

        float quarterTurnDuration = (0.25f * (offsetDuration / StateBus.World_DifficultyCoefficient));


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

    private IEnumerator JumpLogic()
    {
        animator.SetTrigger("JumpClick");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        StopCoroutine(RollLogic());
        highTriggerCollider.enabled = true;
        canRoll = true;

        yield return new WaitForSeconds(0.1f);

        canJump = true;
    }

    private IEnumerator RollLogic()
    {
        highTriggerCollider.enabled = false;

        animator.SetTrigger("RollClick");

        if (!isPlayerGrounded)
        {
            rb.velocity = Vector3.zero;

            rb.AddForce(Vector3.down * jumpPower, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(rollAnimationClip.length / StateBus.World_DifficultyCoefficient);

        canRoll = true;
        highTriggerCollider.enabled = true;
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
        return Physics.Raycast(rb.transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, 1, QueryTriggerInteraction.Ignore);
    }

    private IEnumerator GameOver()
    {
        StateBus.World_IsGameActive = false;

        StateBus.Input_IsInputWorks = false;

        animator.speed = 1;

        animator.SetTrigger("Death");

        yield return new WaitForSeconds(FindAnimationClipInAnimator(animator, "Fall").length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


