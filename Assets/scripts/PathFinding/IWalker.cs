using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWalker : MonoBehaviour
{
    
    public MyTile CurrentTarget;
    public MyTile CurrentTile{get; private set;}
    public List<MyTile> currentPath = new List<MyTile>();
    public PathFinding pathFinder;

    [SerializeField]
    float speed= 1.0f;

    private void Start() {
        pathFinder= new PathFinding();  
        SetNextTarget(CurrentTarget);
    }
    public bool SetNextTarget(MyTile newTarget){
        CurrentTarget = newTarget;
        if(CurrentTarget!= null){
            Debug.Log($"getting path {CurrentTile} to {CurrentTarget.gridLocation}");
            currentPath = pathFinder.findPath(CurrentTile, CurrentTarget);
            Debug.Log($"Path length {currentPath.Count}");
        }
        return CurrentTarget != null;

    }

    public void Step(){
        Step(Time.deltaTime);
    }
    public void Step(float time){
        var step = time * speed;  
        if(currentPath.Count==0)return;

        var zIndex = currentPath[0].transform.position.z;
        transform.position = Vector2.MoveTowards(transform.position, currentPath[0].transform.position, step);
        transform.position = new Vector3(transform.position.x, transform.position.y, zIndex);

        if(Vector2.Distance(transform.position, currentPath[0].transform.position)< 0.01f)
            {
                PositionCharacterOnTile(currentPath[0]);
                currentPath.RemoveAt(0);
            }

    }

    public void PositionCharacterOnTile(MyTile tile)
    {
        MapManager.Instance.PositionOnTile(tile, transform);
        CurrentTile = tile;
    }
}
