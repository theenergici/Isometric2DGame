using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{   
    private MonoBehaviour _parent;
    public IdleState(MonoBehaviour parent){
        _parent = parent;
    }
    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
