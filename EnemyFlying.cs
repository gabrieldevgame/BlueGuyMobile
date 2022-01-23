using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    public float speed;

    public Transform currentTarget;
    public Transform targetA;
    public Transform targetB;

    SpriteRenderer spriteRenderer;

    public Transform HeadPoint;

    public BoxCollider2D boxCollider2D;
    public BoxCollider2D triggerboxCollider2D;
    public CircleCollider2D circleCollider2D;

    private Animator anim;
    private AudioSource sound;

    public float enemyHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = targetA;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == targetA && transform.position == targetA.position){
            currentTarget = targetB;
        }
        if(currentTarget == targetB && transform.position == targetB.position){
            currentTarget = targetA;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if(transform.position.x > currentTarget.position.x){
            spriteRenderer.flipX = false;
        }
        else{
            spriteRenderer.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D Coll) {
        if(Coll.gameObject.tag == "Player"){
            float HeadHeight = Coll.contacts[0].point.y - HeadPoint.position.y;

            if(HeadHeight > 0){
                Coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                speed = 0;
                sound.Play();
                anim.SetTrigger("Die");
                triggerboxCollider2D.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D Coll) {
        if(Coll.gameObject.tag == "Player"){
            anim.SetTrigger("Spike");
        }

        if(Coll.gameObject.tag == "Bullet"){
            if(enemyHealth > 0){
                enemyHealth -= 1;
                anim.SetTrigger("Hit");
            }
            else if(enemyHealth <= 0){
                speed = 0;
                sound.Play();
                anim.SetTrigger("Die");
                boxCollider2D.enabled = false;
                triggerboxCollider2D.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
