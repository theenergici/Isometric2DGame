using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private MonoBehaviour _parent;
    private IWalker _walker;
    int tickInt;
    int _pathTickCheck;
    private float cumulativeTime;


    public PatrolState(MonoBehaviour parent, IWalker walker, int pathTickCheck=5){
        _parent=parent;
        _walker =walker;
        _pathTickCheck= pathTickCheck;        
    }
    public void OnEnter()
    {
        tickInt=0;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        tickInt++;
        cumulativeTime+=Time.deltaTime;

        if(tickInt%_pathTickCheck==0){
            _walker.Step(cumulativeTime);
            cumulativeTime=0;
        }
    }

   
}
