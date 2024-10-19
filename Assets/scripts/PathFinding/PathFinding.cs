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
                if(neighbor.isBlocked || closedList.Contains(neighbor) || Mathf.Abs(neighbor.gridLocation.z - currentTile.gridLocation.z)> PlayerConstants.jumpHeight){
                    continue;
                }
                neighbor.G = GetManhattenDistance(start, neighbor);
                neighbor.H = GetManhattenDistance(end, neighbor);

                neighbor.previous= currentTile;
                
                if(openList.Contains(neighbor)){
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


        for(int i = -1; i<=1; i++){
            for(int j = -1; j<=1; j++){
                if(j!=0 && i!=0 && (i==0 || j==0)){ // condition for only the sides && (i==0 || j==0)
                    Vector2Int locationToCheck = new Vector2Int(currentTile.gridLocation.x+i, currentTile.gridLocation.y +j);
                    if(map.ContainsKey(locationToCheck)){
                        neighbors.Add(map[locationToCheck]);
                    }
                }
            }
        }
        return neighbors;
    }
}
