using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public static class Actions
{
    public static IEnumerator Jump(Rigidbody rb, float power)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * power, ForceMode.Impulse);
        yield return null;
    }

    public static IEnumerator Roll(Rigidbody rb, float duration, float power)
    {
        StateBus.Player_DisableHighCollider += true;

        if (!StateBus.Player_IsGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * power, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(duration / StateBus.World_DifficultyCoefficient);

        StateBus.Player_EnableHighCollider += true;
    }

    public static IEnumerator Shift(Rigidbody rb, SphereCollider collider, int direction, float shiftDuration)
    {
        StateBus.Input_Disable += true;
        float target = CurrentLineCoordinate() + (StateBus.Treadmill_LineWidht * direction);
        float speed = StateBus.Treadmill_LineWidht / (shiftDuration / StateBus.World_DifficultyCoefficient);
        while (!Mathf.Approximately(rb.transform.position.x, target))
        {
            float xCoordinate = Mathf.MoveTowards(rb.transform.position.x, target, speed * Time.fixedDeltaTime);
            rb.transform.position = new Vector3(xCoordinate, rb.transform.position.y, rb.transform.position.z);
            if (CheckObstacle(rb, collider, direction))
            {
                StateBus.Player_SideClash += true;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
        StateBus.Player_CurrentLine += direction;
        StateBus.Input_Enable += true;
    }


    public static IEnumerator ReturnBackToLine(Rigidbody rb)
    {
        float target = CurrentLineCoordinate();
        while (!Mathf.Approximately(rb.transform.position.x, target))
        {
            float xCoordinate = Mathf.MoveTowards(rb.transform.position.x, target, StateBus.Player_Data.ReturnSpeedIfFailShift * Time.fixedDeltaTime);
            rb.transform.position = new Vector3(xCoordinate, rb.transform.position.y, rb.transform.position.z);
            yield return new WaitForFixedUpdate();
        }
        StateBus.Input_Enable += true;
    }

    public static bool IsRigidbodyGrounded(Rigidbody rb)
    {
        return Physics.Raycast(rb.transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, 1, QueryTriggerInteraction.Ignore);
    }

    public static IEnumerator MoveTo_Z(Rigidbody rb, float distantion, float duration)
    {
        float target = rb.position.z + distantion;
        float speed = Math.Abs(distantion) / duration;
        while (!Mathf.Approximately(rb.position.z, target))
        {
            float zCoordinate = Mathf.MoveTowards(rb.position.z, target, speed * Time.fixedDeltaTime);
            rb.transform.position = new Vector3(rb.position.x, rb.position.y, zCoordinate);
            yield return new WaitForFixedUpdate();
        }
        rb.transform.position = new Vector3(rb.position.x, rb.position.y, target);
    }

    private static bool CheckObstacle(Rigidbody rb, SphereCollider collider, float direction)
    {
        Vector3 spawnPos = new Vector3(rb.transform.position.x, rb.transform.position.y + 0.1f + collider.radius, rb.transform.position.z);
        Debug.DrawRay(spawnPos, (Vector3.right * direction) * StateBus.Player_Data.SideCollisionCheckerLength, Color.red);
        return Physics.Raycast(spawnPos, Vector3.right * direction, StateBus.Player_Data.SideCollisionCheckerLength, StateBus.Player_WhatIsObstacle, QueryTriggerInteraction.Ignore);
    }

    private static float CurrentLineCoordinate()
    {
        switch (StateBus.Player_CurrentLine)
        {
            case -1:
                return StateBus.Treadmill_LeftLineCoordinate;
            case 0:
                return StateBus.Treadmill_MiddleLineCoordinate;
            case 1:
                return StateBus.Treadmill_RightLineCoordinate;
            default:
                throw new System.Exception("Incorrect line value");
        }
    }
}
