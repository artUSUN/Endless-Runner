using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Magnet_Ability : MonoBehaviour
{
    private bool isActive;
    private ParticleSystem psEffect;
    private Coroutine timer;

    private void Start()
    {
        psEffect = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (StateBus.Boost_Magnet)
        {
            if (isActive == false) isActive = true;
            else StopCoroutine(timer);

            psEffect.Play();
            //sound
            timer = StartCoroutine(Timer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && isActive) StartCoroutine(ComeHereCoin(other.transform));
    }

    private IEnumerator Timer()
    {
        Debug.Log("Magnet Ability ON.");
        DateTime dateTime = DateTime.Now;
        yield return new WaitForSeconds(StateBus.Player_Data.MagnetDuration);
        TimeSpan timeSpan = dateTime - DateTime.Now;
        Debug.Log("Magnet Ability OFF" + timeSpan);
        //sound off
        isActive = false;
        psEffect.Stop();
    }

    private IEnumerator ComeHereCoin(Transform coin)
    {
        do
        {
            Vector3 magnetZonePos = StateBus.Player_Transform.position + 0.5f * Vector3.up;
            float speed = StateBus.Player_Data.MagnetSpeedOfCoins * StateBus.World_DifficultyCoefficient * Time.deltaTime;
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, magnetZonePos, speed);
            yield return null;
            if (coin == null) break;

        } while (coin.transform.position != StateBus.Player_Transform.position + 0.5f * Vector3.up);
    }
}
