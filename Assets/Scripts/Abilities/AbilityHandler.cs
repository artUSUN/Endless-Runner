using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    private void Update()
    {
        StateBus.Boost_TimeFromLastCatchedUp += Time.deltaTime;
        if (StateBus.Boost.Value != null)
        {
            ResetCounter();
        }
    }

    private void ResetCounter()
    {
        StateBus.Boost_TimeFromLastCatchedUp = 0;
    }
}
