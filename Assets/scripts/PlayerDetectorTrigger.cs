using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerDetectorTrigger : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public PlayerMonobehaviour _player{get; private set;}
    public bool playerInDetectionRange {get;private set;}
    void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerMonobehaviour>()){
            playerInDetectionRange = true;
            Debug.Log("player entered");
            // since we work with only one player is not an issue
            if(!_player)_player= other.GetComponent<PlayerMonobehaviour>();
        }         
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<PlayerMonobehaviour>())
            StartCoroutine(PlayerOutOfVision());
    }         

    private IEnumerator PlayerOutOfVision(){
        yield return new WaitForSecondsRealtime(2.0f);
        playerInDetectionRange = false;
        
    }   
    
}
