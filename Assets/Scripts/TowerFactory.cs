using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit;
    int towerCount = 0;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint){
        
        towerCount = towerQueue.Count;
        if(towerCount < towerLimit){
            var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = gameObject.transform;
            baseWaypoint.isPlaceable = false;
            newTower.baseWaypoint = baseWaypoint;
            towerQueue.Enqueue(newTower);

        }
        else{
            MoveTowerToNewPosition(baseWaypoint);
        }
    }

    private void MoveTowerToNewPosition(Waypoint newBaseWaypoint){
        print("Tower limit reached");
        var oldTower = towerQueue.Dequeue();
        oldTower.transform.position = newBaseWaypoint.transform.position;
        print(oldTower.baseWaypoint);
        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = newBaseWaypoint;
        towerQueue.Enqueue(oldTower);
    }

}
