using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private Transform rotatableObject;
    public Vector3 rotationAngles = Vector3.zero;

    void Start()
    {
         rotatableObject = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        rotatableObject.Rotate(rotationAngles);
    }
}
