using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public Text checkPointMessage;
    public string messageText;

    private bool isMessageActive = false;
    private float textTimer;

    private string currentScene;

    private GameObject player;
    private string loadScene;

    void Awake(){
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMessageActive){
            Color color = checkPointMessage.color;
            color.a += 2f * Time.deltaTime;
            checkPointMessage.color = color;
            if(color.a >= 1){
                isMessageActive = false;
                textTimer = 0;
            }
        }
        else if(!isMessageActive){
            textTimer += Time.deltaTime;
            if(textTimer >= 1f){
                Color color = checkPointMessage.color;
                color.a -= 2f * Time.deltaTime;
                checkPointMessage.color = color;
                if(color.a <= 0){
                    checkPointMessage.text = "";
                }
            }
        }
    }

    public void Save(){
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);

        PlayerPrefs.SetString("Scene", currentScene);
        PlayerPrefs.SetInt("Score", GameController.Instance.TotalScore);
        PlayerPrefs.SetInt("AdmobDeaths", AdmobManager.instance.deaths);
        PlayerPrefs.SetInt("AdmobLevels", AdmobManager.instance.levels);
    }

    public void Load(){
        Vector3 Pos = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
        transform.position = Pos;
    }

    public void InitPosition(){
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("y");
        PlayerPrefs.DeleteKey("z");
    }

    public void DeleteSaveGame(){
        PlayerPrefs.DeleteAll();
    }

    public void SetMessage(string message){
        checkPointMessage.text = message;
        Color color = checkPointMessage.color;
        color.a = 0;
        checkPointMessage.color = color;
        isMessageActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "CheckPoint"){
            Save();
            SetMessage(messageText);
            GameController.Instance.Score = 0;
        }
    }
}
