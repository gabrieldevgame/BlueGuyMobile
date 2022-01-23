using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public bool isRight;

    public Transform bullet;
    public Transform pivot;
    private float bulletTime;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletTime = bulletTime + Time.deltaTime;
        if(bulletTime > 1.5){
            Instantiate(bullet, pivot.position, transform.rotation);
            bulletTime = 0;
        }
    }
}
