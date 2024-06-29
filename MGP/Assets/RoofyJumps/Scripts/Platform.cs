using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce = 10f;

    AudioHandler _audioHandler;

    void Start()
    {
        _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
    }

void OnCollisionEnter2D(Collision2D other)
{
    if (other.relativeVelocity.y <= 0f) {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
            {
                _audioHandler.PlayNormalJumpSound();
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
}

}
