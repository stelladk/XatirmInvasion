using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + " collided with " + collision.gameObject.name);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"{this.name} triggered by {collider.gameObject.name}");
    }
}
