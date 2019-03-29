using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public bool team1;  //team1 is player 1 and 2

    float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }

        lifeTime -= Time.deltaTime;   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Team1" && !team1)
        {
            Debug.Log("Ouch Team1");
            gameObject.SetActive(false);
        }
        else if (collision.collider.gameObject.tag == "Team2" && team1)
        {
            Debug.Log("Ouch Team2");
            collision.collider.gameObject.GetComponent<GoonScript>().TakeDamage();
            gameObject.SetActive(false);

        }
    }


}
