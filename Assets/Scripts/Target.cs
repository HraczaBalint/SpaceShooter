using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    BulletShoot bulletShoot;
    public float fadeSpeed;

    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

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
        GameObject clone = Instantiate(fractured, transform.position, transform.rotation);
        clone.transform.localScale = transform.localScale;
        Destroy(gameObject);
        Destroy(clone, 20f);
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            FractureObject();
        }
    }
}
