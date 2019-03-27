using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonScript : MonoBehaviour
{
    int damage;

    int hp;

    void Start()
    {
        damage = 2;
        hp = 2;
    }
    
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {

    }

    public void TakeDamage()
    {
        if(hp > 1)
        {
            hp -= 1;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
