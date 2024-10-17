using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private MonoBehaviour _parent;
    private MonoBehaviour _player;

    private PlayerDetectorTrigger _detector;
    private SpriteRenderer _renderer;
    private Color normalColor;
    private Color angryColor;
    public ChaseState(MonoBehaviour parent, PlayerDetectorTrigger playerDetector,
     SpriteRenderer renderer , Color normal, Color angry){
        _parent = parent;
        _detector = playerDetector;
        if(_detector._player)
            _player = _detector._player;
        _renderer = renderer;
        normalColor = normal;
        angryColor= angry;
    }
    public void OnEnter()
    {
        if(!_player)
            _player=_detector._player;
            
        _renderer.color= angryColor;
    }

    public void OnExit()
    {   
        _renderer.color= normalColor;
    }

    public void Tick()
    {
        if(_player)
            Debug.Log($"Chasing player:{_player.name}");
    }
}
