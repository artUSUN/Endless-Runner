using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float magnitude = 1f;
    [SerializeField] private float duration = 1f;

    private void Update()
    {
        if (StateBus.Camera_Shake) StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }
}
