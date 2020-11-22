using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOCatchUpHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinCatchingFX;
    [SerializeField] private ParticleSystem ioCatchingFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCatchingFX.Play();
            //sound
            other.gameObject.SetActive(false);
            StateBus.Coin_Add += 1;
        }
        else if (other.CompareTag("InteractObject"))
        {
            ioCatchingFX.Play();
            //sound
            other.GetComponent<InteractObject>().WhenCatched();
        }
    }
}
