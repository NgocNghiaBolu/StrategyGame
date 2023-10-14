using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateTarget : MonoBehaviour
{
    Transform target;
    public Transform weapon;
    public float rangeShoot = 30f;
    public ParticleSystem particleEffect;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDis = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDis)
            {
                closestTarget = enemy.transform;
                maxDis = targetDistance;
            }
        }
        target = closestTarget;
    }

    void AimWeapon()
    {
        float targetDis = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if(targetDis < rangeShoot)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = particleEffect.emission;
        emissionModule.enabled = isActive;
    }
}
