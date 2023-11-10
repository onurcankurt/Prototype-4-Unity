using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody projectileRb;
    public GameObject enemy;

    // Start is called before the first frame update

    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
        projectileRb.AddForce(lookDirection * speed);
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
