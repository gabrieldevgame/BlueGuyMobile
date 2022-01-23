using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float PlatformTime;

    private TargetJoint2D Target;
    private BoxCollider2D BoxColl;

    // Start is called before the first frame update
    void Start()
    {
        Target = GetComponent<TargetJoint2D>();
        BoxColl = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            Invoke("Falling", PlatformTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.layer == 7){
            Destroy(gameObject);
        }
    }

    void Falling(){
        Target.enabled = false;
        BoxColl.isTrigger = true;
    }
}
