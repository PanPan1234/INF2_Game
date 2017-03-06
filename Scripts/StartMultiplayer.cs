using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMultiplayer : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        PlayerPrefs.SetInt("HowManyPlayers", 2);
        SceneManager.LoadScene(sceneIndex);
    }
}
