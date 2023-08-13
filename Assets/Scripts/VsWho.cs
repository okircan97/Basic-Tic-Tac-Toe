using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VsWho : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    
    [SerializeField] TMP_Text vsWhoText;
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
        string vsWho = vsWhoText.text;
        PlayerPrefs.SetString("VsWho", vsWho);
        Debug.Log(vsWho);
        sceneHandler.LoadPlayType();
    }

    // This method is to load the previous scene.
    public void LoadPreviousScene(){
        sceneHandler.LoadPlayMode();
    }
}
