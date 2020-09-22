using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleRBMoving : MonoBehaviour
{
    [SerializeField] private bool IsConstantForce = true;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed = 0;
    [SerializeField] private float periodTime = 0;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!IsConstantForce)
        {
            StartCoroutine(AddForceOnceAtTime(periodTime));
        }
    }

    private void Update()
    {
        if (IsConstantForce)
        {
            rb.AddForce(direction * speed * Time.deltaTime);
        }
    }

    private IEnumerator AddForceOnceAtTime(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(AddForceOnceAtTime(periodTime));
    }
}
