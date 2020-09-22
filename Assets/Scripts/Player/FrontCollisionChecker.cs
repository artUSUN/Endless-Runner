using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(SphereCollider))]
public class FrontCollisionChecker : MonoBehaviour
{
    private CapsuleCollider highCollider;
    private SphereCollider lowCollider;
    private bool isHighColliderEnabled;

    private void Start()
    {
        highCollider = GetComponent<CapsuleCollider>();
        lowCollider = GetComponent<SphereCollider>();
        isHighColliderEnabled = true;
    }

    private void Update()
    {
        if (StateBus.Player_DisableHighCollider) isHighColliderEnabled = false;
        if (StateBus.Player_EnableHighCollider) isHighColliderEnabled = true;

        if (IsCollideWithObstacle())
        {
            StateBus.GlobalState_GameOver += true;
            Debug.Log("Game Over");
            gameObject.SetActive(false);
        }
    }

    private bool IsCollideWithObstacle()
    {
        Vector3 lowColliderGlobalCoordinate = lowCollider.transform.position + lowCollider.center;
        Vector3 highColliderGlobalCoordinate = highCollider.transform.position + highCollider.center;

        DebugDraw(lowColliderGlobalCoordinate, lowCollider);
        if (isHighColliderEnabled) DebugDraw(highColliderGlobalCoordinate, highCollider);

        if (Physics.CheckSphere(lowColliderGlobalCoordinate, lowCollider.radius, StateBus.Player_WhatIsObstacle, QueryTriggerInteraction.Ignore)) return true;
        else if (isHighColliderEnabled && Physics.CheckCapsule(highColliderGlobalCoordinate + Vector3.up * highCollider.radius, highColliderGlobalCoordinate - Vector3.up * highCollider.radius,
                                          highCollider.radius, StateBus.Player_WhatIsObstacle, QueryTriggerInteraction.Ignore)) return true;
        else return false;
    }

    private void DebugDraw(Vector3 center, SphereCollider sphere)
    {
        Debug.DrawLine(center + Vector3.up * sphere.radius, center - Vector3.up * sphere.radius);
        Debug.DrawLine(center + Vector3.forward * sphere.radius, center - Vector3.forward * sphere.radius);
        Debug.DrawLine(center + Vector3.right * sphere.radius, center - Vector3.right * sphere.radius);
    }

    private void DebugDraw(Vector3 center, CapsuleCollider capsule)
    {
        Debug.DrawLine(center + Vector3.up * capsule.radius, center - Vector3.up * capsule.radius);
        //Debug.DrawLine(center + Vector3.forward * capsule.radius, capsule.transform.position - Vector3.forward * capsule.radius);
        //Debug.DrawLine(center + Vector3.right * capsule.radius, capsule.transform.position - Vector3.right * capsule.radius);
    }

}
