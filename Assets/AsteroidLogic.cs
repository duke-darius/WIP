using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

public class AsteroidLogic : MonoBehaviour
{

    public ShipLogic shipLogic;
    public GameObject ResourceSpawn;
    public float speed = 3;
    public bool IsMining = false;
    public static Particle[] particles = new Particle[1000];

    public DateTime LastResource = DateTime.MinValue;
    public float ResourceSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        shipLogic = GameObject.FindObjectOfType<ShipLogic>();

        var rigid = GetComponent<Rigidbody>();
        var vel = shipLogic.transform.position - transform.position;
        vel.AddRandom(Vector3.zero, new Vector3(20, 0, 20));
        rigid.AddForce(vel * speed);
        rigid.AddTorque(RandomVector(), ForceMode.Impulse);

        Destroy(gameObject, 45);
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.RandomRange(0, 100), Random.RandomRange(0, 100), Random.RandomRange(0, 100));
    }

    public void StartMining()
    {
        if (IsMining == false) { 
            Destroy(gameObject, 2);
            var rigid = GetComponent<Rigidbody>();
            rigid.velocity = Vector3.zero;

            rigid.freezeRotation = true;
            IsMining = true;
            var hitbox = GetComponentInChildren<SphereCollider>();
            hitbox.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        var rigid = GetComponent<Rigidbody>();
        rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
        rigid.position = new Vector3(rigid.position.x, 0, rigid.position.z);

        if (IsMining) 
        {
            if (LastResource < DateTime.Now.AddMilliseconds(-ResourceSpawnRate))
            {
                GameObject.Instantiate(ResourceSpawn.gameObject, transform.position, transform.rotation);
                LastResource = DateTime.Now;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        var comp = collision.gameObject.GetComponentInParent<ShipLogic>();
        if (comp != null)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
