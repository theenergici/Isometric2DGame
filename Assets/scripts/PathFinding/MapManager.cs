using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Tilemap tilemap;

    public Dictionary<Vector2, MyTile> map;
    
    private void Awake() {
        if(_instance!= null && _instance!= this)
            Destroy(this.gameObject);

        else _instance = this;
        
        GameObject TileContainer= new GameObject();
        TileContainer.transform.parent = transform;
        TileContainer.name = "TileContainer";
        var tilemaps = GetComponentsInChildren<Tilemap>();
        map = new Dictionary<Vector2, MyTile>();

        var wayTiles = FindObjectsOfType<MyTile>();
        
        tilemap =tilemaps[0];
        
        BoundsInt bounds = tilemap.cellBounds;

        for(int z= bounds.zMax; z> bounds.zMin; z--){
            for(int x = bounds.xMin; x< bounds.xMax; x++){
                for(int y = bounds.yMin; y< bounds.yMax; y++){

                    
                    var tileLoc = new Vector3Int(x,y,z);
                    var tileKey = new Vector2(x,y);

                    MyTile tempTile;
                    if(tilemap.HasTile(tileLoc) && !map.ContainsKey(tileKey) ){

                    
                        tempTile = Instantiate(tilePrefab, TileContainer.transform);
                        tempTile.name = $"Tile:({tileLoc.x},{tileLoc.y})";
                        
                        var cellWorldPosition = tilemap.GetCellCenterWorld(tileLoc);
                        tempTile.transform.position = new Vector3(cellWorldPosition.x,cellWorldPosition.y,cellWorldPosition.z + 1);
                        tempTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                        tempTile.gridLocation = tileLoc;

                        map.Add(tileKey, tempTile);

                        }
                    }
                }
            } 
                
        
        foreach (var tempTile in wayTiles)
            {
                var p =tilemap.WorldToCell(tempTile.transform.position);
                var tKey= new Vector2(p.x,p.y);
                var cellWorldPosition = tilemap.GetCellCenterWorld(p);
                tempTile.transform.position = new Vector3(cellWorldPosition.x,cellWorldPosition.y,cellWorldPosition.z + 1);
                tempTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                tempTile.gridLocation = p;
                
                if(map.ContainsKey(tKey)){
                    var ex = map[tKey];
                    map.Remove(tKey);
                    Destroy(ex.gameObject);
                }      
                map.Add(tKey,tempTile);
                
            } 
        
        
    }

    public MyTile getTileFromWorldPosition(Vector3 position){
        var cellWorldPosition = tilemap.WorldToCell(position);
        var k = new Vector2(cellWorldPosition.x, cellWorldPosition.y);
        if(map.ContainsKey(k))
            return map[k];
        else return null;

    }

    public void PositionOnTile(MyTile tile, Transform _transform)
    {
        _transform.position=  new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        var renderer = _transform.gameObject.GetComponent<SpriteRenderer>();
        var tileRenderer = tile.GetComponent<SpriteRenderer>();
        if (renderer!=null && tileRenderer!= null)
            renderer.sortingOrder = tileRenderer.sortingOrder;
    }



}
