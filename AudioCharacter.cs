using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacter : MonoBehaviour
{
    [SerializeField] AudioSource footStepsAudioSource = null;
    [Header("Audio Clips")]
    [SerializeField] AudioClip[] softSteps = null;
    [SerializeField] AudioClip[] hardSteps = null;

    [Header("Steps")]
    [SerializeField] float timer = 0.5f;

    private float stepsTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySteps(GroundType groundType, float speedNormalized){
        if(groundType == GroundType.None){
            return;
        }

        stepsTimer += Time.fixedDeltaTime * speedNormalized;

        if(stepsTimer >= timer){
            var steps = groundType == GroundType.Hard ? hardSteps : softSteps;
            int index = Random.Range(0, steps.Length);
            footStepsAudioSource.PlayOneShot(steps[index]);

            stepsTimer = 0;
        }
    }
}
