using UnityEngine;

public enum Direction
{
    North=0,
    NE=1,
    East=2,
    SE=3,
    South=4, 
    SW=5,
    West=6,
    NW=7,
    origin=8,
}

public class DirectionMap{
    public static Direction Map(Vector2 input){

        Direction ret=Direction.origin;
        float x= input.x;
        float y= input.y;
        
        if(x==0 && y >0)return Direction.North;
        else if(x>0 && y>0) return Direction.NE;
        else if(x>0 && y==0) return Direction.East;
        else if(x>0 && y<0 ) return Direction.SE;
        else if(x==00 && y<0) return Direction.South;
        else if(x<0 && y<0) return Direction.SW;
        else if(x<0 && y==0) return Direction.West;
        else if(x<0 && y>0) return Direction.NW;

        return ret;
    }   
}