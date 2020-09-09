using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateController : MonoBehaviour
{
    [SerializeField] private GameObject input;

    private void Update()
    {
        if (StateBus.GlobalState_GameOver) EventGameOver();
    }

    private void EventGameOver()
    {
        StateBus.World_IsGameActive = false;
        StateBus.Player_Data.Animator.SetTrigger("Death");
        StateBus.Input_Disable += true;
        StopAllCoroutines();
        StateBus.Player_Data.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
