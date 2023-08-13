using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerButtons : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    
    SceneHandler sceneHandler;
    PlayMode playMode;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
        Debug.Log(sceneHandler.name);
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to load the main menu.
    public void PleaseLoadMainMenuAmk(){
        sceneHandler.LoadMainMenu();
    }
}
