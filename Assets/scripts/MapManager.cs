using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{    
    private static MapManager _instance;
    public static MapManager Instance{
        get{   
            return _instance;    
        }
    }
    
    private void Awake() {
        if(_instance!= null && _instance!= this)
            Destroy(this.gameObject);

        else _instance = this;
        
        var tilemap = GetComponentInChildren<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;

    }
}
