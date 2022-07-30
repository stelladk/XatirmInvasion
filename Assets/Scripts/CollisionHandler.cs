using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{   
    [Tooltip("Explosion particle system effect")]
    [SerializeField] GameObject explosionVFX;
    [Tooltip("Delay of scene reload after crash")]
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip[] AudioSequence;
    [SerializeField] LevelLoader levelLoader;

    SpaceshipController movement;
    SoundManager soundManager;

    bool isTransitioning = false;

    void Awake()
    {
        movement = GetComponent<SpaceshipController>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if(isTransitioning) return;
    //     Debug.Log($"{this.name} collided with {collision.gameObject.name}");
    //     crashSequence();
    // }

    void OnTriggerEnter(Collider collider)
    {
        if(isTransitioning) return;
        Debug.Log($"{this.name} triggered by {collider.gameObject.name}");
        crashSequence();
    }

    void crashSequence()
    {
        isTransitioning = true;
        Invoke("reloadScene", loadDelay);
        movement.enabled = false;
        // explosionVFX.Play();
        GameObject vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = gameObject.transform;
        soundManager.PlaySound(AudioSequence);
    }

    void reloadScene()
    {
        levelLoader.ReloadScene();
    }
}
