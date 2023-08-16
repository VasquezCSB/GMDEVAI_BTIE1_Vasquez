using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIControl_Crow : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject duckling;

    public GameObject mamaDuck;

    public DucklingFollow preyMovement;

    public float enemyDistanceRun = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection);
    }

    void Pursue()
    {
        Vector3 targetDirection = duckling.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / (agent.speed + preyMovement.currentSpeed);

        Seek(duckling.transform.position + duckling.transform.forward * lookAhead);
    }

    bool canSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = mamaDuck.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            return raycastInfo.transform.gameObject.tag == "AI";
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, mamaDuck.transform.position);

        if (distance < enemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - mamaDuck.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;
        } else
        {
            Pursue();

        }
        /*
        if (canSeeTarget())
        {
            Flee(mamaDuck.transform.position);
        }
        else
        {
            Seek(duckling.transform.position);
        }
        */

        //Flee(mamaDuck.transform.position);
    }
}
