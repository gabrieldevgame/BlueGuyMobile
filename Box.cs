using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Animator Anim;

    public float JumpForce;
    public bool IsUp;

    public float BoxHealth;
    public GameObject BoxBreak;

    private AudioSource sound;


    void Start() {
        Anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    void UpDate(){
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            if(IsUp){
                sound.Play();
                Anim.SetTrigger("BoxHit");
                BoxHealth--;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            }
            else{
                sound.Play();
                Anim.SetTrigger("BoxHit");
                BoxHealth--;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -JumpForce), ForceMode2D.Impulse);
            }
        }

        if(BoxHealth <= 0){
            Destroy(transform.parent.gameObject);
            Instantiate(BoxBreak, transform.position, transform.rotation);
        }
    }
}