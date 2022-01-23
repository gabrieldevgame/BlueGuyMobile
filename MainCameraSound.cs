using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraSound : MonoBehaviour
{
    public AudioSource lofiSound;
    public AudioSource rainSound;
    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        lofiSound = GetComponent<AudioSource>();
        rainSound = GetComponent<AudioSource>();
        buttonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OffLofi(){
        lofiSound.Stop();
        buttonSound.Play();
    }
    public void OnLofi(){
        lofiSound.Play();
        buttonSound.Play();
    }
    
    public void OffRain(){
        rainSound.Stop();
        buttonSound.Play();
    }
    public void OnRain(){
        rainSound.Play();
        buttonSound.Play();
    }
}
