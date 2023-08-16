using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrocAI : MonoBehaviour
{
    Animator anim;

    public GameObject food;

    public GameObject GetFood() { 
        return food; 

    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, food.transform.position));
    }
}
