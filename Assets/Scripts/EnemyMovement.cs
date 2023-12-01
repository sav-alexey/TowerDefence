using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] List<Waypoint> waypoints;
    [SerializeField] List<Waypoint> path;
    [SerializeField] float moveStep = 1f;
    [SerializeField] float speed = 1f;

    PathFinder pathFinder;
    EnemyDamage enemyDamage;
    Castle castle;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveStep);
    }

    // Update is called once per frame
    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.LookAt(waypoint.transform);
            targetPosition = waypoint.transform.position;
            yield return new WaitForSeconds(speed);
        }
        enemyDamage = GetComponent<EnemyDamage>();
        enemyDamage.DestroyEnemy(false);
        castle = FindObjectOfType<Castle>();
        castle.TakeDamage();
    }
}
