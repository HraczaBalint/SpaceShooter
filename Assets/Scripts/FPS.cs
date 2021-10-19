using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private float counter;

    private float frames;

    private float fps;

    void Update()
    {
        if (counter >= 1.0f)
        {
            fps = frames / counter;

            counter = 0.0f;
            frames = 0;
        }



    }

    // Megjelenítés hiányzik
}
