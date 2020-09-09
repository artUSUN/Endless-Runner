using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldRotator : MonoBehaviour
{
    [SerializeField] private float startRotationSpeed = 1f;

    private Transform world;

    private void Start()
    {
        world = transform;
        StateBus.World_IsGameActive = true;
    }

    private void Update()
    {
        if (StateBus.World_IsGameActive) Rotate();
    }

    private void Rotate()
    {
        world.Rotate(Vector3.left * startRotationSpeed * StateBus.World_DifficultyCoefficient * Time.deltaTime);
    }
}
