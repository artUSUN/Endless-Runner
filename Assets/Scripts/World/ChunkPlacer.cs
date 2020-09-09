using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject startChunk;
    [SerializeField] private GameObject[] chunkPrefabs;
    [Header("Spawn & destroy settings")]
    [SerializeField] private float lengthOfChunkInDegree = 5f;
    [SerializeField] private float destroyAngle = 345f;
    [SerializeField] private float spawnAngle = 20f;
    [SerializeField] private bool reverceY = false;

    private Transform parent;
    private readonly List<GameObject> spawnedChunks = new List<GameObject>();
    private Quaternion startRotation;

    private void Start()
    {
        parent = transform;
        startRotation = Quaternion.Euler(0f, parent.transform.rotation.eulerAngles.y, 0);

        SpawnChunk(startChunk);
        SpawnChunk(GetRandomChunk());
    }

    private void Update()
    {
        bool spawnCondition = Mathf.Round(spawnedChunks[spawnedChunks.Count - 1].transform.eulerAngles.x) < spawnAngle;
        if (reverceY) spawnCondition = Mathf.Round(spawnedChunks[spawnedChunks.Count - 1].transform.eulerAngles.x) > spawnAngle;

        if (spawnCondition)
        {
            SpawnChunk(GetRandomChunk());
        }

        if (Mathf.Round(spawnedChunks[0].transform.eulerAngles.x) == destroyAngle)
        {
            DestroyOldestChunk();
        }
    }

    private void SpawnChunk(GameObject chunkPrefab)
    {
        GameObject newChunk = Instantiate(chunkPrefab, parent.position + Vector3.right * 50f, startRotation, parent);

        if (spawnedChunks.Count > 0) newChunk.transform.rotation = spawnedChunks[spawnedChunks.Count - 1].transform.rotation * Quaternion.Euler(lengthOfChunkInDegree, 0f, 0f);

        newChunk.transform.position = parent.position;
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
