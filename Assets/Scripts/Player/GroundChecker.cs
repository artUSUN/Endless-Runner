using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool lastFrameCondition;

    private void Start()
    {
        StateBus.Player_IsGrounded = Actions.IsRigidbodyGrounded(StateBus.Player_Data.Rigidbody);
        lastFrameCondition = StateBus.Player_IsGrounded;
    }

    private void Update()
    {
        StateBus.Player_IsGrounded = Actions.IsRigidbodyGrounded(StateBus.Player_Data.Rigidbody);

        if (lastFrameCondition != StateBus.Player_IsGrounded)
        {
            StateBus.Player_ChangedFlagIsGrounded += true;
            lastFrameCondition = StateBus.Player_IsGrounded;
        }
    }
}
