using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioSource shipSource;
    public AudioSource otherSource;

    public AudioClip ftlChargeUp;
    public AudioClip ftlChargeDown;
    public AudioClip ftlJump;
    public AudioClip ftlFinish;

    public AudioClip gasLeak;

    public AudioClip backgroundNoise;

    private void Start()
    {
        backgroundSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        shipSource = GameObject.Find("Exhaust Outlet").GetComponent<AudioSource>();

        backgroundSource.clip = backgroundNoise;
        backgroundSource.Play();
        backgroundSource.loop = true;
    }
}
