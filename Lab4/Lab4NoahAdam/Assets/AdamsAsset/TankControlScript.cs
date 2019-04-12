using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControlScript : MonoBehaviour
{
    float movementSpeed;
    float rotationSpeed;
    float shootTimer;

    public GameObject projectilePrefab;

    public GameObject player1ProjectileSpawn;

    public enum PlayerNum
    {
        P1,
        P2,
        P3,
        P4
    }
    public PlayerNum playerNum;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 0.2f;
        rotationSpeed = 2.0f;
        shootTimer = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNum == PlayerNum.P1)
        {
            if ((Input.GetAxis("Axis10") > 0.1f && shootTimer <= 0))
            {
                GameObject projectile = Instantiate(projectilePrefab, player1ProjectileSpawn.transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
                shootTimer = 0.4f;
            }

            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
        }
        else if(playerNum == PlayerNum.P2)
        {
            if ((Input.GetAxis("Axis10-2") > 0.1f && shootTimer <= 0))
            {
                GameObject projectile = Instantiate(projectilePrefab, player1ProjectileSpawn.transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
                shootTimer = 0.4f;
            }

            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
        }
        else if (playerNum == PlayerNum.P3)
        {
            if ((Input.GetAxis("Axis10-3") > 0.1f && shootTimer <= 0))
            {
                GameObject projectile = Instantiate(projectilePrefab, player1ProjectileSpawn.transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
                shootTimer = 0.4f;
            }

            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
        }
        else if (playerNum == PlayerNum.P4)
        {
            if ((Input.GetAxis("Axis10-4") > 0.1f && shootTimer <= 0))
            {
                GameObject projectile = Instantiate(projectilePrefab, player1ProjectileSpawn.transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
                shootTimer = 0.4f;
            }

            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (playerNum == PlayerNum.P1)
        {
            if ((Input.GetAxis("Axis2") > 0.05f))
            {
                transform.Translate(Vector3.forward * -movementSpeed);
            }
            else if ((Input.GetAxis("Axis2") < -0.05f))
            {
                transform.Translate(Vector3.forward * movementSpeed);
            }

            if ((Input.GetAxis("Axis1") < -0.05f))
            {
                transform.Rotate(Vector3.up * -rotationSpeed);
            }
            else if ((Input.GetAxis("Axis1") > 0.05f))
            {
                transform.Rotate(Vector3.up * rotationSpeed);
            }
        }
        else if(playerNum == PlayerNum.P2)
        {
            if ((Input.GetAxis("Axis1-2") < -0.05f))
            {
                transform.Rotate(Vector3.up * -rotationSpeed);
            }
            else if ((Input.GetAxis("Axis1-2") > 0.05f))
            {
                transform.Rotate(Vector3.up * rotationSpeed);
            }
        }
        else if (playerNum == PlayerNum.P3)
        {
            if ((Input.GetAxis("Axis1-3") < -0.05f))
            {
                transform.Rotate(Vector3.up * -rotationSpeed);
            }
            else if ((Input.GetAxis("Axis1-3") > 0.05f))
            {
                transform.Rotate(Vector3.up * rotationSpeed);
            }

            if(transform.localEulerAngles.y >= 350 && transform.localEulerAngles.y <= 359)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 350, transform.localEulerAngles.z);
            }
            else if (transform.localEulerAngles.y <= 200 && transform.localEulerAngles.y >= 180)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 200, transform.localEulerAngles.z);
            }
        }
        else if (playerNum == PlayerNum.P4)
        {
            if ((Input.GetAxis("Axis1-4") < -0.05f))
            {
                transform.Rotate(Vector3.up * -rotationSpeed);
            }
            else if ((Input.GetAxis("Axis1-4") > 0.05f))
            {
                transform.Rotate(Vector3.up * rotationSpeed);
            }

            if (transform.localEulerAngles.y >= 160 && transform.localEulerAngles.y <= 260)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 160, transform.localEulerAngles.z);
            }
            else if (transform.localEulerAngles.y <= 10 && transform.localEulerAngles.y >= 0)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 10, transform.localEulerAngles.z);
            }
        }
    }
}
