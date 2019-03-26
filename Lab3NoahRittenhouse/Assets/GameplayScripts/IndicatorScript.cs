using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    Color color;

    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        color = Color.white;
    }

    void Update()
    {
        color.a -= 0.01f;
        GetComponent<SpriteRenderer>().color = color;
        if(color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
