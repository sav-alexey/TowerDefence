using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{


    // [SerializeField] [Range(1, 10)] int gridSize = 10;
    

    Vector3 gridPos;
    Waypoint waypoint;

    void Awake(){

        waypoint = GetComponent<Waypoint>();

    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();

    }

    private void UpdateLabel()
    {

        TextMesh label;
        int gridSize = waypoint.GetGridSize();
        string position = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        label = GetComponentInChildren<TextMesh>();
        label.text = position;
        gameObject.name = position;
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
    
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0f,
            waypoint.GetGridPos().y * gridSize
        );
    }
}
