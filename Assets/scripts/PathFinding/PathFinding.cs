using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class PathFinding
{


   public List<MyTile> findPath(MyTile start, MyTile end){
        List<MyTile> openList = new List<MyTile>();
        List<MyTile> closedList = new List<MyTile>();

        openList.Add(start);
        MyTile currentTile;

        while(openList.Count > 0){
            currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);


            if(currentTile == end){
                //we finalize our search
                return GetFinishedList(start, end);
            }

            var neighborTiles = GetNeighborTiles(currentTile);
            
            foreach(var neighbor in neighborTiles){
                if(neighbor.isBlocked || closedList.Contains(neighbor) || Mathf.Abs(neighbor.gridLocation.z - currentTile.gridLocation.z)> 1){
                    continue;
                }
                neighbor.G = GetManhattenDistance(start, neighbor);
                neighbor.H = GetManhattenDistance(end, neighbor);

                neighbor.previous= currentTile;
                
                if(!openList.Contains(neighbor)){
                    openList.Add(neighbor);
                }

            }


        }

        return new List<MyTile>();
   }

    private List<MyTile> GetFinishedList(MyTile start, MyTile end)
    {
        List<MyTile> finishedList = new List<MyTile>();
        MyTile currentTile= end;
        while(currentTile!=start){
            finishedList.Add(currentTile);
            currentTile= currentTile.previous;
        }
        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattenDistance(MyTile start, MyTile neighbor)
    {
        return Mathf.Abs(start.gridLocation.x - neighbor.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbor.gridLocation.y);
    }

    private List<MyTile> GetNeighborTiles(MyTile currentTile)
    {
        var map= MapManager.Instance.map;
        List<MyTile> neighbors = new List<MyTile>();

        //right
        Vector2Int locationToCheck = new Vector2Int(currentTile.gridLocation.x+1, currentTile.gridLocation.y);
        if(map.ContainsKey(locationToCheck)){
            neighbors.Add(map[locationToCheck]);
        }
        // left
        locationToCheck = new Vector2Int(currentTile.gridLocation.x-1, currentTile.gridLocation.y);
        if(map.ContainsKey(locationToCheck)){
            neighbors.Add(map[locationToCheck]);
        }
        //top
        locationToCheck = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y+1);
        if(map.ContainsKey(locationToCheck)){
            neighbors.Add(map[locationToCheck]);
        }
        //bot
        locationToCheck = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y-1);
        if(map.ContainsKey(locationToCheck)){
            neighbors.Add(map[locationToCheck]);
        }
    
        return neighbors;
    }
}
