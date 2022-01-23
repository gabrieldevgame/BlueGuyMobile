using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMan : MonoBehaviour
{
    private Rigidbody2D RigBody;
    private Animator Anim;
    private AudioSource sound;

    public float Speed;

    public Transform RightCollisor;
    public Transform LeftCollisor;

    public Transform HeadPoint;

    private bool EnemyCollision;

    public LayerMask LayerCollisor;

    public BoxCollider2D boxCollider2D;
    public BoxCollider2D triggerboxCollider2D;
    public CircleCollider2D circleCollider2D;

    public float enemyHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        RigBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RigBody.velocity = new Vector2(Speed, RigBody.velocity.y);

        EnemyCollision = Physics2D.Linecast(RightCollisor.position, LeftCollisor.position, LayerCollisor);

        if(EnemyCollision){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            Speed = -Speed;
        }
    }


    void OnCollisionEnter2D(Collision2D Coll) {
        if(Coll.gameObject.tag == "Player"){
            float HeadHeight = Coll.contacts[0].point.y - HeadPoint.position.y;

            if(HeadHeight > 0){
                Coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                Speed = 0;
                sound.Play();
                Anim.SetTrigger("Die");
                boxCollider2D.enabled = false;
                triggerboxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                RigBody.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.5f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D Coll) {
        if(Coll.gameObject.tag == "Bullet"){
            if(enemyHealth > 0){
                enemyHealth -= 1;
                Anim.SetTrigger("Hit");
            }
            else if(enemyHealth <= 0){
                Speed = 0;
                sound.Play();
                Anim.SetTrigger("Die");
                boxCollider2D.enabled = false;
                triggerboxCollider2D.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
