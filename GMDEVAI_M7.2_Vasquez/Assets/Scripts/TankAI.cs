using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject bullet;
    public GameObject turret;
    private Damage damage;


    public GameObject GetPlayer() { return player; }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        damage = GetComponent<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        anim.SetFloat("health", damage.health);

        if(damage.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            damage.health -= damage.damageTaken;
        }
    }

    public void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }
    public void StopFiring()
    {
        CancelInvoke("Fire");

    }
    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);

    }

}
