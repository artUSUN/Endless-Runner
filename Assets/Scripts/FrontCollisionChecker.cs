using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class FrontCollisionChecker : MonoBehaviour
{
    private CapsuleCollider highCollider;

    private void Start()
    {
        highCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (StateBus.Player_ReduceCollider) highCollider.enabled = false;
        if (StateBus.Player_ReduceCollider == false) highCollider.enabled = true;
    }

    private IEnumerator ReduceCollider()
    {
        highCollider.enabled = false;
        float waitingTime = StateBus.Player_Data.RollAnimationDuration / StateBus.World_DifficultyCoefficient;
        if (waitingTime < 0.1f) waitingTime = 0.1f;
        yield return new WaitForSeconds(waitingTime);
        highCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Coin") == false || CompareTag("InteractObject") == false)
        {

        }
        //if (obstacle) += GameOver()
    }
}
