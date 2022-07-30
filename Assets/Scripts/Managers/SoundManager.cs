using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    Queue<AudioClip> clipQueue;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        clipQueue = new Queue<AudioClip>();
    }

    void Update()
    {
        if(!audioSource.isPlaying && clipQueue.Count > 0){
            audioSource.clip = clipQueue.Dequeue();
            audioSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        clipQueue.Enqueue(clip);
    }

    public void PlaySound(AudioClip[] clips)
    {
        foreach (AudioClip clip in clips)
        {
            PlaySound(clip);
        }
    }

    public bool isEmpty()
    {
        return (clipQueue.Count <= 0);
    }
}
