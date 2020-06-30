using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameWorldController : MonoBehaviour
{
    [SerializeField] private GameObject startChunk;
    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] public float gameFieldRotationSpeed = 1;

    private readonly List<GameObject> spawnedChunks = new List<GameObject>();
    private Transform centerOfGameField;

    [SerializeField] private Transform player;
    private PlayerController playerControllerScript;

    //задел на разные длины чанков
    private readonly float degreeOfRotationOfChunk = 5f;
    private readonly float destroyAngle = 355f;
    private readonly float spawnAngle = 20f;

    void Start()
    {
        playerControllerScript = player.GetComponent<PlayerController>();

        centerOfGameField = transform;

        spawnedChunks.Add(startChunk);

        SpawnNewChunk(chunkPrefabs[0]);
    }


    void Update()
    {
        GameFieldRotation();

        SpawnAndDestroyChunksLogic();
    }

    private void SpawnAndDestroyChunksLogic()
    {
        if (spawnedChunks[0].transform.eulerAngles.x < destroyAngle)
        {
            DestroyOldestChunk();
        }

        if (spawnedChunks[spawnedChunks.Count - 1].transform.eulerAngles.x < spawnAngle)
        {
            SpawnNewChunk(chunkPrefabs[1]);
        }

    }

    private void SpawnNewChunk(GameObject chunkPrefab)
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
        centerOfGameField.Rotate(Vector3.left * gameFieldRotationSpeed * playerControllerScript.gameDifficultyCoefficient * Time.deltaTime);
    }
}

