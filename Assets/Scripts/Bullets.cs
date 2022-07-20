using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullets : MonoBehaviour
{
 
    Rigidbody rb;
    public int damage;
    

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }
    public void Attack(Vector3 dir, float force)
    {
        rb.velocity = dir * force;
        Invoke("DestroyBullet", 3);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //decrease the health points of enemy
            other.gameObject.GetComponent<Enemy>().DecreaseHealth(damage);
            Destroy(gameObject);

        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }



}
