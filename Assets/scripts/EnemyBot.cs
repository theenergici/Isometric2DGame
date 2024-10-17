using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    // Start is called before the first frame update
    private PolygonCollider2D vision;
    private SpriteRenderer _renderer;
    void Awake() {
        vision = GetComponent<PolygonCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        _renderer.color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D other) {
        _renderer.color = Color.yellow;
    }            
    

}
