using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    [SerializeField] Button[] gameButtons;
    [SerializeField] TMP_Text[] gameButtonTexts;
    [SerializeField] TMP_Text playerX;
    [SerializeField] TMP_Text playerO;
    [SerializeField] GameObject winnerX;
    [SerializeField] GameObject winnerO;
    [SerializeField] GameObject winnerDraw;
    [SerializeField] TMP_Text xScoreText;
    [SerializeField] TMP_Text oScoreText;
    int xScore;
    int oScore;
    string playMode;
    string playType;
    string vsWho;
    int turnNumber;
    string currentSymbol;
    string winner;
    bool isContinuing;
    LineRend lineRenderer;
    Color color1 = new Color(255f/255f,255f/255f,255f/255f,255f/255f);
    Color color2 = new Color(255f/255f,255f/255f,255f/255f,100f/255f);


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////
    
    void Start()
    {
        // Initialize the fields.
        turnNumber = 1;
        isContinuing = true;
        playerX.color = color1;
        playerO.color = color2; 
        lineRenderer = FindObjectOfType<LineRend>();
        currentSymbol = "X";
        playMode = PlayerPrefs.GetString("PlayMode", "DUEL");
        playType = PlayerPrefs.GetString("PlayType", "3x3");
        vsWho    = PlayerPrefs.GetString("VsWho", "VS PLAYER");
        Debug.Log("Playmode: " + playMode);
        Debug.Log("Playtype: " + playType);

        // If the playmode is "BEST OF 3", make the scoreboards
        // visible.
        if(playMode == "BEST OF 3"){
            // Get the scores.
            xScore = PlayerPrefs.GetInt("xScore", 0);
            oScore = PlayerPrefs.GetInt("oScore", 0);
            Debug.Log("xSCore: " + xScore + " oScore: " + oScore);
            // Update the score board.
            xScoreText.text = xScore.ToString();
            oScoreText.text = oScore.ToString();
            // Make the scoreboard visible.
            xScoreText.gameObject.SetActive(true);
            oScoreText.gameObject.SetActive(true);
        }
    }

    void Update() {
        // If versus Computer, make the computer play.
        if(vsWho == "VS COMPUTER"){
            if(isContinuing && GetTurnNumber() % 2 == 0){
                MakeComputerPlay();
                Debug.Log("It's mah turn");
            }
        }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to change the current symbol, 
    // according to the turn number.
    public string ChangeCurrentSymbol(){
        if(GetTurnNumber() % 2 == 0){
            currentSymbol = "O";
            playerX.color = color1;
            playerO.color = color2;   
        }
        else{
            currentSymbol = "X";
            playerX.color = color2;
            playerO.color = color1;  
        }
        return currentSymbol;
    }

    // This method is to increase the turn number.
    public void IncreaseTurn(){
        turnNumber++;
    }

    // Getter method for the turnNumber;
    public int GetTurnNumber(){
        return turnNumber;
    }

    // This method is to check if all the squares are filled.
    bool IsAllFilled(){
        foreach(TMP_Text gameButtonText in gameButtonTexts){
            if(gameButtonText.text == "")
                return false;
        }
        return true;
    }

    // This method is to stop the game by setting isContinuing 
    // to false.
    public void StopGame(){
        isContinuing = false;
    }

    // Getter for isContinuing.
    public bool GetIsContinuing(){
        return isContinuing;
    }

    // This method is to compare the three values at given indexes.
    bool CompareElements3x3(int index1, int index2, int index3){
        if(gameButtonTexts[index1].text != "" &&
           gameButtonTexts[index2].text != "" &&
           gameButtonTexts[index3].text != ""){
            if(gameButtonTexts[index1].text == gameButtonTexts[index2].text &&
               gameButtonTexts[index1].text == gameButtonTexts[index3].text){
               // Draw a line.
               Transform[] positions = {gameButtonTexts[index1].transform, gameButtonTexts[index3].transform};
               lineRenderer.DrawLine(positions);
               return true;
            }
            else
                return false;
        }
        else
            return false;  
    }
    
    // This method is to compare the five values at given indexes.
    bool CompareElements5x5(int index1, int index2, int index3, int index4, int index5){
        if(gameButtonTexts[index1].text != "" &&
           gameButtonTexts[index2].text != "" &&
           gameButtonTexts[index3].text != "" &&
           gameButtonTexts[index4].text != "" &&
           gameButtonTexts[index5].text != ""){
            if(gameButtonTexts[index1].text == gameButtonTexts[index2].text &&
               gameButtonTexts[index1].text == gameButtonTexts[index3].text &&
               gameButtonTexts[index1].text == gameButtonTexts[index4].text &&
               gameButtonTexts[index1].text == gameButtonTexts[index5].text){
               // Draw a line.
               Transform[] positions = {gameButtonTexts[index1].transform, gameButtonTexts[index5].transform};
               lineRenderer.DrawLine(positions);
               return true;
            }
            else
                return false;
        }
        else
            return false;  
    }

    // This method is to activate the winner message, 
    // according to the winner (or draw).
    void ActivateWinnerMessage(string winner){
        if(winner == "X")
            winnerX.SetActive(true);
        else if(winner == "O")
            winnerO.SetActive(true);
        else if(winner == "draw")
            winnerDraw.SetActive(true);
    }

    // This method is to arrange the winner. It'll act as a helper function
    // inside the "CheckTheWinner" functions.
    string ArrangeWinner(string win){
        winner = win;
        Debug.Log("(GameHandler) The winner is: " + " " + winner);
        if(playMode == "BEST OF 3"){
            winner = CheckWinnerMatch(winner);
        }
        ActivateWinnerMessage(winner);
        return winner;    
    }

    // This method is to check the winner for 3x3 play type.
    string CheckWinner3x3(){
        // Check columns, rows and diagonals for winners.
        for(int i = 0; i < gameButtonTexts.Length; i++){
            // Check columns.
            if(i % 3 == 0){
                if(CompareElements3x3(i, i+1,i+2)){
                    return ArrangeWinner(currentSymbol);
                }
            }
            // Check rows.
            if(i == 0 || i == 1 || i == 2){
                if(CompareElements3x3(i, i+3, i+6)){
                    return ArrangeWinner(currentSymbol);   
                }
            }
            // Check diagonals.
            if(i == 0){
                if(CompareElements3x3(i, i+4, i+8)){
                    return ArrangeWinner(currentSymbol);   
                }
            }
            if(i == 2){
                if(CompareElements3x3(i,i+2,i+4)){
                    return ArrangeWinner(currentSymbol);  
                }
            }
        }
        // Check if all squares are filled. (for draw)
        if(IsAllFilled()){
            return ArrangeWinner("draw");
        }

        // The game is still continuing.
        Debug.Log("Game still continuing...");
        return "Game still continuing...";
    }

    // This method is to check the winner for 5x5 play type.
    string CheckWinner5x5(){
        // Check columns, rows and diagonals for winners.
        for(int i = 0; i < gameButtonTexts.Length; i++){
            // Check columns.
            if(i % 5 == 0){
                if(CompareElements5x5(i,i+1,i+2,i+3,i+4)){
                    return ArrangeWinner(currentSymbol); 
                }
            }
            // Check rows.
            if(i == 0 || i == 1 || i == 2 || i == 3 || i == 4){
                if(CompareElements5x5(i,i+5,i+10,i+15,i+20)){
                    return ArrangeWinner(currentSymbol);  
                }
            }
            // Check diagonals.
            if(i == 0){
                if(CompareElements5x5(i,i+6,i+12,i+18,i+24)){
                    return ArrangeWinner(currentSymbol);   
                }
            }
            if(i == 4){
                if(CompareElements5x5(i,i+4,i+8,i+12,i+16)){
                    return ArrangeWinner(currentSymbol); 
                }
            }
        }

        // Check if all squares are filled. (for draw)
        if(IsAllFilled()){
            return ArrangeWinner("draw");
        }
        
        // The game is still continuing.
        Debug.Log("Game still continuing...");
        return "Game still continuing...";
    }

    // This method is to check if there's a winner. By calling
    // CheckWinner3x3 or CheckWinner5x5 according to the game mode.
    public string CheckWinner(){
        if(playType == "3x3")
           return CheckWinner3x3();
        else
            return CheckWinner5x5();
    }

    // This method is to check the score for BEST OF 3s.
    string CheckWinnerMatch(string winner){
        // If the turn's winner is X, increase it on playerprefs.
        if(winner == "X"){
            xScore = PlayerPrefs.GetInt("xScore", 0);
            xScore++;
            PlayerPrefs.SetInt("xScore", xScore); 
        }
        // If the turn's winner is O, increase it on playerprefs.
        else if(winner == "O"){
            oScore = PlayerPrefs.GetInt("oScore", 0);
            oScore++;
            PlayerPrefs.SetInt("oScore", oScore); 
        }
        // If it's a draw, increase both.
        else if(winner == "draw"){
            xScore = PlayerPrefs.GetInt("xScore", 0);
            xScore++;
            oScore = PlayerPrefs.GetInt("oScore", 0);
            oScore++;
            PlayerPrefs.SetInt("xScore", xScore); 
            PlayerPrefs.SetInt("oScore", oScore);
        }
        // Get the updated xScore and oScore values.
        xScore = PlayerPrefs.GetInt("xScore", 0);
        oScore = PlayerPrefs.GetInt("oScore", 0);
        // Update them on the scoreboard.
        xScoreText.text = xScore.ToString();
        oScoreText.text = oScore.ToString();
        // If both scores are 2, then it is a draw.
        if(xScore == 2 && oScore == 2){
            winner = "draw";
            return winner;
        }
        // If any one of them is equal to 2, then the 
        // winner of the "BEST OF 3" is decided.
        else if(xScore == 2 || oScore == 2){
            winner = currentSymbol;
            return winner;
        }
        // If not, return the turn's winner.
        else{
            winner = "This turn's winner is " + winner;
            return winner;
        }
    }    

    // This method is to make the Computer choose a random
    // square and fill it with "O".
    void MakeComputerPlay(){
        int index;
        bool filled = false;

        // Check for a winning index, and if there's, draw an O.
        if(playType == "3x3"){
            index = CheckWinningMove3x3();
            if(index != 99){
                Debug.Log("huh! I found a winning index: " + index);
                gameButtons[index].GetComponent<GameButtons>().DrawXorO();
                filled = true;
            }
        }
        else if(playType == "5x5"){
            index = CheckWinningMove5x5();
            if(index != 99){
                Debug.Log("huh! I found a winning index: " + index);
                gameButtons[index].GetComponent<GameButtons>().DrawXorO();
                filled = true;
            }
        }

        // If there's no winning index, fill a random one.
        if(!filled){
            // A list to keep empty square indexes.
            List<int> squareIndexes = new List<int>();
            // Get the empty indexes.
            for(int i = 0; i < gameButtonTexts.Length; i++){
                if(gameButtonTexts[i].text == "")
                    squareIndexes.Add(i);
            }
            for(int i = 0; i < squareIndexes.Count; i++){
                Debug.Log(squareIndexes[i]);
            }
            // Get a random index value to choose a random square.
            index = Random.Range(0, squareIndexes.Count);
            Debug.Log("Im gonna fuck up the index: " + index);
            // Call DrawXorO from a random button.
            gameButtons[squareIndexes[index]].GetComponent<GameButtons>().DrawXorO();
        }
    }

    // This method is to check if there's a possible winning move for 3x3.
    int CheckWinningMove3x3(){
        int index;
        // Check for a winning move and return the winning index,
        // if there's any.
        for(int i = 0; i < gameButtonTexts.Length; i++){
            // Check columns for a winning move.
            if(i % 3 == 0){
                index = Compare2Elements(i, i+1, i+2);
                if(index != 99)
                    return index;
            }
            // Check rows for a winning move.
            if(i == 0 || i == 1 || i == 2){
                index = Compare2Elements(i, i+3, i+6);
                if(index != 99)
                    return index;
            }
            // Check diagonals for a winning move.
            if(i == 0){
                index = Compare2Elements(i, i+4, i+8);
                if(index != 99)
                    return index;
            }
            if(i == 2){
                index = Compare2Elements(i,i+2,i+4);
                if(index != 99)
                    return index;
            }
        }
        // If there's no winning index, return 99.
        return 99;
    }


    // This method is to check if there's a possible winning move for 5x5.
    int CheckWinningMove5x5(){
        int index;
        for(int i = 0; i < gameButtonTexts.Length; i++){
            // Check columns for a winning move.
            if(i % 5 == 0){
                index = Compare4Elements(i,i+1,i+2,i+3,i+4);
                if(index != 99)
                    return index;
            }
            // Check rows for a winning move.
            if(i == 0 || i == 1 || i == 2 || i == 3 || i == 4){
                index = Compare4Elements(i,i+5,i+10,i+15,i+20);
                if(index != 99)
                    return index;
            }
            // Check diagonals for a winning move.
            if(i == 0){
                index = Compare4Elements(i,i+6,i+12,i+18,i+24);
                if(index != 99)
                    return index;
            }
            if(i == 4){
                index = Compare4Elements(i,i+4,i+8,i+12,i+16);
                if(index != 99)
                    return index;
            }
        }
        // If there's no winning index, return 99.
        return 99;
    }

    // This method is to compare two given indexes.
    int Compare2Elements(int index1, int index2, int index3){

        List<int> indexes = new List<int>() {index1, index2, index3};
        List<int> filledIndexes = new List<int>();
        List<int> unfilledIndexes = new List<int>();

        // Check which indexes are filled and put them to the relevant list.
        for(int i = 0; i < indexes.Count; i++){
            if(gameButtonTexts[indexes[i]].text != ""){
                filledIndexes.Add(indexes[i]);
            }
            else
                unfilledIndexes.Add(indexes[i]);
        }

        // If 2 of them are filled with the same symbol, get the unfilled one.
        if(filledIndexes.Count == 2){
            if(gameButtonTexts[filledIndexes[0]].text == gameButtonTexts[filledIndexes[1]].text){
                Debug.Log("unfilled index: " + unfilledIndexes[0]);
                return unfilledIndexes[0];
            }
            else
                return 99;
        }
        // If not, return 99.
        else
            return 99;
    }

    // This method is to compare four given indexes.
    int Compare4Elements(int index1, int index2, int index3, int index4, int index5){

        List<int> indexes = new List<int>() {index1, index2, index3, index4, index5};
        List<int> filledIndexes = new List<int>();
        List<int> unfilledIndexes = new List<int>();

        // Check which indexes are filled and put them to the relevant list.
        for(int i = 0; i < indexes.Count; i++){
            if(gameButtonTexts[indexes[i]].text != ""){
                filledIndexes.Add(indexes[i]);
            }
            else
                unfilledIndexes.Add(indexes[i]);
        }

        // If 4 of them are filled with the same symbol, get the unfilled one.
        if(filledIndexes.Count == 4){
            int counter = 0;
            // The symbol of the first index.
            string str = gameButtonTexts[filledIndexes[0]].text;

            // Check how many of the indexes are the same.
            for(int i = 1; i < filledIndexes.Count; i++){
                if(gameButtonTexts[filledIndexes[i]].text == str)
                    counter++;               
            }

            // If all of them are the same, return the empty index.
            if(counter == 3){
                Debug.Log("unfilled index: " + unfilledIndexes[0]);
                return unfilledIndexes[0];
            }
            // If not, return 99.
            else
                return 99;
        }

        // If not, return 99.
        else
            return 99;
    }
}


    