using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
AudioHandler _audioHandler;
void Start()
{
    _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
}
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0f) {
            if(other.gameObject.tag == "Player")
            {
                _audioHandler.PlaySpikeSound();
                GameObject.Find("Destroyer").GetComponent<GarbageCollector>().LoseGame();
                Destroy(other.gameObject);
            }
        }
    }

}
