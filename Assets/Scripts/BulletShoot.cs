using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bullet;
    private GameObject bulletClone;
    public float bulletDamage = 10f;
    public float bulletDestroyTime = 10f;

    private void Start()
    {
        bullet = (GameObject)Resources.Load("Prefabs/Bullet");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bulletClone = Instantiate(bullet, transform.position, transform.rotation);
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(0, 0, bulletSpeed);

            Destroy(bulletClone, bulletDestroyTime);
        }
    }
}
