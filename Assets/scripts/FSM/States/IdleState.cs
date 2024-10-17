using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{   
    private MonoBehaviour _parent;
    private SpriteRenderer _renderer;
    private Color _color;
    public IdleState(MonoBehaviour parent, SpriteRenderer renderer, Color color){
        _parent = parent;
        _color= color;
        _renderer = renderer;
    }
    public void OnEnter()
    {
        _renderer.color= _color;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
