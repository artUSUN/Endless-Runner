using System;
using System.Collections;
using UnityEngine;

public class SideClashHandler : MonoBehaviour
{
    private bool isNeedToLose = false;
    private ParticleSystem stunStarsFX;

    private void Start()
    {
        stunStarsFX = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (StateBus.Player_SideClash) CheckClashState();
        if (StateBus.GlobalState_GameOver) gameObject.SetActive(false);
    }

    private void CheckClashState()
    {
        if (isNeedToLose)
        {
            StopAllCoroutines();
            StateBus.GlobalState_GameOver += true;
        }
        else
        {
            isNeedToLose = true;

            StartCoroutine(Actions.ReturnBackToLine(StateBus.Player_Data.Rigidbody));

            StartCoroutine(StartStunState());
        }
    }

    private IEnumerator StartStunState()
    {
        StateBus.Camera_Shake += true;
        stunStarsFX.Play();
        //play sound
        StateBus.Pursuer_CatchUp += true;

        yield return new WaitForSeconds(StateBus.Player_Data.SideClashRecovery);
        StopStunState();
    }

    private void StopStunState()
    {
        isNeedToLose = false;
        stunStarsFX.Stop();
        //stop sound
    }

   
}
