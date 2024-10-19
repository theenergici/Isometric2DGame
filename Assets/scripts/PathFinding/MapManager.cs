using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [SerializeField]
    MyTile tilePrefab;

    public Dictionary<Vector2, MyTile> map;
    
    private void Awake() {
        if(_instance!= null && _instance!= this)
            Destroy(this.gameObject);

        else _instance = this;
        
        

    }

    private void Start() {
        GameObject TileContainer= new GameObject();
        TileContainer.transform.parent = this.transform;
        TileContainer.name = "TileContainer";
        var tilemaps = GetComponentsInChildren<Tilemap>();
        map = new Dictionary<Vector2, MyTile>();

        foreach (var tilemap in tilemaps)
        {
        BoundsInt bounds = tilemap.cellBounds;

        for(int z= bounds.zMax; z> bounds.zMin; z--){
            for(int x = bounds.xMin; x< bounds.xMax; x++){
                for(int y = bounds.yMin; y< bounds.yMax; y++){

                    
                    var tileLoc = new Vector3Int(x,y,z);
                    var tileKey = new Vector2(x,y);

                    if(tilemap.HasTile(tileLoc) && !map.ContainsKey(tileKey) ){

                        var tempTile = Instantiate(tilePrefab, TileContainer.transform);
                        var cellWorldPosition = tilemap.GetCellCenterWorld(tileLoc);
                        tempTile.transform.position = new Vector3(cellWorldPosition.x,cellWorldPosition.y,cellWorldPosition.z + 1);
                        tempTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                        tempTile.gridLocation = tileLoc;

                        map.Add(tileKey, tempTile);

                        }
                    }
                }
            } 
        }
        
        
    }
}
