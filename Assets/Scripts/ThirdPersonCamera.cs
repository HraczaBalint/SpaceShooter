using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float _minAngleY = -89.9f;
    private const float _maxAngleY = 89.9f;

    private Transform ship;

    public float minDistance = 3f;
    public float maxDistance = 15f;
    public Vector3 offset = Vector3.zero;

    private float currentX;
    private float currentY;

    Quaternion rotation;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Scroll();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void Init()
    {
        ship = GameObject.Find("Player").transform.GetChild(0);
    }

    private void Rotate()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        //currentY = Mathf.Clamp(currentY, _minAngleY, _maxAngleY);
    }

    private void Scroll()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetAxis("Mouse ScrollWheel") > 0f && offset.z < -minDistance)
        {
            offset.z += 0.2f;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetAxis("Mouse ScrollWheel") < 0f && offset.z > -maxDistance)
        {
            offset.z -= 0.2f;
        }
    }

    private void MoveCamera()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Rotate();
            rotation = Quaternion.Euler(-currentY, currentX, 0f); // 0f == nem forog a kamera -- player.rotation.eulerAngles.z
            transform.rotation = rotation; 
        }
        
        transform.position = ship.position + rotation * offset;
    }
}