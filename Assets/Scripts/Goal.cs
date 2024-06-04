using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameNotification.GameNotificationType type;
    public float multiplier;

    public bool isDynamic;
    public Vector2 bounds;
    public float speed;
    
    private bool reverce;

    private void Update()
    {
        if(!isDynamic)
            return;
            
        float x = transform.position.x;

        if (!reverce)
        {
            x += Time.deltaTime * speed;
        }
        else
        {
            x -= Time.deltaTime * speed;
        }

        if (x >= bounds.y)
            reverce = true;
        
        if (x <= bounds.x)
            reverce = false;
        
        
        transform.position = new Vector3(x, transform.position.y, 0);
    }

    private void OnValidate()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = multiplier.ToString();
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = multiplier.ToString();
    }
}
