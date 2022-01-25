using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerData{
    public int TotalScore;
    public float PlayerPosX, PlayerPosY;
    public int scene;
}
public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public int TotalScore;
    public float PlayerPosX, PlayerPosY;
    public int scene;

    private string filePath;

    // Start is called before the first frame update
    void Awake()
    {
        if(gm == null){
            gm = this;
        }
        else if(gm != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/gameInfo.dat";
    }

    public void Save(){
        TotalScore = GameController.Instance.TotalScore;

        BinaryFormatter bf = new BinaryFormatter();

    }
}
