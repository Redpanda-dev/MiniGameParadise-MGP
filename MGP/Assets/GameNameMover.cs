using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNameMover : MonoBehaviour
{
    public float speed = 1.0f;
     public float maxRotation = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        MoveItem();
    }

    void MoveItem()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, maxRotation * Mathf.Sin(Time.time * speed));
    }
}
