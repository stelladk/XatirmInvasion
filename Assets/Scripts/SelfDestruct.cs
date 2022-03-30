using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float destructionTime = 3f;
    void Start()
    {
        Destroy(gameObject, destructionTime);
    }
}
