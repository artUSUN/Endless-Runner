using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldController : MonoBehaviour
{
    [SerializeField] public GameObject startChunk;
    [SerializeField] private float gameFieldRotationSpeed = 1;
    [SerializeField] private GameObject[] chunkPrefabs;

    private List<GameObject> spawnedChunks = new List<GameObject>();
    private Transform centerOfGameField;

    //задел на разные длины чанков
    private readonly float degreeOfRotationOfChunk = 5f;
    private readonly float destroyAngle = 355f;
    private readonly float spawnAngle = 20f;

    void Start()
    {
        centerOfGameField = transform;
        spawnedChunks.Add(startChunk);

        SpawnChunk(chunkPrefabs[1]);
    }


    void Update()
    {
        GameFieldRotation();

        SpawndAndDestroyChunk();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(spawnedChunks[0].transform.eulerAngles.x);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnChunk(chunkPrefabs[0]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnChunk(chunkPrefabs[1]);
        }
    }

    private void SpawndAndDestroyChunk()
    {
        if (spawnedChunks[0].transform.eulerAngles.x < destroyAngle)
        {
            DestroyOldestChunk();
            SpawnChunk(chunkPrefabs[1]);
        }

        if (spawnedChunks[spawnedChunks.Count - 1].transform.eulerAngles.x < spawnAngle)
        {
            SpawnChunk(chunkPrefabs[1]);
        }

    }

    private void SpawnChunk(GameObject chunkPrefab)
    {
        GameObject newChunk = Instantiate(chunkPrefab, centerOfGameField.position, Quaternion.identity, centerOfGameField);

        newChunk.transform.rotation = spawnedChunks[spawnedChunks.Count - 1].transform.rotation * Quaternion.Euler(degreeOfRotationOfChunk, 0f, 0f);

        spawnedChunks.Add(newChunk);
    }

    private void DestroyOldestChunk()
    {
        Destroy(spawnedChunks[0].gameObject);
        spawnedChunks.RemoveAt(0);
    }
    

    private void GameFieldRotation()
    {
        centerOfGameField.Rotate(Vector3.left * gameFieldRotationSpeed * Time.deltaTime);
    }
}

