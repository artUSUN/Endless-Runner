using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenBoosts;
    [SerializeField] private GameObject[] boosts;
    [SerializeField] private Transform mesh;

    private void Start()
    {
        mesh.gameObject.SetActive(false);
        
        if (StateBus.Boost_TimeFromLastCatchedUp > timeBetweenBoosts)
        {
            Spawn(GetRandomBoost());
            StateBus.Boost_TimeFromLastCatchedUp = 0;
        }
    }

    private GameObject GetRandomBoost()
    {
        int randValue = Random.Range((int)0, (int)boosts.Length);
        return boosts[randValue];
    }

    private void Spawn(GameObject boost)
    {
        Instantiate(boost, transform.position, transform.rotation, transform);
    }
}
