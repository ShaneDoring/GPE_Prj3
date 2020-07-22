﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FSMExample : MonoBehaviour
{
    public string AIState = "Idle";
    public float aiSenseRadius;
    public Vector3 targetPosition;
    public float health;
    public float healthCutOff;
    public float moveSpeed;
    public float healingRate;
    public float maxHealth;
    public float hearingDistance; //how far the object can hear from
    public float fieldOfView=90f;
    public float visionMaxDistance = 40f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            //Do behaviour
            Idle();
            //Checks for transition
            if(Vector3.Distance(transform.position,targetPosition)< aiSenseRadius)
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Seek")
        {
            // Do the Seek Behaviour
            Seek();
            //Check for transitions
            if (health < healthCutOff)
            {
                ChangeState("Rest");
            }
            else if(Vector3.Distance(transform.position,targetPosition)>= aiSenseRadius)
            {
                ChangeState ("Idle");
            }
        }
        else if (AIState == "Rest")
        {
            //Do the Rest behaviour
            Rest();
            //Check for transitions
            if (health >= healthCutOff)
            {
                ChangeState("Idle");
            }
        }
        else
        {
            Debug.LogWarning("AIState not found" + AIState);
        }
    }

    void Idle()
    {
        // Do nothing
    }

    void Seek()
    {
        
        Vector3 vectorToTarget = targetPosition - transform.position;
        transform.position += vectorToTarget.normalized * moveSpeed*Time.deltaTime;
    }

    void Rest()
    {
        //TODO Rest state
        health += healingRate * Time.deltaTime;
        health = Mathf.Min(health, maxHealth);
    }

    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    private bool CanHear(GameObject target)
    {
        //Get the target's Noise Maker
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        //if they do not have a Noise Maker we can not hear them
        if (targetNoiseMaker == null)
        {
            return false;
        }
        //if the distance between us and the target is less than the sum of the noise distance and the hearing distace we can hear it
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if((targetNoiseMaker.volumeDistance+hearingDistance)> distanceToTarget)
        {
            return true;
        }
        return false;
    }

    private bool CanSee(GameObject target)
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;

        float angleToTarget = Vector3.Angle(vectorToTarget, transform.up); 

        //Check if the target is within the field of view 
        if (angleToTarget < fieldOfView)
        {
            // Use a raycast to see if there are obstructions between us and the target.
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, vectorToTarget,visionMaxDistance);

            if (hitInfo.collider.gameObject == target)
            {
                return true;
            }
            
        }
        return false;
    }
}

