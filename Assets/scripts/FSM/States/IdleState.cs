using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{   
    private EnemyBot _parent;
    private SpriteRenderer _renderer;
    private Color _color;
    public IdleState(EnemyBot parent, SpriteRenderer renderer, Color color){
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
        _parent.stayPutCounter = 0;
    }

    public void Tick()
    {
        
    }
}
