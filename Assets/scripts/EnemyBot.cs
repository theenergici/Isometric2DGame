using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{

    [SerializeField]
    Color DefaultColor= Color.yellow;
    [SerializeField]
    Color Angry = Color.red;

    private PlayerDetectorTrigger detector;
    private FSM _stateMachine;
    private SpriteRenderer _renderer;




    private void Awake() {
        detector = GetComponent<PlayerDetectorTrigger>();  
        _stateMachine  = new FSM();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color= DefaultColor;

        // isntantiate possible states the enemy can have
        var idle = new IdleState(this);
        //TODO add player
        var chaseState = new ChaseState(this, detector, _renderer, DefaultColor, Angry);
        var atkState = new AttackState(this, detector);
        var patrolState = new PatrolState(this);

        //TODO fix conditions
        At(idle, chaseState, playerDetected());
        At(patrolState, chaseState, playerDetected());
        At(chaseState, idle, playerOutOfRange());


        _stateMachine.SetState(idle);





        void At(IState to, IState from, Func<bool> condition)=> _stateMachine.addTransition(to, from, condition);

        Func<bool> playerDetected() => ()=>detector.playerInDetectionRange;
        Func<bool> playerOutOfRange() => ()=>!detector.playerInDetectionRange;



    }        
    
    private void Update() {
        _stateMachine.Tick();
    }
    

}
