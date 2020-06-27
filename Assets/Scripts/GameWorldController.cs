using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldController : MonoBehaviour
{
    public GameObject startChunk;
    [SerializeField] private Vector3 gameFieldRotationSpeed;

    [SerializeField] private GameObject[] chunkPrefabs;

    private List<GameObject> spawnedChunks = new List<GameObject>();
    private Transform centerOfGameField;
    private Transform player;

    void Start()
    {
        centerOfGameField = transform;
        player = GameObject.Find("Player").transform;
        spawnedChunks.Add(startChunk);
    }

    
    void Update()
    {
        GameFieldRotation();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnChunk(chunkPrefabs[0]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnChunk(chunkPrefabs[1]);
        }
    }

    private void SpawnChunk(GameObject chunkPrefab)
    {
        GameObject newChunk = Instantiate(chunkPrefab, centerOfGameField.position, Quaternion.identity, centerOfGameField);

        newChunk.transform.rotation = spawnedChunks[spawnedChunks.Count - 1].transform.rotation * Quaternion.Euler(5f, 0f, 0f);

        spawnedChunks.Add(newChunk);

        //newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        //newChunk.transform.Rotate()
    }

    private void GameFieldRotation()
    {
        centerOfGameField.Rotate(gameFieldRotationSpeed * Time.deltaTime);
    }
}

