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

    private void OnTriggerEnter(Collider other) {
        var o =other.GetComponent<IWalker>();
        if(o!= null){
            o.SetNextTarget(GetNextTile());
        }
    }

    public virtual MyTile GetNextTile(){

        int rand = Random.Range((int)0,(int)possibleNextTiles.Count);
        return possibleNextTiles[rand];

    }

}
