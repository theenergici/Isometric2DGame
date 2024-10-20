using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{   
    [SerializeField]
    Animator animator;
    private void Awake() {
        if(animator==null){
            animator= GetComponentInChildren<Animator>();
        }
    }
    public void AfterAttack(){
        animator?.SetBool("Attack", false);
    }
}
