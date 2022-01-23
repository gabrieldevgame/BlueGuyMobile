using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItens : MonoBehaviour
{

    private SpriteRenderer SpriteR;
    private CircleCollider2D CircleC;
    private AudioSource sound;

    public GameObject Collected;

    // Start is called before the first frame update
    void Start()
    {
        SpriteR = GetComponent<SpriteRenderer>();
        CircleC = GetComponent<CircleCollider2D>();
        sound = GetComponent<AudioSource>();
    }
    
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player"){
            sound.Play();
            GameController.Instance.UpdateScoreText();
            
            SpriteR.enabled = false;
            CircleC.enabled = false;
            Collected.SetActive(true);
            Destroy(gameObject, 0.5f);
        }
    }
}
