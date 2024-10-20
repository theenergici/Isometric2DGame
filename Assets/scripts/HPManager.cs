using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class HPManager : MonoBehaviour, IHittable
{
    // Start is called before the first frame update
    [SerializeField]
    public float MAX_HP=100.0f;
    [SerializeField]
    Slider HP_visual;
    private HP hp;
    [SerializeField]
    private GameObject ParentObject;


    private void Awake() {
        hp= new HP(MAX_HP);
        if(HP_visual==null)
            HP_visual = GetComponentInChildren<Slider>();
        if(ParentObject==null)
            ParentObject=gameObject;
    }

    public void OnDeath()
    {   
        HP_visual.gameObject.SetActive(false);
        ParentObject.SetActive(false);
        Debug.Log($"Chracter {name} is dead\n");

    }

    public bool OnHit(float dmg)
    {   
        
        bool isDead = hp.OnHit(dmg);
        HP_visual.value = hp.currentHP/hp.MaxHP;
        
        return isDead;
    }

    
}
