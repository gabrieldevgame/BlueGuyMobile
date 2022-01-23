using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
using UnityStandardAssets.CrossPlatformInput;

public enum GroundType{
    None,
    Soft,
    Hard
}

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]

public class Player : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;


    public float jumpForce;

    public Rigidbody2D Rig;
    // public AudioSource dieSound;
    private AudioSource jumpSound;

    [Header("Audio")]
    [SerializeField] AudioCharacter audioPlayer = null;
    private LayerMask softGround;
    private LayerMask hardGround;
    private GroundType groundType;
    public CircleCollider2D coll2D;


    private Animator Anim;

    private bool IsFloating = false;

    public Transform bullet;
    public Transform pivot;

    private bool isRight = false;
    public bool IsRight{
        get{return isRight;}
    }
    private bool isLeft = false;
    public bool IsLeft{
        get{return isLeft;}
    }

    

    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        // dieSound = GetComponent<AudioSource>();
        jumpSound = GetComponent<AudioSource>();
        softGround = LayerMask.GetMask("Ground");
        hardGround = LayerMask.GetMask("Wall");
        coll2D = GetComponent<CircleCollider2D>();

        isRight = true;
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
            isRight = true;
            isLeft = false;
        }
        else if(movementInput.x < 0){
            spriteRenderer.flipX = true;
            Anim.SetBool("Walk", true);
            isLeft = true;
            isRight = false;
        }
        else if(movementInput.x == 0){
            Anim.SetBool("Walk", false);
        }


        //PULO
        if(playerInput.IsJumpButtonDown()){
            playerMovement.Jump();
            Anim.SetBool("Jump", true);
            jumpSound.Play();
        }

        //Bullet
        if(playerInput.IsAttackButtonDown()){
            Instantiate(bullet, pivot.position, transform.rotation);
        }

        UpdateGround();

        //Play audio
        if((movementInput.x > 0 || movementInput.x < 0) && (coll2D.IsTouchingLayers(softGround) || coll2D.IsTouchingLayers(hardGround))){
            audioPlayer.PlaySteps(groundType, Mathf.Abs(playerMovement.groundMaxSpeed));
        }
    }

    private void UpdateGround(){
        if(coll2D.IsTouchingLayers(softGround)){
            groundType = GroundType.Soft;
        }
        else if(coll2D.IsTouchingLayers(hardGround)){
            groundType = GroundType.Hard;
        }
        else{
            groundType = GroundType.None;
        }
    }


    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 13){
            Anim.SetBool("Jump", false);
        }

        if(collision.gameObject.layer == 9){
            Anim.SetBool("Jump", false);
        }
        
        if(collision.gameObject.layer == 10){
            Anim.SetBool("Jump", false);
        }

        if(collision.gameObject.tag == "Enemy"){
            Anim.SetBool("Jump", false);
        }
    }

    

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Spike"){
            GameController.Instance.ShowGameOver();
            Destroy(gameObject);
            AdmobManager.instance.deaths++;
            if(AdmobManager.instance.deaths >= 6){
                AdmobManager.instance.deaths = 0;
                AdmobManager.instance.ShowPopUp();
            }
        }
        
        if(collider.gameObject.tag == "Saw"){
            GameController.Instance.ShowGameOver();
            Destroy(gameObject);
            AdmobManager.instance.deaths++;
            if(AdmobManager.instance.deaths >= 6){
                AdmobManager.instance.deaths = 0;
                AdmobManager.instance.ShowPopUp();
            }
        }

        if(collider.gameObject.tag == "NextLevel"){
            GameController.Instance.ShowNextLevel();
            AdmobManager.instance.levels++;
            if(AdmobManager.instance.levels >= 4){
                AdmobManager.instance.levels = 0;
                AdmobManager.instance.ShowPopUp();
            }
        }

        if(collider.gameObject.tag == "Enemy"){
            GameController.Instance.ShowGameOver();
            Destroy(gameObject);
            AdmobManager.instance.deaths++;
            if(AdmobManager.instance.deaths >= 6){
                AdmobManager.instance.deaths = 0;
                AdmobManager.instance.ShowPopUp();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if(collider.gameObject.tag == "Fan"){
            Anim.SetBool("Jump", true);
            IsFloating = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.tag == "Fan"){
            IsFloating = false;
        }
    }
}
