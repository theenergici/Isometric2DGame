using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{   
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer SpriteToOrderInLayer;
    private void Awake() {
        if(animator==null){
            animator= GetComponentInChildren<Animator>();
        }
    }
    public void AfterAttack(){
        animator?.SetBool("Attack", false);
    }

    public void SetSpriteRendererOrderInLayer(){
            var sr = GetComponent<SpriteRenderer>();
            if(SpriteToOrderInLayer!=null && sr!= null){
                SpriteToOrderInLayer.sortingOrder =sr.sortingOrder;
            }
    }
}
