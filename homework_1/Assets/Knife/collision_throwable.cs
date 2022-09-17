using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class collision_throwable : MonoBehaviour
{

    public Throwable throwable;
    private float throwableDamage;

    private void Start()
    {
        throwableDamage = throwable.throwableDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        Damageable check = other.GetComponent<Damageable>();
        if (check)
        {
            check.InflictDamage(throwableDamage, false, gameObject);
        }
    }
}
