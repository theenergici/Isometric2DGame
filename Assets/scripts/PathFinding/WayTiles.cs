using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WayTiles : MyTile
{
    
    [SerializeField]
    List<MyTile> possibleNextTiles;




    private void Start() {
        if(possibleNextTiles.Count == 0)
            Destroy(this);
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        var o = other.GetComponentInChildren<IWalker>();
        if(o!= null){
            // Debug.Log($"arrived at point {name}");
            o.SetNextTarget(GetNextTile());
        }
    }

    public virtual MyTile GetNextTile(){

        int rand = Random.Range((int)0,(int)possibleNextTiles.Count);
        return possibleNextTiles[rand];

    }

}
