using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float Speed;
    public float MoveTime;

    private bool DirRight = true;
    private float Timer;

    // Update is called once per frame
    void Update()
    {
        if(DirRight){
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        else{
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }

        Timer += Time.deltaTime;
        if(Timer >= MoveTime){
            DirRight = !DirRight;
            Timer = 0f;
        }
    }
}
