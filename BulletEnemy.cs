using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        ShootEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootEnemy(){
        rig.AddForce(transform.right * -500);
        spriteRenderer.flipX = true;
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 13){
            Destroy(gameObject);
        }
    }
}
