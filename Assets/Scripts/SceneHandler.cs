using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    public SceneHandler sceneHandler;

    // Singleton design to not destroy the scene 
    // handler on load.
    void Start()
    {   
        if(sceneHandler != null){
            Destroy(gameObject);
        }

        else{
            sceneHandler = this;
            DontDestroyOnLoad(gameObject);
        }

        // Set target frame rate.
        Application.targetFrameRate = 30;
    }
    
    // Load main menu.
    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    // Load game.
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    // Load playMode.
    public void LoadPlayMode(){
        PlayerPrefs.SetInt("xScore", 0);
        PlayerPrefs.SetInt("oScore", 0);
        SceneManager.LoadScene("PlayMode");
    }

    // Load playType.
    public void LoadPlayType(){
        SceneManager.LoadScene("PlayType");
    }

    // Load 3xTicTacToe.
    public void LoadGame3(){
        SceneManager.LoadScene("3x3");
    }

    // Load 5xTicTacToe.
    public void LoadGame5(){
        SceneManager.LoadScene("5x5");
    }

    // Load credits.
    public void LoadCredits(){
        SceneManager.LoadScene("Credits");
    }
    
    // Load VsWho
    public void LoadVsWho(){
        SceneManager.LoadScene("VsWho");
    }
    
    // Load given scene.
    public void LoadGivenScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
