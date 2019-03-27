using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonScript : MonoBehaviour
{
    GameTracker gcInstance;
    int damage;
    int hp;
    float damageTimer;
    void Start()
    {
        gcInstance = GameTracker.gcInstance;
        damage = 1;
        hp = 2;
        damageTimer = 2;
    }

    void Update()
    {
        damageTimer -= Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Team1")
        {
            if (damageTimer <= 0)
            {
                other.gameObject.GetComponent<PlayerControlScript>().TakeDamage(damage);
                damageTimer = 2;
            }
        }
    }

    public void TakeDamage()
    {
        if (hp > 0)
        {
            hp -= 1;
        }
        else
        {
            gcInstance.numGoons--;
            gameObject.SetActive(false);
        }
    }
}
