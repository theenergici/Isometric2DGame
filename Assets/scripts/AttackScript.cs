using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    float damageOnHit= 5.0f;
    [SerializeField]
    float attackDistance = 1.8f;

    [SerializeField]
    Animator animator;
    Transform attackEmiterTransform;

    [SerializeField]
    InputActionReference attackReference;
    
    private Direction attackDirection=Direction.North;

    


    private void Awake() {
        
        if(attackReference==null)Debug.LogError($"Missing attack InputActionReference on {name}");
        if(!animator) animator= GetComponent<Animator>();
        var t = GetComponentsInChildren<Transform>();
        foreach (var trans in t ){
            if(trans.gameObject!= this.gameObject){
                attackEmiterTransform= trans.transform;
                break;
            }
        }
        attackEmiterTransform.localPosition = new Vector3(attackEmiterTransform.localPosition.x, attackDistance ,attackEmiterTransform.localPosition.z);
    }
    private void OnEnable() {
        attackReference.action.performed += DoAttack;
        
    }

    private void OnDisable() {
        attackReference.action.performed -= DoAttack;
    }


    public void changeDirection(Direction newDirection){
        attackDirection = newDirection;
    }

    private void OnTriggerEnter(Collider other) {

        var cast =other.GetComponentInChildren<IHittable>();
        if(cast!=null){
            if(cast.OnHit(damageOnHit))
                cast.OnDeath();
        }

    }


    public void afterAttack(){
        animator.SetBool("Attack", false);
    }


    private void DoAttack(InputAction.CallbackContext context)
    {   
        var angle = (int)attackDirection * -45;
        Debug.Log(angle);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
        animator.SetBool("Attack", true);
    }

}
