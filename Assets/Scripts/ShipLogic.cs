using Assets.Upgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public decimal Energy = 100;
    public decimal MaxEnergy = 100;
    public decimal EnergyGain = 0.001M;


    public List<BaseUpgrade> Upgrades= new List<BaseUpgrade>();

    // Start is called before the first frame update
    void Start()
    {
        Upgrades.Add(new DoubleTapUpgrade(this) { Level = 10});
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
                if (CanFireProjectile())
                {
                    LastShot = DateTime.Now;
                    CreateProjectile();
                    foreach (var upgrade in Upgrades)
                        upgrade.OnProjectileShoot();
                    UseEnergy(10);
                }
            }

        }
        else
        {
            //renderer.enabled = false;
        }
        RunEnergyGain();
        var text = GetComponentInChildren<Text>();
        text.text = $"{Energy:0}/{MaxEnergy:0.##}";
    }

    private void RunEnergyGain()
    {
        Energy += EnergyGain;
    }

    internal void AddEnergy(long energy)
    {
        Energy += energy;
        if(Energy > MaxEnergy)
            Energy = MaxEnergy; 

        
    }

    private bool CanFireProjectile()
    {
        return (Energy >= 10);
    }

    private void UseEnergy(decimal toUse)
    {
        Energy -= toUse;
        if(Energy < 0)
            Energy = 0; 
    }

    public void CreateProjectile(float? angle = null)
    {
        var obj = GameObject.Instantiate(Projectile.gameObject, gameObject.transform.position, gameObject.transform.rotation);
        var rigid = obj.GetComponent<Rigidbody>();
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera

        var vel = Camera.main.ScreenToWorldPoint(screenPoint) - transform.position;
        vel.y = 0;
        if (angle.HasValue)
            rigid.MoveRotation(Quaternion.AngleAxis(angle.Value, Vector3.up));
        rigid.AddForce(vel * ProjectileSpeed);
    }
}
