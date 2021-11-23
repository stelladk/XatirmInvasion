using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [Tooltip("Explosion particle system effect")]
    [SerializeField] ParticleSystem explosionVFX;
    [Tooltip("Delay of scene reload after crash")]
    [SerializeField] float loadDelay = 1f;

    SpaceshipController movement;

    bool isTransitioning = false;

    void Awake()
    {
        movement = GetComponent<SpaceshipController>();
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
        explosionVFX.Play();
    }

    void reloadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
