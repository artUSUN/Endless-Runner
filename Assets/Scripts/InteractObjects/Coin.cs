using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject coinMesh;

    private bool isWorked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWorked)
        {
            particles.Play();
            coinMesh.SetActive(false);

            isWorked = true;
        }
    }
}
