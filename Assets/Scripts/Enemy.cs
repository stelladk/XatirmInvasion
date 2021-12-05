using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] Transform parent;
    [SerializeField] int points = 10;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    
    void OnParticleCollision(GameObject other)
    {
        scoreBoard.increaseScore(points);
        destroyEnemy();
    }

    private void destroyEnemy()
    {
        GameObject vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
