using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerMenu : MonoBehaviour
{
    public GameObject newGameConfirm;
    public Text gameNotFoundText;
    public string messageText;

    public bool isInitialMenu;

    public static GameControllerMenu Instance;
    private bool isMessageActive = false;
    private float textTimer;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        sound = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if(isMessageActive){
            Color color = gameNotFoundText.color;
            color.a += 2f * Time.deltaTime;
            gameNotFoundText.color = color;
            if(color.a >= 1){
                isMessageActive = false;
                textTimer = 0;
            }
        }
        else if(!isMessageActive){
            textTimer += Time.deltaTime;
            if(textTimer >= 1f && !isInitialMenu){
                Color color = gameNotFoundText.color;
                color.a -= 2f * Time.deltaTime;
                gameNotFoundText.color = color;
                if(color.a <= 0){
                    gameNotFoundText.text = "";
                }
            }
        }
    }

    public void NewGameConfirm(){
        sound.Play();
        newGameConfirm.SetActive(true);
    }
    public void NewGameCancel(){
        sound.Play();
        newGameConfirm.SetActive(false);
    }

    public void SetMessage(string message){
        gameNotFoundText.text = message;
        Color color = gameNotFoundText.color;
        color.a = 0;
        gameNotFoundText.color = color;
        isMessageActive = true;
    }

    public void LoadScene(){
        if(PlayerPrefs.GetString("Scene") == ""){
            sound.Play();
            SetMessage(messageText);
            return;
        }
        else if(PlayerPrefs.GetString("Scene") != ""){
            sound.Play();
            AdmobManager.instance.deaths = PlayerPrefs.GetInt("AdmobDeaths");
            AdmobManager.instance.levels = PlayerPrefs.GetInt("AdmobLevels");
            SceneManager.LoadScene(PlayerPrefs.GetString("Scene"));
        }
    }

    public void GoToNextLevel(string LevelName){
        sound.Play();
        SceneManager.LoadScene(LevelName);
    }
}
