using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    public TMP_Text victory;
    public Button restart;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("duckling"))
        {
            victory.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
        }
    }
}
