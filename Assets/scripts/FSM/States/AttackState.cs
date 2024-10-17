using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    MonoBehaviour _parent;
    MonoBehaviour _player;
    public AttackState(MonoBehaviour parent, MonoBehaviour player){
        _parent = parent;
        _player = player;
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}