using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMonobehaviour : MonoBehaviour
{

        
    
    [HeaderAttribute("Speeds")]

    [TextArea(3,1000), Tooltip("This is just a description it doesn't do anything to the code."), readOnly()]
    public string speedDescription= "The NS and EW speed have been adjusted in code to the ration used"+ 
                                        "by default in the isometric view.\n"+
                                        "\tIf this ratio is changed it should be changed in the code as well.";
   
   
    [MinAttribute(0.0f), Tooltip("North-South speed")]
    public float NS_Speed = 5.0f;
    [MinAttribute(0.0f), Tooltip("East-West speed")]
    public float EW_Speed = 5.0f;

    private const float isoRatio = 2.0f;
    private Vector2 inputMove;
    private Vector2 scalingVector;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //due to the isometric view the EW direction should be 2*NS direction to keep the correct perspective
        scalingVector = new Vector2(isoRatio * EW_Speed, NS_Speed);
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
