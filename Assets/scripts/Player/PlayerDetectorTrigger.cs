using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerDetectorTrigger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _bodyRenderer;
    [SerializeField]
    private SpriteRenderer _viewRenderer;
    private PlayerMonobehaviour _player=null;
    public PlayerMonobehaviour Player{get{
        return _player;
    }}
    public bool playerInDetectionRange {get;private set;}
    private Coroutine runningCoroutine= null;
    void Awake() {
        if(_bodyRenderer==null)
            _bodyRenderer = GetComponent<SpriteRenderer>();
        if(_viewRenderer==null)
            _viewRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void LateUpdate() {
        var t =MapManager.Instance.getTileFromWorldPosition(transform.position);
        var tileRenderer = t?.GetComponent<SpriteRenderer>();
            if (_viewRenderer!=null && tileRenderer!= null)
                _viewRenderer.sortingOrder = tileRenderer.sortingOrder+1;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.GetComponent<PlayerMonobehaviour>()){
            if(_player==null)_player= other.GetComponentInChildren<PlayerMonobehaviour>();
            if(runningCoroutine!= null){
                StopCoroutine(runningCoroutine);
            }
            playerInDetectionRange = true;
            
        }         
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if(other.GetComponent<PlayerMonobehaviour>()){
            // Debug.Log($"Player lost: {other.name}");
            runningCoroutine = StartCoroutine(PlayerOutOfVision());
            
            }
    }         

    private IEnumerator PlayerOutOfVision(){

        Color orange= new Color(.99f, 0.4f, 0.0f, 1.0f);// color orange
       _bodyRenderer.color= Color.yellow;
       yield return new WaitForSecondsRealtime(0.4f);
       _bodyRenderer.color= orange;
       yield return new WaitForSecondsRealtime(0.4f);
       _bodyRenderer.color= Color.yellow;
       yield return new WaitForSecondsRealtime(0.4f);
       _bodyRenderer.color= orange;
       yield return new WaitForSecondsRealtime(0.4f);
       _bodyRenderer.color= Color.yellow;
       yield return new WaitForSecondsRealtime(0.4f);


       runningCoroutine=null;
       playerInDetectionRange = false;  
        
    }   

    public PlayerMonobehaviour ForceGetPlayer(){

       var _player = FindObjectsOfType<PlayerMonobehaviour>();

       if(_player.Count() >0) return _player[0];
       
       return null;
    }
    
}
