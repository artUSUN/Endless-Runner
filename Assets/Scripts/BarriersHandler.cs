using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriersHandler : MonoBehaviour
{
    [SerializeField] private Transform gameBarriers;
    [SerializeField] private Transform gameOverBarriers;

    private void Start()
    {
        StateBus.World_BarriersHandler = transform.GetComponent<BarriersHandler>();
        SwitchToGameBarriers();
    }

    public void SwitchToGameOverBarriers()
    {
        gameOverBarriers.gameObject.SetActive(true);
        gameBarriers.gameObject.SetActive(false);
    }

    public void SwitchToGameBarriers()
    {
        gameBarriers.gameObject.SetActive(true);
        gameOverBarriers.gameObject.SetActive(false);
    }

}

