using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerAnimation : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (StateBus.Input_Horizontal != 0) AnimateShift(StateBus.Input_Horizontal);
    }

    private void AnimateShift(int direction)
    {
        StartCoroutine(Animations.TurnTo(rb, StateBus.Player_Data.ShiftAnimationAngleOfTurn, direction));
    }
}
