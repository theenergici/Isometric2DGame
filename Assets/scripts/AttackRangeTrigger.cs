using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeTrigger : MonoBehaviour
{
    public bool IsInAttackRange{get; private set;}
    private void OnTriggerEnter2D(Collider2D other) {
        IsInAttackRange= true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        IsInAttackRange= false;
    }
}
