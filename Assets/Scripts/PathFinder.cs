using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> queue = new Queue<Waypoint>();

    Waypoint searchCenter;

    Vector2Int [] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down
    };

    bool isRunning = true;

    List<Waypoint> path = new List<Waypoint>();

    void Start()
    {

    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        endWaypoint.isPlaceable = false;

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
            previous.isPlaceable = false;
        }
        SetAsPath(startWaypoint);
        startWaypoint.isPlaceable = false;
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void FindPath()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning) 
        {
            searchCenter = queue.Dequeue();
            // print("Searching from: " + searchCenter);
            searchCenter.isExplored = true;
            if (searchCenter == endWaypoint)
            {
                // print("Found endWaypoint");
                isRunning = false;
            }
            else
            {
                ExploreNeighbours();
            }
        }
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                Waypoint neighbourPoint = grid[neighbourCoordinates];
                if (neighbourPoint.isExplored || queue.Contains(neighbourPoint))
                {
                    // do nothing
                }
                else
                {
                    queue.Enqueue(grid[neighbourCoordinates]);
                    neighbourPoint.isExplored = true;
                    neighbourPoint.exploredFrom = searchCenter;
                    neighbourPoint.SetTopColor(Color.white);
                }
            }
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                // waypoint.SetTopColor(Color.white);
            }
        }
    }

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            ColorStartAndEnd();
            FindPath();
            CreatePath();
        }
        return path;
    }
    
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
