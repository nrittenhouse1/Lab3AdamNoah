using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneTwoScript : MonoBehaviour
{
    [SerializeField]
    int health, damage;
    float movementSpeed, iFrameTime;
    bool isInvincible;
    void Start()
    {
        health = 10;
        damage = 2;
        movementSpeed = 5;
        iFrameTime = 2;
    }

    void Update()
    {
        if(isInvincible)
        {
            iFrameTime -= Time.deltaTime;
        }
        if(iFrameTime <= 0)
        {
            iFrameTime = 2;
            isInvincible = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            isInvincible = true;
        }
    }
}
