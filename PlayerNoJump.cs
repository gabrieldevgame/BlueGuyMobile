using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerNoJump : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;

    public Rigidbody2D Rig;


    private Animator Anim;

    

    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVIMENTAÇÃO
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput);

        //FLIP DO PLAYER e ANIMATION WALK
        if(movementInput.x > 0){
            spriteRenderer.flipX = false;
            Anim.SetBool("Walk", true);
        }
        else if(movementInput.x < 0){
            spriteRenderer.flipX = true;
            Anim.SetBool("Walk", true);
        }
        else if(movementInput.x == 0){
            Anim.SetBool("Walk", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "NextLevel"){
            GameController.Instance.ShowNextLevel();
            Destroy(gameObject);
        }
    }
}
