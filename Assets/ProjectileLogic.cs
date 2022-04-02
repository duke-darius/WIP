using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        var comp = other.GetComponentInParent<AsteroidLogic>();
        if (comp != null)
        {

            comp.StartMining();
            Destroy(gameObject);
        }
    }

}
