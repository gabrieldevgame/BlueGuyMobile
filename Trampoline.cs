using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

public class Trampoline : MonoBehaviour
{
    private Animator Anim;

    public float JumpForce;

    CharacterMovement2D playerMovement;

    void Start()
    {
        Anim = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement2D>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Anim.SetTrigger("TrampolineJump");
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}
