using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrashblockade : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("boat"))
        {
            Destroy(this.gameObject);
        }
    }
}
