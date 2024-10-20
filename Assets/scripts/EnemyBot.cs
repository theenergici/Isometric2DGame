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

    [SerializeField]
    WayTiles StartingObjective;

  
    private PlayerDetectorTrigger detector;
    private FSM _stateMachine;
    private SpriteRenderer _renderer;

    public bool isAttacking=false;

    private IWalker walker;
    
    private Vector3 lastPosition;

    

    [SerializeField]
    private float MaxTimeIdle= 10.0f;
    [SerializeField, Tooltip("only visualization of how much time bot has been idle")]
    public float stayPutCounter;
    [SerializeField]
    AttackScript attackScript;




    private void Start() {

        if(attackScript==null){
            attackScript= GetComponentInChildren<AttackScript>();
        }

        Real_attackRange = attackRange + (transform.localScale.x/2 /Mathf.Sin(MathF.PI/4));
       
        attackCollider = GetComponentInChildren<AttackRangeTrigger>();
        walker = GetComponent<IWalker>();

        var t = MapManager.Instance.getTileFromWorldPosition(transform.position);
        walker.PositionCharacterOnTile(t);

        walker.SetNextTarget(StartingObjective);
        
        

        //
        detector = GetComponent<PlayerDetectorTrigger>();  
        _stateMachine  = new FSM();

        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color= DefaultColor;

        // isntantiate possible states the enemy can have
        var idle = new IdleState(this, _renderer, DefaultColor);
        //TODO add player
        var chaseState = new ChaseState(this, detector, _renderer, DefaultColor, Angry, walker);
        var atkState = new AttackState(this, detector, attackScript);
        var patrolState = new PatrolState(this, walker);

        //TODO fix conditions
        At(idle, chaseState, playerOutOfRange());//set specifics for each
        At(patrolState, chaseState, playerOutOfRange());
        At(chaseState, idle, playerDetected());

        // to atk state
        _stateMachine.addAnyTransition(atkState, playerInATKRange());
        
        At(idle, atkState, playerOutOfRange());
        At(chaseState, atkState, playerDetected());
        At(patrolState, idle, TooMuchTimeIdle());


        void At(IState to, IState from, Func<bool> condition)=> _stateMachine.addTransition( from, to, condition);

        Func<bool> playerDetected() => ()=>detector.playerInDetectionRange && !isAttacking;
        Func<bool> playerOutOfRange() => ()=>!detector.playerInDetectionRange && !isAttacking;
        Func<bool> playerInATKRange() => ()=> attackCollider.IsInAttackRange && !isAttacking;
        Func<bool> TooMuchTimeIdle ()=> ()=> stayPutCounter> MaxTimeIdle;


        _stateMachine.SetState(patrolState);
    }        
    
    
        

    private void Update() {
        _stateMachine.Tick();

        Real_attackRange = attackRange + (transform.localScale.x/2 /Mathf.Sin(MathF.PI/4));  
        var tmp = Real_attackRange*2;
        if(attackRangeVisual!=null){
            attackRangeVisual.transform.localScale = new Vector3(tmp, tmp , tmp);   
        } 

        if(lastPosition==transform.position){
            stayPutCounter+=Time.deltaTime;
            if(stayPutCounter> MaxTimeIdle){
                walker.SetNextTarget(StartingObjective);
            }
        }else stayPutCounter=0;   

        lastPosition = transform.position;  
    

    }
    

}
