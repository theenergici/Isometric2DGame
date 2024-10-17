using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class PlayerDetectorTrigger : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public PlayerMonobehaviour _player{get; private set;}
    public bool playerInDetectionRange {get;private set;}
    private Coroutine runningCoroutine= null;
    void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerMonobehaviour>()){
            if(runningCoroutine!= null){
                StopCoroutine(runningCoroutine);
            }
            playerInDetectionRange = true;

            
            // since we work with only one player is not an issue
            if(!_player)_player= other.GetComponent<PlayerMonobehaviour>();
        }         
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<PlayerMonobehaviour>())
            runningCoroutine = StartCoroutine(PlayerOutOfVision());
    }         

    private IEnumerator PlayerOutOfVision(){

        Color orange= new Color(.99f, 0.4f, 0.0f, 1.0f);// color orange
       _renderer.color= Color.yellow;
       yield return new WaitForSecondsRealtime(0.4f);
       _renderer.color= orange;
       yield return new WaitForSecondsRealtime(0.4f);
       _renderer.color= Color.yellow;
       yield return new WaitForSecondsRealtime(0.4f);
       _renderer.color= orange;
       yield return new WaitForSecondsRealtime(0.4f);
       _renderer.color= Color.yellow;
       yield return new WaitForSecondsRealtime(0.4f);

 
        playerInDetectionRange = false;
        runningCoroutine=null;
        
    }   
    
}
