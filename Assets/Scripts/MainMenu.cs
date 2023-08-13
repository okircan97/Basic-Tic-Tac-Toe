using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{   
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    [SerializeField] TMP_Text tic;
    [SerializeField] TMP_Text tac;
    [SerializeField] TMP_Text toe;
    bool isWritten;

    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Update()
    {   
        WriteTTT();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to write down tic-tac-toe, and
    // delete it from the board over time.
    void WriteTTT(){
        if(!isWritten){           
            StartCoroutine(ChangeText(tic, "TIC", 0.5f));
            StartCoroutine(ChangeText(tac, "TAC", 1f));
            StartCoroutine(ChangeText(toe, "TOE", 1.5f));
            StartCoroutine(UpdateIsWritten(true, 1.5f));
        }
        else{
            StartCoroutine(ChangeText(tic, "", 0.5f));
            StartCoroutine(ChangeText(tac, "", 1f));
            StartCoroutine(ChangeText(toe, "", 1.5f));
            StartCoroutine(UpdateIsWritten(false, 1.5f));
        }
    }

    // A coroutine to change text over time.
    IEnumerator ChangeText(TMP_Text txt_obj, string str, float awaitTime){
        yield return new WaitForSeconds(awaitTime);
        txt_obj.text = str;
    }

    // This coroutine is to change the value of isWritten.
    IEnumerator UpdateIsWritten(bool t_or_f, float waitTime){
        yield return new WaitForSeconds(waitTime);
        isWritten = t_or_f;
    }
}
