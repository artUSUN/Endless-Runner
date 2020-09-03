using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private ParticleSystem TakenEffect;
    [SerializeField] private ParticleSystem FramingEffect;
    [SerializeField] private GameObject mesh;

    private bool isWorked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWorked)
        {
            StateBus.Boost_Magnet += true;
            TakenEffect.Play();
            FramingEffect.gameObject.SetActive(false);
            mesh.SetActive(false);
            isWorked = true;
        }
    }
}
