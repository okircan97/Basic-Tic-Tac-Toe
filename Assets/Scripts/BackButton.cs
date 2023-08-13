using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    
    SceneHandler sceneHandler;


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to put the game type (duel or match)
    // to the player prefs and load game type.
    public void LoadMainMenu(){
        sceneHandler.LoadMainMenu();
    }
}
