using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [Header("Knife Config")] 
    public GameObject knife;
    public Transform attackPoint;
    public Camera mainCamera;
    public float throwForce;
    public int maxAmount;
    public float throwableDamage;
    private int currentAmount;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetPoint;
    private Vector3 directionToTarget;

    // Start is called before the first frame update
    void Awake()
    {
        currentAmount = maxAmount;
        knife.GetComponent<collision_throwable>().throwable = this;
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        directionToTarget = targetPoint - attackPoint.position;
        
        if (Input.GetKeyDown(KeyCode.E) && currentAmount > 0)
        {
            GameObject gameobject = Instantiate(knife, attackPoint.position, Quaternion.identity);
            gameobject.transform.forward = directionToTarget.normalized;
            
            gameobject.GetComponent<Rigidbody>().AddForce(directionToTarget.normalized * throwForce, ForceMode.Impulse);
            gameobject.transform.eulerAngles = new Vector3(90, attackPoint.transform.eulerAngles.y, 0f);
            
            currentAmount--;
        }
    }

    public void addThrowable()
    {
        if (currentAmount < maxAmount)
        {
            currentAmount++;
        }
    }

    public bool isFull()
    {
        return currentAmount == maxAmount;
    }
    
}
