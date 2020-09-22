using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget_zPos : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform target;
    private Transform currentObject;

    private void Start()
    {
        if (target == null) Debug.LogError("Target is null");
        currentObject = transform;
    }

    private void Update()
    {
        Vector3 newPos = new Vector3(currentObject.position.x, currentObject.position.y, Mathf.Lerp(currentObject.position.z, target.position.z, speed * Time.deltaTime));
        currentObject.position = newPos;
    }
}
