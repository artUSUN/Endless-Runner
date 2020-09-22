using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMagnet : MonoBehaviour
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
        if (StateBus.GlobalState_GameOver && isActive) psEffect.Stop(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && isActive) StartCoroutine(ComeHereCoin(other.transform));
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(StateBus.Player_Data.MagnetDuration);
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
