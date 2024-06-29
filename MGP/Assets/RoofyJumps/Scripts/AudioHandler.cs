using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip normalJump;
    [SerializeField] AudioClip spikes;
    [SerializeField] AudioClip crumble;
    [SerializeField] AudioClip boing;
    [SerializeField] AudioClip fallDown;
    [SerializeField] AudioClip victorySound;

    public GameObject audioObject;
    AudioSource audioSource;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        SetAudioObject();
    }

    private void SetAudioObject()
    {
        if(audioSource != null) { return; }
        audioObject = GameObject.FindGameObjectWithTag("SoundEffects");
        audioSource = audioObject.GetComponent<AudioSource>();
    }

    public void PlayNormalJumpSound()
    {
        if(PlayerPrefs.GetInt("isSound") == 0){ return; }
        SetAudioObject();
        audioSource.Stop();
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = 0.6f;
        audioSource.clip = normalJump;
        audioSource.Play();
    }

    public void PlayCrumbleSound()
    {
        if(PlayerPrefs.GetInt("isSound") == 0){ return; }
        SetAudioObject();
        audioSource.Stop();
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = 0.6f;
        audioSource.clip = crumble;
        audioSource.Play();
    }

    public void PlaySpikeSound()
    {
        if(PlayerPrefs.GetInt("isSound") == 0){ return; }
        SetAudioObject();
        audioSource.Stop();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.volume = 0.3f;
        audioSource.clip = spikes;
        audioSource.Play();
    }

    public void PlayBoingSound()
    {
        if(PlayerPrefs.GetInt("isSound") == 0){ return; }
        SetAudioObject();
        audioSource.Stop();
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = 0.6f;
        audioSource.clip = boing;
        audioSource.Play();
    }

    public void PlayFallingSound()
    {
        if(PlayerPrefs.GetInt("isSound") == 0){ return; }
        SetAudioObject();
        audioSource.Stop();
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = 0.4f;
        audioSource.clip = fallDown;
        audioSource.Play();
    }
    public void PlayVictorySound()
    {
        if(PlayerPrefs.GetInt("isSound") == 0){ return; }
        SetAudioObject();
        audioSource.Stop();
        audioSource.pitch = Random.Range(0.6f, 1.4f);
        audioSource.volume = 0.4f;
        audioSource.clip = victorySound;
        audioSource.Play();
    }

}
