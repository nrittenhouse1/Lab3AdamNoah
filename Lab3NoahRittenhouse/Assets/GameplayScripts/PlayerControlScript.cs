using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
    GameTracker gcInstance;
    float movementSpeed;
    float rotationSpeed;
    float jumpForce;

    bool grounded;

    public GameObject projectilePrefab;

    public GameObject projectileSpawnPoint;

    public GameObject healthDisplay, onePlayerHealthDisplay;
    public GameObject OnePlayerHud, TwoPlayerHud;

    float shootTimer;

    public bool player1;
    public int health;

    void Start()
    {
        gcInstance = GameTracker.gcInstance;
        grounded = false;
        movementSpeed = 0.2f;
        rotationSpeed = 2.0f;
        shootTimer = 0.4f;
        jumpForce = 500.0f;
        health = 20;
        if(!player1 && gcInstance.numPlayers <= 1)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(gcInstance.numPlayers < 2)//If only one player
        {
            TwoPlayerHud.SetActive(false);
            onePlayerHealthDisplay.GetComponent<Text>().text = health.ToString();
        }
        else//if two or three players
        {
            OnePlayerHud.SetActive(false);
            healthDisplay.GetComponent<Text>().text = health.ToString();

        }
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
        if(health <= 0)
        {
            gcInstance.numTeam1--;
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetAxis("Axis2") > 0.05f && player1) || (Input.GetAxis("Axis2-2") > 0.05f && !player1))
        {
            transform.Translate(Vector3.forward * -movementSpeed);
        }
        else if ((Input.GetAxis("Axis2") < -0.05f && player1) || (Input.GetAxis("Axis2-2") < -0.05f && !player1))
        {
            transform.Translate(Vector3.forward * movementSpeed);
        }

        if ((Input.GetAxis("Axis1") < -0.05f && player1) || (Input.GetAxis("Axis1-2") < -0.05f && !player1))
        {
            transform.Translate(Vector3.right * -movementSpeed);
        }
        else if ((Input.GetAxis("Axis1") > 0.05f && player1 ) || (Input.GetAxis("Axis1-2") > 0.05f && !player1))
        {
            transform.Translate(Vector3.right * movementSpeed);
        }

        if ((Input.GetAxis("Axis4") < -0.05f && player1) || (Input.GetAxis("Axis4-2") < -0.05f && !player1))
        {
            transform.Rotate(Vector3.up * -rotationSpeed);
        }
        else if ((Input.GetAxis("Axis4") > 0.05f && player1 || (Input.GetAxis("Axis4-2")) > 0.05f && !player1))
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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
