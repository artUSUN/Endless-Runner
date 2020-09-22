using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollChanger : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private GameObject animMesh;

    private void Start()
    {
        animMesh.SetActive(true);
        ragdoll.SetActive(false);
    }

    private void Update()
    {
        if (StateBus.Player_EnableRagdoll)
        {
            CopyTransformData(animMesh.transform, ragdoll.transform);
            animMesh.SetActive(false);
            ragdoll.SetActive(true);
        }
    }

    private void CopyTransformData(Transform sourceTransform, Transform destinationTransform)
    {
        if (sourceTransform.childCount != destinationTransform.childCount)
        {
            Debug.LogError("Invalid transform copy, they need to match transform hierarchies");
        }
        for (int i = 0; i < sourceTransform.childCount; i++)
        {
            Transform source = sourceTransform.GetChild(i).transform;
            Transform destination = destinationTransform.GetChild(i).transform;

            destination.position = source.position;
            destination.rotation = source.rotation;

            Rigidbody rb = destination.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = velocity;
            }

            CopyTransformData(source, destination);
        }

    }
}
