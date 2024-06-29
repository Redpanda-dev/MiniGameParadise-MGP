using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    public float minForceToBreak = 4f;
    public GameObject destructionEffect;
    AudioHandler _audioHandler;
    // Start is called before the first frame update
    void Start()
    {
        _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        CalculateForce();
    }

    private void CalculateForce()
    {
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Vector2 initialVelocity;

        //get velocity
        initialVelocity = rigidbody.velocity;


        //impulse = magnitude of change
        Vector2 result = initialVelocity;

        Debug.Log("Impulse taken: " + result.magnitude);
        if (result.magnitude > minForceToBreak)
        {
            BreakBlock();
        }
    }

    private void BreakBlock()
    {
        int breakCount = PlayerPrefs.GetInt("BreakCount", 0);
        int newBreakCount = breakCount+1;
        PlayerPrefs.SetInt("BreakCount", newBreakCount);
        _audioHandler.PlayCrumbleSound();
        Destroy(gameObject);
        GameObject explosion = Instantiate(destructionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 0.3f);
    }
}
