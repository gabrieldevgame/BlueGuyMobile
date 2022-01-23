using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(){
        if(FindObjectOfType<Player>().IsRight == true){
            GetComponent<Rigidbody2D>().AddForce(transform.right * 800);
            Destroy(gameObject, 0.45f);
        }
        else if(FindObjectOfType<Player>().IsLeft == true){
            GetComponent<Rigidbody2D>().AddForce(transform.right * -800);
            Destroy(gameObject, 0.45f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 13){
            Destroy(gameObject);
        }
    }
}
