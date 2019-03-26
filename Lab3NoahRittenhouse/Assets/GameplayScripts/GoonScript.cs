using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonScript : MonoBehaviour
{
    int damage;
    void Start()
    {
        damage = 2;
    }
    
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerOneTwoScript>().TakeDamage(damage);
        }
    }
}
