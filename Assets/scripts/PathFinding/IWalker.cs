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
    float NormalSpeed= 1.0f;
    [SerializeField]
    float sprintSpeed = 3.0f;
    float speed;
    [SerializeField]
    Animator _animator;

    private void Start() {
        speed= NormalSpeed;
        pathFinder= new PathFinding();  
        SetNextTarget(CurrentTarget);
        if(_animator==null)Debug.LogWarning($"No animator found in:{name}");
    }
    public bool SetNextTarget(MyTile newTarget){
        CurrentTarget = newTarget;
        if(CurrentTarget!= null){
            // Debug.Log($"getting path {CurrentTile} to {CurrentTarget.gridLocation}");
            currentPath = pathFinder.findPath(CurrentTile, CurrentTarget);
            // Debug.Log($"Path length {currentPath.Count}");
        }
        return CurrentTarget != null;

    }

    public void SetSprinting(){
        speed= sprintSpeed;
    }
    public void SetWalking(){
        speed= NormalSpeed;
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
                if(_animator!=null){
                    var t=directionTowardsTarget(currentPath[0].transform);
                    if(t>0)
                    _animator.SetInteger("Direction", t);
                } 
            }


    }

    public void PositionCharacterOnTile(MyTile tile)
    {
        MapManager.Instance.PositionOnTile(tile, transform);
        CurrentTile = tile;
    }

    public int directionTowardsTarget(Transform target){
        if(currentPath.Count<=0)return -1;
        var dir = new Vector2(target.position.x - transform.position.x,  target.position.y - transform.position.y);
        if(Mathf.Abs(dir.x) < 0.01 )dir.x = 0;
        if(Mathf.Abs(dir.y) < 0.01 )dir.y = 0;
        return (int)DirectionMap.Map(dir);
    }
}
