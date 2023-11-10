using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 1;
    private GameObject focalPoint;

    private bool hasPowerup;
    private float powerupStrength = 15;

    public GameObject powerupIndicator;

    public Projectile projectile;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed *  forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine("PowerupCountdownRoutine");

            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                Instantiate(projectile.gameObject, transform.position + (enemy.transform.position - projectile.transform.position).normalized * 1, projectile.transform.rotation);
                projectile.enemy = enemy.gameObject;
                projectile.GetComponent<Rigidbody>().AddForce((enemy.transform.position - projectile.transform.position).normalized * 10, ForceMode.Impulse);
            }

        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerupIndicator.gameObject.SetActive(false);
        hasPowerup = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to: " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);


        }
    }
}
