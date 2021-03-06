﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.CanController;

public class PhysicsObject : MonoBehaviour
{
    public float waitOnPickup = 0.2f;
    public float breakForce = 35f;
    [HideInInspector] public bool pickedUp = false;
    [HideInInspector] public PlayerInteractions playerInteractions;
    
   

    

    private void Start()
    {
        
       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if(collision.relativeVelocity.magnitude > breakForce)
            {
                playerInteractions.BreakConnection();
            }
        }
        
    }

    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;
    }

    
}
