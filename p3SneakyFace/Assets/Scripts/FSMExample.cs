using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Video;

public class FSMExample : MonoBehaviour
{
    public string AIState = "Idle";
    public float aiSenseRadius;
    public Vector3 targetPosition;
    public float health;
    public float healthCutOff;
    public float moveSpeed;
    public float rotationSpeed;
    public float healingRate;
    public float maxHealth;
    public float hearingDistance; //how far the object can hear from
    public float fieldOfView=90f;
    public float visionMaxDistance = 4f;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        DestroyAllEnemies();
        


            if (AIState == "Idle")
        {
            //Do behaviour
            Idle();
            //Checks for transition
            //  if (Vector3.Distance(transform.position, targetPosition) < aiSenseRadius)
            //   {
            //       ChangeState("Seek");
            //  }

            if (GameManager.Instance.player != null)
            {
                if (CanSee(GameManager.Instance.player) == true)

                {
                    Debug.LogWarning("I can See You");
                    ChangeState("Seek");
                }
                if (CanHear(GameManager.Instance.player) == true)
                {
                    Debug.LogWarning("I Can Hear You");
                    ChangeState("Seek");
                }
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
            if (CanSee(GameManager.Instance.player) == false)
            {
                ChangeState("Idle");
            }

            if (CanHear(GameManager.Instance.player) == false)
            {
                ChangeState("Idle");
            }
           // else if (Vector3.Distance(transform.position, targetPosition) >= aiSenseRadius)
           //   {
           ///       ChangeState ("Idle");
            //  }
           


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
        //targetPosition = GameManager.Instance.player.transform.position;
        if (GameManager.Instance.player != null)
        {
            targetPosition = GameManager.Instance.player.transform.position;
        }
       
        Vector3 vectorToTarget = targetPosition - transform.position;
       //targetPosition = GameManager.Instance.player.transform.position;//TODO:Check for errors
        Vector3 directionToLook = targetPosition - transform.position;
        transform.right = directionToLook;
       // transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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

         float angleToTarget = Vector3.Angle(vectorToTarget, transform.right); //TODO: FIX this if the orientation does not match with sprite

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

    private void DestroyAllEnemies()
    {
        if (GameManager.Instance.player == null)
        {
            Destroy(this.gameObject);
        }
    }
   


}

