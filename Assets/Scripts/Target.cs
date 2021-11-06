using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    private BulletShoot bulletShoot;

    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    private GameObject fracturesClone;
    public float fracturesDestroyTime = 20f;

    private void Start()
    {
        bulletShoot = GameObject.Find("Gun").GetComponent<BulletShoot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ship")
        {
            FractureObject();
        }

        if (collision.collider.tag == "Projectile")
        {
            TakeDamage(bulletShoot.bulletDamage);
        }
    }

    public void FractureObject()
    {
        fracturesClone = Instantiate(fractured, transform.position, transform.rotation);
        fracturesClone.transform.localScale = transform.localScale;
        Destroy(gameObject);
        Destroy(fracturesClone, fracturesDestroyTime);
    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            FractureObject();
        }
    }
}
