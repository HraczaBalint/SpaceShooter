using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship
{
    public GameObject shipPrefab;
    public string name;
    public string shipClass;
    public bool selected;

    public float slideSpeed;
    public float movementSpeed;
    public float maxMovementSpeed;
    public float minMovementSpeed;
    public float lastMovementSpeed;
    public float boostSpeed;
    public float acceleration;
    public float boostAcceleration;
    public float deceleration;
    public float turnTime;
    public float rollSpeed;
    public bool boostReleased;
    public float ftlChargeTime;
    public float ftlBurstTime;
    public float ftlSpeed;
    public Vector3 slideDirection;
    public Vector3 turnDirection;
    public Quaternion targetCamera;
    public float x;
    public float y;

    [HideInInspector]
    public ShipMovement shipMovement;
}
