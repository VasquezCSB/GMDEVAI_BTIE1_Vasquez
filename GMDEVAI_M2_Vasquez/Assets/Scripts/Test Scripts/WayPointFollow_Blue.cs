using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollow_Blue : MonoBehaviour
{
    //public GameObject[] waypoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    int currentWaypointIndex = 0;

    float speed = 5;
    float rotSpeed = 3; //Rotation Speed
    float accuracy = 1; //Fixes the wobbly char problem


    // Start is called before the first frame update
    void Start()
    {
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (circuit.Waypoints.Length == 0) return;

        GameObject currentWayPoint = circuit.Waypoints[currentWaypointIndex].gameObject;

        Vector3 lookAtGoal = new Vector3(currentWayPoint.transform.position.x,
                                        this.transform.position.y,
                                        currentWayPoint.transform.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        if(direction.magnitude < 1.0f)
        {
            currentWaypointIndex++;

            if(currentWaypointIndex >= circuit.Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                Quaternion.LookRotation(direction),
                                Time.deltaTime * rotSpeed);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
