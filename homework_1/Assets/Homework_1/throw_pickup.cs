using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_pickup : MonoBehaviour
{
    [Header("Lootable Config")] 
    public Transform player;
    public Throwable throwable;
    public float pickUpRange;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (withinRange() && !throwable.isFull())
        {
            throwable.addThrowable();
            Destroy(gameObject);
        }
    }

    public bool withinRange()
    {
        Vector3 distance = player.position - transform.position;
        
        return distance.magnitude <= pickUpRange;
    }
}
