using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private GameObject startChunk;
    [SerializeField] private GameObject[] chunkPrefabs;

    private Transform world;
    private List<GameObject> spawnedChunks = new List<GameObject>();

    //задел на разные длины чанков
    private readonly float degreeOfRotationOfChunk = 5f;
    private readonly float destroyAngle = 345f;
    private readonly float spawnAngle = 20f;

    private void Start()
    {
        world = transform;
        
        SpawnChunk(startChunk);
        SpawnChunk(GetRandomChunk());
    }

    private void Update()
    {
        if (spawnedChunks[spawnedChunks.Count - 1].transform.eulerAngles.x < spawnAngle)
        {
            SpawnChunk(GetRandomChunk());
        }

        if (spawnedChunks[0].transform.eulerAngles.x < destroyAngle)
        {
            DestroyOldestChunk();
        }
    }

    private void SpawnChunk(GameObject chunkPrefab)
    {
        GameObject newChunk = Instantiate(chunkPrefab, world.position, Quaternion.Euler(-1, 0, 0), world);

        if (spawnedChunks.Count > 0) newChunk.transform.rotation = spawnedChunks[spawnedChunks.Count - 1].transform.rotation * Quaternion.Euler(degreeOfRotationOfChunk, 0f, 0f);

        spawnedChunks.Add(newChunk);
    }

    private void DestroyOldestChunk()
    {
        Destroy(spawnedChunks[0].gameObject);
        spawnedChunks.RemoveAt(0);
    }

    private GameObject GetRandomChunk()
    {
        return chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
    }
}
