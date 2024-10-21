using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"collided with:{other.name}");
        var o=other.GetComponentInChildren<IHittable>();
        if(o!= null){
            Debug.Log($"Hittable");
        }
    }
}
