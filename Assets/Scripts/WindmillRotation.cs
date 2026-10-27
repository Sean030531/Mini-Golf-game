using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Rotation speed, adjustable from the Unity Editor

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the windmill blades
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
