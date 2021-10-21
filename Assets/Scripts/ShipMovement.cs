﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Sounds sounds;

    private Rigidbody rigidBody;
    private Transform playerCamera;
    private GameObject[] exhaustParticles;

    public float slideSpeed = 5f;
    public float movementSpeed = 0f;
    private float _lastMovementSpeed = 0f;
    private float _minMovementSpeed = 0f;
    public float maxMovementSpeed = 200f;
    public float boostSpeed = 300f;
    public float acceleration = 5f;
    public float boostAcceleration = 10f;
    public float deceleration = 1f;
    private bool _boostReleased;
    public float turnTime = 40f;
    private const float _rollSpeedMultiplier = 0.01f;
    public float rollSpeed = 25f;

    public float ftlChargeTime = 5f;
    private float _ftlChargeCountdown;
    public float ftlBurstTime = 3f;
    private float _ftlBurstCountdown;
    public float ftlSpeed = 1000f;
    private bool _ftlJumpStart;
    private bool _ftlBurstStart;
    private bool _ftlJumpComplete;

    private Vector3 slideDirection;
    private Vector3 turnDirection;

    private Quaternion targetCamera;

    private float x;
    private float y;

    private bool playedJumpSound = false;

    void Start()
    {
        Init();
        MouseLock();
    }

    void Update()
    {
        Ftl();
        Speed();
        BoostSpeed();
        Exhaust();

        if (!_ftlBurstStart)
        {
            Slide();
            Turn();
            Roll();
        }
    }

    private void Init()
    {
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerCamera = GameObject.Find("Player Camera").GetComponent<Transform>();
        exhaustParticles = GameObject.FindGameObjectsWithTag("Exhaust Particle");

        sounds = GameObject.Find("Audio Manager").GetComponent<Sounds>();
    }

    private void MouseLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void InputAxis()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    private void Ftl()
    {
        if (Input.GetKeyDown(KeyCode.J) && !_ftlJumpStart)
        {
            if (!sounds.shipSource.isPlaying)
            {
                sounds.shipSource.PlayOneShot(sounds.ftlChargeUp);

                _ftlJumpStart = true;
                _ftlJumpComplete = false;

                _ftlChargeCountdown = ftlChargeTime;
                _ftlBurstCountdown = ftlBurstTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.K) && _ftlJumpStart && !_ftlBurstStart)
        {
            if (sounds.shipSource.isPlaying)
            {
                sounds.shipSource.Stop();
                sounds.shipSource.PlayOneShot(sounds.ftlChargeDown);
            }
            _ftlJumpStart = false;
        }

        if (_ftlJumpStart && !_ftlJumpComplete)
        {
            _ftlChargeCountdown -= Time.deltaTime;

            if (_ftlChargeCountdown <= 0)
            {
                if (!playedJumpSound)
                {
                    sounds.shipSource.PlayOneShot(sounds.ftlJump);
                    playedJumpSound = true;
                }

                _ftlBurstStart = true;
                _ftlBurstCountdown -= Time.deltaTime;

                if (_ftlBurstCountdown > 0)
                {
                    rigidBody.AddForce(turnDirection * ftlSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                else
                {
                    sounds.shipSource.PlayOneShot(sounds.ftlFinish, 0.5f);
                    _ftlJumpComplete = true;
                    _ftlBurstStart = false;
                    _ftlJumpStart = false;
                }
            }
        }
    }

    private void Exhaust()
    {
        if (!_ftlBurstStart)
        {
            foreach (GameObject exhaustParticle in exhaustParticles)
            {
                exhaustParticle.GetComponent<ParticleSystem>().startLifetime = Mathf.Sqrt(movementSpeed / 500);
            }
        }
        else
        {
            foreach (GameObject exhaustParticle in exhaustParticles)
            {
                exhaustParticle.GetComponent<ParticleSystem>().startLifetime = 5f;
                exhaustParticle.GetComponent<ParticleSystem>().simulationSpace = ParticleSystemSimulationSpace.Local;
            }
        }
    }

    private void Roll()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rollSpeed * _rollSpeedMultiplier);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back, rollSpeed * _rollSpeedMultiplier);
        }
    }

    private void Turn()
    {
        if (!Input.GetKey(KeyCode.C))
        {
            targetCamera = Quaternion.Euler(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCamera, turnTime * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.H))
        {
            targetCamera = Quaternion.Euler(0f, transform.eulerAngles.y, 0f); // z --> 0 == auto kiegyenesítés
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCamera, turnTime * Time.deltaTime);
        }
    }

    private void Slide()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            InputAxis();
            slideDirection = transform.right * x + transform.up * y;
            rigidBody.AddForce(slideDirection.normalized * slideSpeed * Time.deltaTime);

            //sounds.shipSource.PlayOneShot(sounds.gasLeak, 0.1f);
        }
    }

    private void Speed()
    {
        turnDirection = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * Vector3.forward;

        rigidBody.AddForce(turnDirection * movementSpeed * Time.deltaTime);

        rigidBody.drag = (float)Math.Round(Math.Sqrt(rigidBody.velocity.magnitude / 500), 2);

        rigidBody.angularVelocity = Vector3.zero;

        if (rigidBody.velocity.magnitude < 1.5f && movementSpeed == 0f && slideDirection == Vector3.zero)
        {
            rigidBody.velocity = Vector3.zero;
        }

        if (rigidBody.velocity.magnitude > 100f && !_ftlBurstStart)
        {
            rigidBody.drag = 100f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !Input.GetKey(KeyCode.LeftControl) && movementSpeed < maxMovementSpeed)
        {
            movementSpeed += acceleration;

            if (movementSpeed >= maxMovementSpeed)
            {
                movementSpeed = maxMovementSpeed;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !Input.GetKey(KeyCode.LeftControl) && movementSpeed > _minMovementSpeed)
        {
            movementSpeed -= acceleration;

            if (movementSpeed <= _minMovementSpeed)
            {
                movementSpeed = _minMovementSpeed;
            }
        }
    }

    private void BoostSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _lastMovementSpeed = movementSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _boostReleased = false;

            movementSpeed += boostAcceleration;

            if (movementSpeed >= boostSpeed)
            {
                movementSpeed = boostSpeed;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _boostReleased = true;
        }

        if (_boostReleased)
        {
            if (movementSpeed > _lastMovementSpeed)
            {
                movementSpeed -= deceleration;
            }

            if (movementSpeed <= _lastMovementSpeed)
            {
                _boostReleased = false;
            }
        }
    }
}