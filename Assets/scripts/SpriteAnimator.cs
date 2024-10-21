using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    Color Original= Color.cyan;
    [SerializeField]
    Color OnHit = Color.red;
    private SpriteRenderer _sr;

    private void Awake() {
        _sr = GetComponent<SpriteRenderer>();
        if(_sr!=null)
            Original= _sr.color;

        
    }
    public void OnHitChangeColor(){
        StartCoroutine(ChangeColorOnHitCoroutine());
    }

    public void OnDeath(){
        Debug.Log($"Character {name} died");
    }


    private IEnumerator ChangeColorOnHitCoroutine(){
        var trans = transform.position;
        var movex = Random.Range(-0.02f, 0.02f);
        var movey = Random.Range(-0.02f, 0.02f);

        if(_sr!=null){
            _sr.color = OnHit;
            transform.position = new Vector3(transform.position.x + movex, transform.position.y + movey);
            yield return new WaitForSeconds(0.05f);
            transform.position = new Vector3(transform.position.x - movex, transform.position.y - movey);
            yield return new WaitForSeconds(0.05f);
            transform.position = new Vector3(transform.position.x - movex, transform.position.y - movey);
            yield return new WaitForSeconds(0.05f);        
            transform.position = new Vector3(transform.position.x + movex, transform.position.y + movey);
            yield return new WaitForSeconds(0.05f);
            _sr.color = Original;



        }
        yield return new WaitForEndOfFrame();
    }


}
