using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{

    [SerializeField]
    Color DefaultColor= Color.yellow;

    [SerializeField]
    Color Angry = Color.red;

    [SerializeField]
    float attackRange = 0.5f;
    private float Real_attackRange;

    [SerializeField]
    GameObject attackRangeVisual;
    AttackRangeTrigger attackCollider;

  
    private PlayerDetectorTrigger detector;
    private FSM _stateMachine;
    private SpriteRenderer _renderer;

    public bool isAttacking=false;




    private void Awake() {

        
        Real_attackRange = attackRange + (transform.localScale.x/2 /Mathf.Sin(MathF.PI/4));
       
        attackCollider = GetComponentInChildren<AttackRangeTrigger>();
        
        

        //
        detector = GetComponent<PlayerDetectorTrigger>();  
        _stateMachine  = new FSM();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color= DefaultColor;

        // isntantiate possible states the enemy can have
        var idle = new IdleState(this, _renderer, DefaultColor);
        //TODO add player
        var chaseState = new ChaseState(this, detector, _renderer, DefaultColor, Angry);
        var atkState = new AttackState(this, detector);
        var patrolState = new PatrolState(this);

        //TODO fix conditions
        At(idle, chaseState, playerOutOfRange());
        At(patrolState, chaseState, playerOutOfRange());
        At(chaseState, idle, playerDetected());

        // to atk state
        _stateMachine.addAnyTransition(atkState, playerInATKRange());

        At(idle, atkState, playerOutOfRange());
        At(chaseState, atkState, playerDetected());


        void At(IState to, IState from, Func<bool> condition)=> _stateMachine.addTransition( from, to, condition);

        Func<bool> playerDetected() => ()=>detector.playerInDetectionRange && !isAttacking;
        Func<bool> playerOutOfRange() => ()=>!detector.playerInDetectionRange && !isAttacking;
        Func<bool> playerInATKRange() => ()=> attackCollider.IsInAttackRange && !isAttacking;

        _stateMachine.SetState(idle);
    }        
    
    
        

    private void Update() {
        _stateMachine.Tick();


        Real_attackRange = attackRange + (transform.localScale.x/2 /Mathf.Sin(MathF.PI/4));  
        var tmp = Real_attackRange*2;
        if(attackRangeVisual!=null){
            attackRangeVisual.transform.localScale = new Vector3(tmp, tmp , tmp);
            
        }      
    
        Debug.Log(_stateMachine.CurrentState);
    }
    

}
