using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLogic : MonoBehaviour
{
    private DateTime Spawned = DateTime.MaxValue;
    public bool IsTracking = false;
    public long Energy = 2;
    // Start is called before the first frame update
    void Start()
    {
        var rigid = GetComponent<Rigidbody>();
        rigid.AddForce(RandomVector() * 10);
        Spawned = DateTime.Now;
        Destroy(gameObject,2);

    }

    private Vector3 RandomVector()
    {
        return new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20));
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawned < DateTime.Now.AddMilliseconds(-500))
        {
            
            if (!IsTracking)
            {
                var rigid = GetComponent<Rigidbody>();
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
                rigid.isKinematic = true;
                IsTracking = true;
            }


            var ship = GameObject.FindObjectOfType<ShipLogic>();

            var dist = Vector3.Distance(transform.position, ship.transform.position);
            if (dist > 0.2)
            {
                transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, Time.deltaTime * 100);
            }
            else
            {
                ship.AddEnergy(Energy);
                Destroy(gameObject);
            }
        }
    }
}
