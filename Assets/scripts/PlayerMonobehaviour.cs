using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMonobehaviour : MonoBehaviour
{

        
    private const float MaxSpeed = 25.0f;
    [Range(0.0f, MaxSpeed)]
    public float NS_Speed = 5.0f;
    [Range(0.0f, MaxSpeed)]
    public float EW_Speed = 5.0f;
    private Vector2 inputMove;
    private Vector2 scalingVector;
    
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scalingVector = new Vector2(EW_Speed, NS_Speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.fixedDeltaTime*inputMove*scalingVector);
    }

    public void OnMovement(InputValue value){

        Vector2 t = value.Get<Vector2>();
        inputMove = VectorMath.rotateVector(t, -Mathf.PI/4);
    }



}
