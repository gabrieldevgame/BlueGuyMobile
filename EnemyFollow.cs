using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigBody;

    public float speed;
    public float distanceTarget;
    public float enemyXRange;
    public float enemyYRange;
    private Vector3 playerDistance;

    private Transform target;


    private Animator anim;
    private AudioSource sound;
    public AudioSource walkSound;

    public Transform HeadPoint;

    public BoxCollider2D boxCollider2D;
    public BoxCollider2D triggerboxCollider2D;
    public CircleCollider2D circleCollider2D;

    public float enemyHealth = 4;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigBody = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = target.transform.position - transform.position;
        if(Mathf.Abs(playerDistance.x) < enemyXRange && Mathf.Abs(playerDistance.y) < enemyYRange){
            if(transform.position.x > target.position.x){
                spriteRenderer.flipX = false;
            }
            else{
                spriteRenderer.flipX = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("Walk", true);
        }
        else if(Mathf.Abs(playerDistance.x) > enemyXRange && Mathf.Abs(playerDistance.y) > enemyYRange){
            anim.SetBool("Walk", false);
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
                boxCollider2D.enabled = false;
                triggerboxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D Coll) {
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

    public void ChickenWalk(){
        walkSound.Play();
    }
}
