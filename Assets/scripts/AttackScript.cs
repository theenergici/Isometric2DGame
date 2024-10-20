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
    [SerializeField, Tooltip("Distance from pivot the animation will be done")]
    float attackDistance = 1.8f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    public Transform attackEmiterPivot;

    [SerializeField]
    InputActionReference attackReference;
    
    [SerializeField]
    bool isBot=false;

    private Direction attackDirection=Direction.North;

    


    private void Awake() {
        
        if(attackReference==null)Debug.LogError($"Missing attack InputActionReference on {name}");
        if(!animator) animator= GetComponent<Animator>();
       
        
        if(attackEmiterPivot != null)
            transform.position = new Vector3(transform.localPosition.x, transform.position.y + attackDistance ,transform.localPosition.z);
        
    }
    private void OnEnable() {
        if(!isBot)
            attackReference.action.performed += DoAttack;
        
    }

    private void OnDisable() {
        if(!isBot)    
            attackReference.action.performed -= DoAttack;
    }


    public void changeDirection(Direction newDirection){
        attackDirection = newDirection;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        
        var cast =other.GetComponentInChildren<IHittable>();
        if(cast!=null){
            if(cast.OnHit(damageOnHit))
                cast.OnDeath();
        }

    }


    public void afterAttack(){
        
        if(animator!=null)
            animator?.SetBool("Attack", false);
    }


    private void DoAttack(InputAction.CallbackContext context)
    {   
        DoAttack();
    }
    public void DoAttack(){
        var angle = (int)attackDirection * -45 + 90;
        attackEmiterPivot.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
        if(animator!=null)
            animator?.SetBool("Attack", true);
    }

}
