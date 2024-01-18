using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PoolManager.instacne.DeSpawnFromPool(other.gameObject);
    }
}
