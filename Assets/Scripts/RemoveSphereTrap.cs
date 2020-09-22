using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSphereTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
