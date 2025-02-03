using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    PlayerHealth hp;
    public Text waveText;

    public void Setup(int wave)
    {
        gameObject.SetActive(true);
        waveText.text = wave.ToString() ;
    }
    void Start(){
        gameOverUI.SetActive(false);
        hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        //Start with game over UI off

    }
    void Update(){
        if (hp == null)
        {
                hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        }
        //When player dies the game over UI turns on
        if(hp.GetIsDowned()){
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void quitBtn(){
        //Exits game
        Debug.Log("test Exit Button");
        Application.Quit();
        
    }
    public void playAgainBtn(){
        //Reloads active scene
        Debug.Log("test Scene Reloaded");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
