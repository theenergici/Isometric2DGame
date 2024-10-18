using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    float damageOnHit= 5.0f;
    [SerializeField]
    Animator animator;
    

    private void Awake() {
        if(!animator) animator= GetComponent<Animator>();
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
}
