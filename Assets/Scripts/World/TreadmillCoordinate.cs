using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillCoordinate : MonoBehaviour
{
    [SerializeField] private float treadmillWidth = 1f;

    private void Start()
    {
        StateBus.Treadmill_LineWidht = treadmillWidth;

        StateBus.Treadmill_MiddleLineCoordinate = transform.position.x;
        StateBus.Treadmill_LeftLineCoordinate = transform.position.x - treadmillWidth;
        StateBus.Treadmill_RightLineCoordinate = transform.position.x + treadmillWidth;
    }
}

