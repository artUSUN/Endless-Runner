using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float magnitude = 1f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float speed = 1f;

    public Vector3 DistantionToPlayer { get { return delta; } }

    private Transform camTransform;
    private Vector3 delta;
    private Vector3 defaultDelta;

    private void Start()
    {
        StateBus.Camera_Script = GetComponent<CameraHandler>();

        camTransform = transform;

        delta = camTransform.position - StateBus.Player_Transform.position;
        defaultDelta = delta;
    }

    private void Update()
    {
        if (StateBus.Camera_Shake) StartCoroutine(Shake());

        FollowPlayer();
    }

    public void SetDelta_Z(float value)
    {
        delta = new Vector3(delta.x, delta.y, value);
    }

    public void SetDeltaDefault()
    {
        delta = defaultDelta;
    }

    private void FollowPlayer()
    {
        camTransform.position = Vector3.Lerp(camTransform.position, StateBus.Player_Transform.position + delta, speed * Time.deltaTime);
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
