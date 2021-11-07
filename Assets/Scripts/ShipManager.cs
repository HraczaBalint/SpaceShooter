using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Ship[] ships;
    public static ShipManager instance;

    public GameObject spawn;
    public GameObject parent;

    private void Awake()
    {
        //CheckInstance();

        foreach (Ship s in ships)
        {
            s.shipMovement = gameObject.AddComponent<ShipMovement>();

            //s.shipMovement.slideSpeed = s.slideSpeed;
            //s.shipMovement.movementSpeed = s.movementSpeed;
            //s.shipMovement.maxMovementSpeed = s.maxMovementSpeed;
            //s.shipMovement.minMovementSpeed = s.minMovementSpeed;
            //s.shipMovement.lastMovementSpeed = s.lastMovementSpeed;
            //s.shipMovement.boostSpeed = s.boostSpeed;
            //s.shipMovement.acceleration = s.acceleration;
            //s.shipMovement.deceleration = s.deceleration;
            //s.shipMovement.boostAcceleration = s.boostAcceleration;
            //s.shipMovement.turnTime = s.turnTime;
            //s.shipMovement.rollSpeed = s.rollSpeed;
            //s.shipMovement.boostReleased = s.boostReleased;
            //s.shipMovement.ftlChargeTime = s.ftlChargeTime;
            //s.shipMovement.ftlBurstTime = s.ftlBurstTime;
            //s.shipMovement.ftlSpeed = s.ftlSpeed;

            //s.slideDirection = s.shipMovement.slideDirection;
            //s.turnDirection = s.shipMovement.turnDirection;
            //s.targetCamera = s.shipMovement.targetCamera;
            //s.x = s.shipMovement.x;
            //s.y = s.shipMovement.y;

        }
    }

    private void Start()
    {
        spawn = GameObject.Find("Spawn Point 1");
        parent = GameObject.Find("Player");

        foreach (Ship s in ships)
        {
            if (s.selected)
            {
                Instantiate(s.shipPrefab, parent.transform, false);
            }
        }
    }

    private void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
