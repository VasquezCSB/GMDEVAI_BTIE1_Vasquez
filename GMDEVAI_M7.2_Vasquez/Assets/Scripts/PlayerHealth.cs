using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider hpSlider;
    float progress;
    private Damage damage;

    private void Start()
    {
        damage = GetComponent<Damage>();
        
    }

    private void Update()
    {
        if (damage.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        damage.health -= damage.damageTaken;

        progress = damage.health / 100;

        hpSlider.value = progress;
    }
}
