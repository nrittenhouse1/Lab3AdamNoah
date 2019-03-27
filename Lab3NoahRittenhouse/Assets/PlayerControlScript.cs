using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    float movementSpeed;
    float rotationSpeed;
    float jumpForce;

    bool grounded;

    public GameObject projectilePrefab;

    public GameObject projectileSpawnPoint;

    float shootTimer;

    public bool player1;

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        movementSpeed = 0.2f;
        rotationSpeed = 2.0f;
        shootTimer = 0.4f;
        jumpForce = 500.0f;

        if(!player1 && GameTracker.numPlayers <= 1)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if((Input.GetAxis("Axis10") > 0.1f && shootTimer <= 0 && player1) || (Input.GetAxis("Axis10-2") > 0.1f && shootTimer <= 0 && !player1))
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
            projectile.GetComponent<ProjectileScript>().team1 = true;
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
            shootTimer = 0.4f;
        }

        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetAxis("Axis2") > 0.05f && player1) || (Input.GetAxis("Axis2-2") > 0.05f && grounded && !player1))
        {
            transform.Translate(Vector3.forward * -movementSpeed);
        }
        else if ((Input.GetAxis("Axis2") < -0.05f && player1) || (Input.GetAxis("Axis2-2") < -0.05f && grounded && !player1))
        {
            transform.Translate(Vector3.forward * movementSpeed);
        }

        if ((Input.GetAxis("Axis1") < -0.05f && player1) || (Input.GetAxis("Axis1-2") < -0.05f && grounded && !player1))
        {
            transform.Translate(Vector3.right * -movementSpeed);
        }
        else if ((Input.GetAxis("Axis1") > 0.05f && player1 ) || (Input.GetAxis("Axis1-2") > 0.05f && grounded && !player1))
        {
            transform.Translate(Vector3.right * movementSpeed);
        }

        if ((Input.GetAxis("Axis4") < -0.05f && player1) || (Input.GetAxis("Axis4-2") < -0.05f && grounded && !player1))
        {
            transform.Rotate(Vector3.up * -rotationSpeed);
        }
        else if ((Input.GetAxis("Axis4") > 0.05f && player1 || (Input.GetAxis("Axis4-2")) > 0.05f && grounded && !player1))
        {
            transform.Rotate(Vector3.up * rotationSpeed);
        }

        if ((Input.GetAxis("Button0") > 0.1f && grounded && player1) || (Input.GetAxis("Button0-2") > 0.1f && grounded && !player1))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            grounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }
}
