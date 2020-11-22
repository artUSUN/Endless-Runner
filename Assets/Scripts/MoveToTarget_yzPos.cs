using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget_yzPos : MonoBehaviour
{
    [SerializeField] private float speedY = 1f;
    [SerializeField] private float speedZ = 1f;
    [SerializeField] private Transform target;
    private Transform currentObject;

    private void Start()
    {
        if (target == null) Debug.LogError("Target is null");
        currentObject = transform;
    }

    private void Update()
    {
        Vector3 newPos = new Vector3(currentObject.position.x, 
            Mathf.Lerp(currentObject.position.y, target.position.y, speedY * Time.deltaTime), 
            Mathf.Lerp(currentObject.position.z, target.position.z, speedZ * Time.deltaTime));
        currentObject.position = newPos;
    }
}
