using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class PlayerMonobehaviour : MonoBehaviour
{

       
    
    [HeaderAttribute("Speeds")]

    [TextArea(3,1000), Tooltip("This is just a description it doesn't do anything to the code.")]
    public string speedDescription= "The NS and EW speed have been adjusted in code to the ration used"+ 
                                        "by default in the isometric view.\n"+
                                        "\tIf this ratio is changed it should be changed in the code as well.";
   
   
    [MinAttribute(0.0f), Tooltip("North-South speed")]
    public float NS_Speed = 5.0f;
    [MinAttribute(0.0f), Tooltip("East-West speed")]
    public float EW_Speed = 5.0f;

    [SerializeField]
    float MaxJumpHeight = 1;

    private const float isoRatio = 2.0f;
    private Vector2 inputMove;
    private Vector2 scalingVector;
    private Rigidbody2D rb;
    private AttackScript AttackScript;


    [SerializeField]
    Animator viewAnimator;
    
    public Direction ViewDirection{get; private set;} 
    
    public MyTile currentTile;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //due to the isometric view the EW direction should be 2*NS direction to keep the correct perspective
        scalingVector = new Vector2(isoRatio * EW_Speed, NS_Speed);
        AttackScript = GetComponentInChildren<AttackScript>();
        if(viewAnimator==null)viewAnimator= GetComponent<Animator>();
        
        PlayerConstants.jumpHeight= MaxJumpHeight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var newpos =rb.position + Time.fixedDeltaTime*inputMove*scalingVector;
        var t =MapManager.Instance.getTileFromWorldPosition(newpos);
        

        if(t!= null && !t.isBlocked){
            currentTile= t;
            rb.MovePosition(newpos);
            transform.position= new Vector3(transform.position.x, transform.position.y, t.transform.position.z);
            var renderer = GetComponent<SpriteRenderer>();
            var tileRenderer = t.GetComponent<SpriteRenderer>();
            if (renderer!=null && tileRenderer!= null)
                renderer.sortingOrder = tileRenderer.sortingOrder;
        }
            // probably should change for a better option not to call get component as often
        
              

        
        
        
       

    }

    public void OnMovement(InputValue value){

        Vector2 t = value.Get<Vector2>();
        inputMove = VectorMath.rotateVector(t, -Mathf.PI/4);
        
        var d = DirectionMap.Map(inputMove);
        
        if(d!= Direction.origin){
            // Debug.Log(d);
            ViewDirection = d; 
            AttackScript?.changeDirection(d);
            if(viewAnimator!=null){
                viewAnimator.SetInteger("Direction", (int)d);
            }
        }       
            


        //Comentable if we dont want it to be changeable at run time
        PlayerConstants.jumpHeight= MaxJumpHeight;
        
    }



}
