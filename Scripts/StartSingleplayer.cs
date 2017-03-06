using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSingleplayer : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)
    {
        PlayerPrefs.SetInt("HowManyPlayers", 1);
        SceneManager.LoadScene(sceneIndex);
    }
}