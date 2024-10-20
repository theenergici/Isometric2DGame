using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    EnemyBot _parent;
    MonoBehaviour _player;
    PlayerDetectorTrigger _detector;

    Coroutine attacking =null;

    float attackDuration = .75f;
    float backAnimationDuration= .3f;
    float attackRotation = 90;
    Transform initialTrans;

    public AttackState(EnemyBot parent, PlayerDetectorTrigger playerDetector){
        _parent = parent;

        _detector = playerDetector;

        if(_detector.Player)
            _player= _detector.Player;
    }

    public void OnEnter()
    {
        _parent.isAttacking= true;
        initialTrans= _parent.transform;
    }

    public void OnExit()
    {
        _parent.transform.rotation= initialTrans.rotation;
        _parent.isAttacking=false;
        if(attacking!=null){
            _parent.StopCoroutine(attacking);
            attacking=null;
        }
    }

    public void Tick()
    {
        if(attacking==null){
            DoAttack();
        }
    }

    private void DoAttack(){
        attacking = _parent.StartCoroutine(attackAnimation());
    }

    private IEnumerator attackAnimation(){

        float t=0;
        float startRotation = _parent.transform.rotation.z;
        float endRotation = startRotation + attackRotation;
        float zRotation;
        do{
            t+=Time.deltaTime;
            zRotation = Mathf.Lerp(startRotation, endRotation, t / attackDuration) % 360.0f;
            _parent.transform.eulerAngles = new Vector3(_parent.transform.eulerAngles.x, _parent.transform.eulerAngles.y, zRotation);
            yield return new WaitForEndOfFrame();

        }while(t<attackDuration);

        t=0;
        do{
            t+=Time.deltaTime;
            zRotation = Mathf.Lerp( endRotation, startRotation, t / backAnimationDuration) % 360.0f;
            _parent.transform.eulerAngles = new Vector3(_parent.transform.eulerAngles.x, _parent.transform.eulerAngles.y, zRotation);
            yield return new WaitForEndOfFrame();
            
        }while(t<backAnimationDuration);

        attacking=null;
        
        _parent.isAttacking= false;
    }
}