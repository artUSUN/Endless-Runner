using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIncreaser : MonoBehaviour
{
    [SerializeField] private float startingCoefficient = 2f;
    [SerializeField] private float timeBetweenIncreaseInSec = 15f;
    [SerializeField] private float powerOfIncrease = 0.1f;

    private void Start()
    {
        StateBus.World_DifficultyCoefficient = startingCoefficient;
        StartCoroutine(IncreaseCycle());
    }

    private void Update()
    {
        if (StateBus.World_DifficultyChanged) Increase(powerOfIncrease);
    }

    private IEnumerator IncreaseCycle()
    {
        yield return new WaitForSeconds(timeBetweenIncreaseInSec);
        StateBus.World_DifficultyChanged += true;
        StartCoroutine(IncreaseCycle());
    }

    private void Increase(float value)
    {
        StateBus.World_DifficultyCoefficient += value;
        Debug.Log("Difficulty raised. Current value: " + StateBus.World_DifficultyCoefficient);
    }
}
