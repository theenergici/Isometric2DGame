using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackState : IState
{
    EnemyBot _parent;
    MonoBehaviour _player;
    PlayerDetectorTrigger _detector;

    Coroutine attacking =null;

    // float attackDuration = .75f;
    // float backAnimationDuration= .3f;
    float attackRotation = 90;
    float startRotation;
    float endRotation; 
    
    AttackScript attack;

    public AttackState(EnemyBot parent, PlayerDetectorTrigger playerDetector, AttackScript Attack){
        _parent = parent;

        _detector = playerDetector;
        
        attack=Attack;

        if(_detector.Player)
            _player= _detector.Player;

        startRotation = _parent.transform.rotation.z;
        endRotation = startRotation + attackRotation;
    }

    public void OnEnter()
    {
        _parent.isAttacking= true;

    }

    public void OnExit()
    {
        _parent.isAttacking=false;
        if(attacking!=null){
            _parent.StopCoroutine(attacking);
            attacking=null;
        }
        _parent.transform.eulerAngles = new Vector3(_parent.transform.eulerAngles.x, _parent.transform.eulerAngles.y, startRotation);

    }

    public void Tick()
    {
        if(attacking==null){
            DoAttack();
        }
    }

    private void DoAttack(){

        // attacking = _parent.StartCoroutine(attackAnimation());
        if(_player== null){
            if(_detector.Player!=null)_player= _detector.Player;
            else Debug.Log("No player found using detector");
        }
        Vector2 dir = new Vector2(_player.transform.position.x - _parent.transform.position.x , _player.transform.position.y -  _parent.transform.position.y);
        if(math.abs(dir.x) < 0.001 )dir.x = 0;
        if(math.abs(dir.y) < 0.001 )dir.y = 0;

        attack.changeDirection(DirectionMap.Map(dir));// like this we can only attack in 8 directions
        attack.DoAttack();
        _parent.isAttacking= false;

    }

    private IEnumerator attackAnimation(){
        Vector2 dir = new Vector2(_player.transform.position.x - _parent.transform.position.x , _player.transform.position.y -  _parent.transform.position.y);
        if(math.abs(dir.x) < 0.001 )dir.x = 0;
        if(math.abs(dir.y) < 0.001 )dir.y = 0;

        attack.changeDirection(DirectionMap.Map(dir));// like this we can only attack in 8 directions
        attack.DoAttack();


        // float t=0;
        // float zRotation;
        // do{
        //     t+=Time.deltaTime;
        //     zRotation = Mathf.Lerp(startRotation, endRotation, t / attackDuration) % 360.0f;
        //     _parent.transform.eulerAngles = new Vector3(_parent.transform.eulerAngles.x, _parent.transform.eulerAngles.y, zRotation);
        //     yield return new WaitForEndOfFrame();

        // }while(t<attackDuration);

        // t=0;
        // do{
        //     t+=Time.deltaTime;
        //     zRotation = Mathf.Lerp( endRotation, startRotation, t / backAnimationDuration) % 360.0f;
        //     _parent.transform.eulerAngles = new Vector3(_parent.transform.eulerAngles.x, _parent.transform.eulerAngles.y, zRotation);
        //     yield return new WaitForEndOfFrame();
            
        // }while(t<backAnimationDuration);

        // attacking=null;
        //     _parent.transform.eulerAngles = new Vector3(_parent.transform.eulerAngles.x, _parent.transform.eulerAngles.y, startRotation);
        yield return null;
        _parent.isAttacking= false;
    }
}