using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text needMoreAppleMessage;
    public string messageText;

    private bool isMessageActive = false;
    private float textTimer;

    public int Score = 0;
    public Text ScoreText;

    public int TotalScore = 0;

    public GameObject GameOver;
    public GameObject NextLevel;
    public GameObject Menu;
    public GameObject AdmobMenu;
    public GameObject Levels;

    public bool dontNeedAppleScore;

    public static GameController Instance;

    public float scoreNeeded;

    private SaveData saveData;
    public AudioSource buttonSound;
    public AudioSource winSound;
    public AudioSource gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        saveData = FindObjectOfType<SaveData>();
        buttonSound = GetComponent<AudioSource>();
        winSound = GetComponent<AudioSource>();
        gameOverSound = GetComponent<AudioSource>();

        if(!dontNeedAppleScore){
            TotalScore = PlayerPrefs.GetInt("Score");
            ScoreText.text = TotalScore.ToString();
        }

        AdmobManager.instance.RequestBanner();
        // AdmobManager.instance.RequestInterstitial();
        // AdmobManager.instance.RequestRewardedAd();
    }
    
    void Update()
    {
        if(!dontNeedAppleScore){
            TotalScore = PlayerPrefs.GetInt("Score");
            ScoreText.text = TotalScore.ToString();
        }

        if(isMessageActive){
            Color color = needMoreAppleMessage.color;
            color.a += 2f * Time.deltaTime;
            needMoreAppleMessage.color = color;
            if(color.a >= 1){
                isMessageActive = false;
                textTimer = 0;
            }
        }
        else if(!isMessageActive){
            textTimer += Time.deltaTime;
            if(textTimer >= 3f && !dontNeedAppleScore){
                Color color = needMoreAppleMessage.color;
                color.a -= 2f * Time.deltaTime;
                needMoreAppleMessage.color = color;
                if(color.a <= 0){
                    needMoreAppleMessage.text = "";
                }
            }
        }
    }

    public void UpdateScoreText(){
        Score++;
        ScoreText.text = Score.ToString();
        TotalScore++;
        PlayerPrefs.SetInt("Score", TotalScore);
    }
    
    public void OpenMenu(){
        buttonSound.Play();
        Menu.SetActive(true);
    }
    public void CloseMenu(){
        buttonSound.Play();
        Menu.SetActive(false);
    }
    
    public void OpenAdmobMenu(){
        buttonSound.Play();
        AdmobMenu.SetActive(true);
    }
    public void CloseAdmobMenu(){
        buttonSound.Play();
        AdmobMenu.SetActive(false);
    }

    public void ShowLevels(){
        buttonSound.Play();
        Levels.SetActive(true);
    }

    public void ShowGameOver(){
        gameOverSound.Play();
        LostTotalScore();
        GameOver.SetActive(true);
    }

    public void LoadScene(){
        buttonSound.Play();
        SceneManager.LoadScene(PlayerPrefs.GetString("Scene"));
    }

    public void ShowNextLevel(){
        if(TotalScore < scoreNeeded){
            SetMessage(messageText);
        }
        else if(TotalScore >= scoreNeeded){
            NextLevel.SetActive(true);
            if(gameObject.tag == "Player"){
                winSound.Play();
            }
        }
    }

    public void GoToNextLevel(string LevelName){
        buttonSound.Play();
        SceneManager.LoadScene(LevelName);
    }

    public void DeleteSaveGame(){
        PlayerPrefs.DeleteAll();
    }

    public void SetMessage(string message){
        needMoreAppleMessage.text = message;
        Color color = needMoreAppleMessage.color;
        color.a = 0;
        needMoreAppleMessage.color = color;
        isMessageActive = true;
    }

    public void LostTotalScore(){
        TotalScore = TotalScore - Score;
        PlayerPrefs.SetInt("Score", TotalScore);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            saveData.InitPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            DeleteSaveGame();
            saveData.DeleteSaveGame();
        }
    }
}
