using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody bullet;
    public float bulletDamage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Rigidbody clone = Instantiate(bullet, transform.position, transform.rotation);
            clone.velocity = transform.TransformDirection(0, 0, bulletSpeed);

            Destroy(clone, 10f);
        }
    }
}
