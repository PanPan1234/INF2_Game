using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNameInput : MonoBehaviour {

    public string playerOneName = "Insert name player one";
    public string playerTwoName = "Insert name player two";
    private int HowManyPlayers;

    // Use this for initialization
    void Start ()
    {
        HowManyPlayers = PlayerPrefs.GetInt("HowManyPlayers");
    }

    void OnGUI()
    {
        GUI.skin.textField.fontSize = 20;
        playerOneName = GUI.TextField(new Rect(835, 350, 250, 35), playerOneName, 22);

        if (HowManyPlayers == 1)
        {
            playerTwoName = "Computer";
        }

        else if (HowManyPlayers == 2)
        {
            playerTwoName = GUI.TextField(new Rect(835, 400, 250, 35), playerTwoName, 22);
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        PlayerPrefs.SetString("PlayerOneName", playerOneName);      //In Anführungszeichen ist die SaveDatei, daneben der String(der Name), der oben angegeben wurde
        PlayerPrefs.SetString("PlayerTwoName", playerTwoName);      //In Anführungszeichen ist die SaveDatei, daneben der String(der Name), der oben angegeben wurde
        SceneManager.LoadScene(sceneIndex);
    }

}
