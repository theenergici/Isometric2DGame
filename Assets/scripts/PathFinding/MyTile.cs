using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    public int G;
    public int H;
    public Vector3Int gridLocation;
    public int F{get{return G+H;}}
    public bool isBlocked=false;
    public MyTile previous;

}
