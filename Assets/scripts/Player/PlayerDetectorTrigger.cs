using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDetectorTrigger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    private PlayerMonobehaviour _player=null;
    public PlayerMonobehaviour Player{get{
        return _player;
    }}
    public bool playerInDetectionRange {get;private set;}
    private Coroutine runningCoroutine= null;
    void Awake() {
        if(_renderer==null)
            _renderer = GetComponent<SpriteRenderer>();
        
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.GetComponent<PlayerMonobehaviour>()){
            if(runningCoroutine!= null){
                StopCoroutine(runningCoroutine);
            }
            playerInDetectionRange = true;
            Debug.Log($"Detected: {other.name}");

            // since we work with only one player is not an issue
            if(_player==null)_player= other.GetComponentInChildren<PlayerMonobehaviour>();
        }         
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if(other.GetComponent<PlayerMonobehaviour>()){
            Debug.Log($"Player lost: {other.name}");
            runningCoroutine = StartCoroutine(PlayerOutOfVision());
            
            }
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
