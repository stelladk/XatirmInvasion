using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float loadDelay = 1f;

    SpaceshipController movement;

    bool isTransitioning = false;

    void Awake()
    {
        movement = GetComponent<SpaceshipController>();
    }

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
    }

    void reloadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
