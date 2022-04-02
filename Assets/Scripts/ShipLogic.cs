using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{

    public BlackHoleLogic BlackHole;
    public float OrbitDistance = 10;
    public float OrbitRotationMultiplier = 30f;
    public GameObject Ship;
    public GameObject Projectile;

    [Range(1, 10)]
    public float Difficulty = 1;

    public float AltitudeDrop = 0.000001f;
    public float AltitudeDropAcc = 0.0000001f;

    public AnimationCurve RotationCurve;

    public DateTime LastShot = DateTime.MinValue;
    public float ShotInterval = 200;
    public float ProjectileSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BlackHole != null)
        {
            //AltitudeDrop += (AltitudeDropAcc );
            OrbitDistance -= AltitudeDrop * Time.deltaTime;

            if(OrbitDistance < 3)
            {
                // Lose
                OrbitDistance = 10;
            }
            var centerPos = BlackHole.transform.position;
            var currentPos = transform.position;
            currentPos = centerPos + (currentPos - centerPos).normalized * OrbitDistance;
            transform.position = currentPos;
            var rotation = OrbitRotationMultiplier  * Time.deltaTime;
            transform.RotateAround(centerPos, Vector3.up, rotation);

            
        }

        var renderer = GetComponent<LineRenderer>();

        if (Input.GetMouseButton(0))
        {
            if(LastShot < DateTime.Now.AddMilliseconds(-ShotInterval))
            {
                LastShot = DateTime.Now;
                CreateProjectile();
            }
            //renderer.enabled = true;
            //var mousePos = Input.mousePosition;
            //var screenPoint = Input.mousePosition;
            //screenPoint.z = 10.0f; //distance of the plane from the camera
            
            //renderer.SetPositions(new Vector3[] { gameObject.transform.position, Camera.main.ScreenToWorldPoint(screenPoint) });

        }
        else
        {
            //renderer.enabled = false;
        }

    }

    private void CreateProjectile()
    {
        var obj = GameObject.Instantiate(Projectile.gameObject, gameObject.transform.position, gameObject.transform.rotation);
        var rigid = obj.GetComponent<Rigidbody>();
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera

        var vel = Camera.main.ScreenToWorldPoint(screenPoint) - transform.position;
        vel.y = 0;
        rigid.AddForce(vel * ProjectileSpeed);
    }
}
