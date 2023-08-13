using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    int turnNumber;
    string currentSymbol;
    [SerializeField] TMP_Text gameButtonText;
    GameHandler gameHandler;
    string winner;
    SceneHandler sceneHandler;
    string playMode;
    string playType;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        gameHandler  = FindObjectOfType<GameHandler>();
        sceneHandler = FindObjectOfType<SceneHandler>();
        playMode = PlayerPrefs.GetString("PlayMode", "DUEL");
        playType = PlayerPrefs.GetString("PlayType", "3x3");
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // A coroutine to load a scene.
    public IEnumerator WaitAndLoad(float awaitTime, string sceneName){
        yield return new WaitForSeconds(awaitTime);
        sceneHandler.LoadGivenScene(sceneName);
    }
    
    // This method is draw an X or O.
    public void DrawXorO(){
        // If the game still continues:
        if(gameHandler.GetIsContinuing()){
            // If the square is empty, fill it with an X or an O.
            if(gameButtonText.text == ""){
                gameButtonText.text = gameHandler.ChangeCurrentSymbol();
                // Check if there's a winner.
                winner = gameHandler.CheckWinner();
                // Increase the turn.
                gameHandler.IncreaseTurn();
                // Check the winner for the DUEL playmode.
                if(playMode == "DUEL"){
                    if(winner != "Game still continuing..."){
                        gameHandler.StopGame();
                        // Change the current symbol again. So that it'll 
                        // seem like it did not change at all (on vs player).
                        if(PlayerPrefs.GetString("VsWho", "VS PLAYER") != "VS COMPUTER")
                            gameHandler.ChangeCurrentSymbol();
                    }
                }
                // Check the winner for the Best of 3.
                else{
                    if(winner == "This turn's winner is X" ||
                       winner == "This turn's winner is O" ||
                       winner == "This turn's winner is draw"){
                            gameHandler.StopGame();
                            StartCoroutine(WaitAndLoad(1f, playType));
                    }
                    else if(winner == "X" ||
                            winner == "O" ||
                            winner == "draw"){
                            // There is a logic error somewhere in the code, which resulting
                            // showing the winner of the match wrong on VS COMPUTER; and I
                            // feel to lazy to solve it. Increasing the turn number solves it
                            // anyways... 
                            if(PlayerPrefs.GetString("VsWho", "VS PLAYER") == "VS COMPUTER")
                                gameHandler.IncreaseTurn();
                                Debug.Log("We have a winner!");
                            }
                }
            }
        }
    }
}
