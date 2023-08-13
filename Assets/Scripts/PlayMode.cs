using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    
    [SerializeField] TMP_Text playModeText;
    SceneHandler sceneHandler;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to put the game type (Single or Best of 3)
    // to the player prefs and load VsWho.
    public void LoadPlayType(){
        string playMode = playModeText.text;
        PlayerPrefs.SetString("PlayMode", playMode);
        Debug.Log(playMode);
        sceneHandler.LoadVsWho();
    }
}
