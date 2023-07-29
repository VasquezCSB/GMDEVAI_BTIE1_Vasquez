using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{

    NavMeshAgent agent;
    public GameObject target;
    public WASDMovement playerMovement;

    Vector3 wanderTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }

    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    private void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection); //fleeDirection
    }

    //Predicts where Police will go
    private void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);

        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    //predicts where police will go
    private void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    void Wander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 
                        0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void fleeWander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                        0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Flee(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if(spotDistance < distance)
            {
                chosenSpot= hidePosition;
                distance= spotDistance;
            }
        }

        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDir = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float rayDistance = 100.0f;
        hideCol.Raycast(back, out info, rayDistance);

        Seek(info.point + chosenDir.normalized * 5);
    }

    bool canSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            return raycastInfo.transform.gameObject.tag == "Player";
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canSeeTarget() == true)
        {
            fleeWander();
            Debug.Log("Can See");
        }
        else
        {
            Wander();
            Debug.Log("Cant See");
        }

    }
}
