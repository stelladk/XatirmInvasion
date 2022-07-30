using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int points = 10;
    [SerializeField] int health = 5;

    GameObject spawnParent;
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        spawnParent = GameObject.FindWithTag("RuntimeSpawn");
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    
    void OnParticleCollision(GameObject other)
    {
        processHit();
        if (health < 1){
            destroyEnemy();
        }
    }

    private void processHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = spawnParent.transform;
        health -= 1;
    }

    private void destroyEnemy()
    {
        if(scoreBoard != null) scoreBoard.increaseScore(points);
        GameObject vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = spawnParent.transform;
        Destroy(gameObject);
    }
}
