using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    const int gridSize = 10;
    Vector2Int gridPos;
    public bool isExplored = false;
    public Waypoint exploredFrom;
    
    public bool isPlaceable = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGridSize(){
        return gridSize;
    }

    public Vector2Int GetGridPos(){
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public void SetTopColor(Color color){
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            if(isPlaceable){
                FindObjectOfType<TowerFactory>().AddTower(this);
            }else{
                print("Can't place here");
            }
        }
    }

}
