using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingFollow : MonoBehaviour
{
    public Transform goal;
    public float speed; //3
    public float rotSpeed; //3
    public float currentSpeed = 0;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x,
                            this.transform.position.y,
                            goal.position.z);

        Vector3 direction = lookAtGoal - transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                Quaternion.LookRotation(direction),
                                Time.deltaTime * rotSpeed);

        //transform.Translate(0, 0, transform.position.z);
        //currentSpeed = transform.position.z;

        if (Vector3.Distance(lookAtGoal, transform.position) > 1)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
