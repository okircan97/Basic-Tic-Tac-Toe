using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayType : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    
    [SerializeField] TMP_Text playTypeText;
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

    // This method is to load the relevant game according
    // to the play type.
    public void LoadRelevantScene(){
        string playType = playTypeText.text;
        PlayerPrefs.SetString("PlayType", playType);
        Debug.Log(playType);
        sceneHandler.LoadGivenScene(playType);
    }

    // This method is to load the previous scene.
    public void LoadPreviousScene(){
        sceneHandler.LoadVsWho();
    }
}
