using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoss : MonoBehaviour
{
    public float speed;

    private float platformXRange = 2000f;
    private float platformYRange = 2000f;
    private Transform target;
    private Vector3 playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = target.transform.position - transform.position;
        if(Mathf.Abs(playerDistance.x) < platformXRange && Mathf.Abs(playerDistance.y) < platformYRange){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
