using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.aspect = 9 / 16;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
