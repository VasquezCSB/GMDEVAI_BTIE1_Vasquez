using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.90f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] boatwaypoints;
    GameObject currentNode;

    int currentWaypointIndex = 0;

    Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        boatwaypoints = wpManager.GetComponent<BoatWaypointMNGR>().waypoints;
        graph = wpManager.GetComponent<BoatWaypointMNGR>().graph;
        currentNode = boatwaypoints[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        //If close enough to current waypoint, move to the next one
        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).
            transform.position, transform.position) < accuracy)
        {

            currentWaypointIndex++;
        }

        //If we are not at the end of the path
        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;

            Vector3 lookAtGoal = new Vector3(goal.position.x,
                                        transform.position.y,
                                        goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                Quaternion.LookRotation(direction),
                                Time.deltaTime * rotSpeed);

            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void GoTo1stArea()
    {
        graph.AStar(currentNode, boatwaypoints[0]);
        currentWaypointIndex = 0;
    }

    public void GoTo2ndArea()
    {
        graph.AStar(currentNode, boatwaypoints[1]);
        currentWaypointIndex = 0;
    }
}
