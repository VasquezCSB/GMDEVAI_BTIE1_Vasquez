using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoat : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("boat"))
        {
            Destroy(collider.gameObject);
        }
    }
}
