using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject CameraPlayer;
    private float ImgLength, ImgStartPosition;
    public float SpeedParallax;

    // Start is called before the first frame update
    void Start()
    {
        ImgStartPosition = transform.position.x;
        ImgLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixeUpdate()
    {
        float Temp = (CameraPlayer.transform.position.x * (1 - SpeedParallax));
        float Dist = (CameraPlayer.transform.position.x * SpeedParallax);

        transform.position = new Vector3 (ImgStartPosition + Dist, transform.position.y, transform.position.z);

        if(Temp > ImgStartPosition + ImgLength){
            ImgStartPosition += ImgLength;
        }
        else if(Temp < ImgStartPosition - ImgLength){
            ImgStartPosition -= ImgLength;
        }
    }
}
