using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour {

    private float startPositionGuiFromScreenWidthInPercent;     //Startwert für X in Prozent um Bildschirmgrößen anzupassen
    private bool showGui;

    void Start()
    {
        startPositionGuiFromScreenWidthInPercent = 41.5F;       //x-wert 41.5%
        showGui = true;
    }

    void OnGUI()
    {
        if (showGui == true)
        {
            GUI.skin.label.fontSize = 150;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUI.skin.box.fontSize = 20;

            GUI.Box(new Rect((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent, (Screen.height / 100) * 11, 40, 35), "1.");
            GUI.Box(new Rect((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent, (Screen.height / 100) * 21, 40, 35), "2.");
            GUI.Box(new Rect((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent, (Screen.height / 100) * 31, 40, 35), "3.");
            GUI.Box(new Rect((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent, (Screen.height / 100) * 41, 40, 35), "4.");
            GUI.Box(new Rect((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent, (Screen.height / 100) * 51, 40, 35), "5.");

            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40, (Screen.height / 100) * 11, 250, 35), PlayerPrefs.GetString("HighScoreNameSave01"));
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40, (Screen.height / 100) * 21, 250, 35), PlayerPrefs.GetString("HighScoreNameSave02"));
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40, (Screen.height / 100) * 31, 250, 35), PlayerPrefs.GetString("HighScoreNameSave03"));
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40, (Screen.height / 100) * 41, 250, 35), PlayerPrefs.GetString("HighScoreNameSave04"));
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40, (Screen.height / 100) * 51, 250, 35), PlayerPrefs.GetString("HighScoreNameSave05"));

            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40 + 250, (Screen.height / 100) * 11, 60, 35), PlayerPrefs.GetInt("HighScorePointsSave01").ToString());
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40 + 250, (Screen.height / 100) * 21, 60, 35), PlayerPrefs.GetInt("HighScorePointsSave02").ToString());
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40 + 250, (Screen.height / 100) * 31, 60, 35), PlayerPrefs.GetInt("HighScorePointsSave03").ToString());
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40 + 250, (Screen.height / 100) * 41, 60, 35), PlayerPrefs.GetInt("HighScorePointsSave04").ToString());
            GUI.Box(new Rect(((Screen.width / 100) * startPositionGuiFromScreenWidthInPercent) + 40 + 250, (Screen.height / 100) * 51, 60, 35), PlayerPrefs.GetInt("HighScorePointsSave05").ToString());
        }
    }

    public void resetHightscore()
    {
        PlayerPrefs.SetInt("HighScorePointsSave01", 0);
        PlayerPrefs.SetInt("HighScorePointsSave02", 0);
        PlayerPrefs.SetInt("HighScorePointsSave03", 0);
        PlayerPrefs.SetInt("HighScorePointsSave04", 0);
        PlayerPrefs.SetInt("HighScorePointsSave05", 0);

        PlayerPrefs.SetString("HighScoreNameSave01", "");
        PlayerPrefs.SetString("HighScoreNameSave02", "");
        PlayerPrefs.SetString("HighScoreNameSave03", "");
        PlayerPrefs.SetString("HighScoreNameSave04", "");
        PlayerPrefs.SetString("HighScoreNameSave05", "");
    }

    public void LoadByIndex(int sceneIndex)
    {
        showGui = false;
        SceneManager.LoadScene(sceneIndex);
    }
}
