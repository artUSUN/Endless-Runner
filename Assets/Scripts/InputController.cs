using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Input input;

    private void Awake()
    {
        input = new Input();
        input.PlayMode.Jump.performed += context => StateBus.Input_Vertical += 1;
        input.PlayMode.Roll.performed += context => StateBus.Input_Vertical += -1;
        input.PlayMode.ShiftLeft.performed += context => StateBus.Input_Horizontal += -1;
        input.PlayMode.ShiftRight.performed += context => StateBus.Input_Horizontal += 1;

        //debug func:
        input.PlayMode.Test_IncreaseDifficulty.performed += context => StateBus.World_DifficultyChanged += true;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
