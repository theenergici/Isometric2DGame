using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChaseState : IState
{
    private MonoBehaviour _parent;
    private PlayerMonobehaviour _player;

    private PlayerDetectorTrigger _detector;
    private SpriteRenderer _renderer;
    private Color normalColor;
    private Color angryColor;

    private IWalker _walker;
    private MyTile lastObjective;

    private int tickCounter;
    private int tickToRecalculate;
   

    public ChaseState(MonoBehaviour parent, PlayerDetectorTrigger playerDetector,
     SpriteRenderer renderer , Color normal, Color angry, IWalker walker, int ticksToRecalculatePath=5){
        _parent = parent;
        _detector = playerDetector;
        if(_detector.Player)
            _player = _detector.Player;
        _renderer = renderer;
        normalColor = normal;
        angryColor= angry;
        tickToRecalculate= ticksToRecalculatePath;
        _walker=walker;
    }
    public void OnEnter()
    {
        if(!_player)
            _player=_detector.Player;
        
        lastObjective = _walker.CurrentTarget;
        _walker.SetNextTarget(_player.currentTile);

        _renderer.color= angryColor;
        tickCounter=0;
        
    }

    public void OnExit()
    {   
        _renderer.color= normalColor;
        _walker.SetNextTarget(lastObjective);
    }

    public void Tick()
    {
        tickCounter++;
        _walker.Step();
        if(tickCounter%tickToRecalculate==0 && _player!=null){
            _walker.SetNextTarget(_player.currentTile);
            tickCounter=0;      
        }
    }
}
