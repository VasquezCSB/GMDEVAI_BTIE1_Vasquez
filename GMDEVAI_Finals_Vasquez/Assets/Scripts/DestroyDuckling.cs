using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestroyDuckling : MonoBehaviour
{
    public TMP_Text gameOver;
    public Button restart;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("duckling"))
        {
            Destroy(collider.gameObject);
            gameOver.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
        }
    }
}
