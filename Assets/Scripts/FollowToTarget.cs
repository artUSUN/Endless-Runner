using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 distantion;

    private void Start()
    {
        distantion = target.position - transform.position;
    }

    private void Update()
    {
        transform.position = target.position - distantion;
    }
}
