using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    float liveTimer;

    private void Start()
    {
        liveTimer = 2.0f;
    }

    private void Update()
    {
        if(liveTimer <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            liveTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Goon")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
