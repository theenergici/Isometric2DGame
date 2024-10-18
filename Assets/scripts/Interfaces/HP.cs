using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP 
{
    public float MaxHP{get;}
    public float currentHP{get; private set;}

    public HP(float maxHP){
        MaxHP= maxHP;
        currentHP = MaxHP;
    }

    public bool OnHit(float dmg){

        currentHP -= dmg;
        return currentHP<=0;

    }
    
    public void Heal(float health){
        this.OnHit(-health);
    }

}
