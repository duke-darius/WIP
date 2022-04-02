using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerLogic : MonoBehaviour
{
    public ShipLogic Target;
    public AsteroidLogic Spawner;
    public float MinSpeed = 2;
    public float MaxSpeed = 8;

    public float CullDistance = 10;

    public float SpawnRate = 1;

    public DateTime LastCreated = DateTime.MinValue;

    // Start is called before the first frame update
    void Awake()
    {
        
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        if(LastCreated < DateTime.Now.AddMilliseconds(-SpawnRate))
        {
            
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        LastCreated = DateTime.Now;
        
        GameObject.Instantiate(Spawner.gameObject, RandomPosition(), transform.rotation);
    }

    private Vector3 RandomPosition()
    {
        var vec = new Vector3(20, 0, 0);
        vec = Quaternion.AngleAxis(UnityEngine.Random.Range(0,360), Vector3.up) * vec;
        return vec;
    }
}
