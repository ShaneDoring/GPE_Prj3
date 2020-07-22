using System.Collections;
using System.Collections.Generic;
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
}
