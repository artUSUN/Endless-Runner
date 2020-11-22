using System;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private float RotationPerSecond = 1;
    private int direction;

    private void Start()
    {
        if (UnityEngine.Random.value < 0.5f) direction = -1;
        else direction = 1;
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond * direction);
    }
}