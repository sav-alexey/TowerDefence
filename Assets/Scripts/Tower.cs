using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Tower : MonoBehaviour
{   

    [SerializeField] Transform towerTop;
    [SerializeField] Transform target;
    [SerializeField] float attackRange;
    [SerializeField] ParticleSystem projectileParticle;
    public Waypoint baseWaypoint;

    void Update()
    {
        SetTargetEnemy();
        if (target)
        {
            towerTop.LookAt(target);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }
        Transform closestEnemy = sceneEnemies[0].transform;
        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        target = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distanceToA = Vector3.Distance(transformA.position, gameObject.transform.position);
        float distanceToB = Vector3.Distance(transformB.position, gameObject.transform.position);
        if (distanceToA < distanceToB)
        {
            return transformA;
        }
        return transformB;
    }


    public void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
