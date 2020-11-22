using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Movement options")]
    [SerializeField] private float shiftDuration = 0.5f;
    [SerializeField] private float jumpPower = 1.1f;
    [Header("Side Clash options")]
    [SerializeField] private float sideClashRecovery = 5f;
    [Header("Magnet Ability options")]
    [SerializeField] private float magnetDuration = 10f;
    [SerializeField] private float magnetSpeedOfCoins = 1f;
    [Header("Double Points Ability options")]
    [SerializeField] private float doublePointsDuration = 10f;
    [Header("Jet Pack Fly Ability options")]
    [SerializeField] private float jetPackDuration = 10f;
    [SerializeField] private float jetPackJumpRaiseDuration = 3f;
    [SerializeField] private float jetPackJumpRaiseDistation = 3f;
    [Header("Other options")]
    [SerializeField] private float shiftAnimationAngleOfTurn = 60f;
    [SerializeField] private float returnSpeedIfFailShift = 0.1f;
    [SerializeField] private float sideCollisionCheckerLength = 0.1f;

    [SerializeField] private LayerMask whatIsObstacleForPlayer = 0;
    public float ShiftDuration { get { return shiftDuration; } }
    public float ShiftAnimationAngleOfTurn { get { return shiftAnimationAngleOfTurn; } }
    public float JumpPower { get { return jumpPower; } }
    public float ReturnSpeedIfFailShift { get { return returnSpeedIfFailShift; } }
    public float SideCollisionCheckerLength { get { return sideCollisionCheckerLength; } }
    public float MagnetDuration { get { return magnetDuration; } }
    public float MagnetSpeedOfCoins { get { return magnetSpeedOfCoins; } }
    public float SideClashRecovery { get { return sideClashRecovery; } }
    public float JetPackDuration { get { return jetPackDuration; } }
    public float JetPackJumpRaiseDuration { get { return jetPackJumpRaiseDuration; } }
    public float JetPackJumpRaiseDistation { get { return jetPackJumpRaiseDistation; } }
    public float DoublePointsDuration { get { return doublePointsDuration; } }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public SphereCollider Collider { get; private set; }
    public float RollAnimationDuration { get; private set; }

    private void Awake()
    {
        StateBus.Player_Transform = transform;
        StateBus.Player_Data = this;
        StateBus.Player_CurrentLine = 0;
        StateBus.Player_WhatIsObstacle = whatIsObstacleForPlayer;

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<SphereCollider>();
        RollAnimationDuration = Animations.FindAnimationInAnimator(Animator, "RunningRoll").length;
    }
}
