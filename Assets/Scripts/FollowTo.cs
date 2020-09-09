using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTo : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Transform currentObject;
    private Vector3 delta;

    private void Start()
    {
        if (target == null) Debug.LogError("Target is null");
        currentObject = transform;

        delta = currentObject.position - target.position;
    }

    private void Update()
    {
        currentObject.position = target.position + delta;
    }
}
