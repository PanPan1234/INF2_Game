using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private Handanimations handanimations;
    private Handanimations2 handanimations2;
    public int InputPlayerOne; // 0 = Rock | 1 = Paper | 2 = Scissors | 3 = PlayerDidntPress or GameDidntStart
    public int InputPlayerTwo; // 0 = Rock | 1 = Paper | 2 = Scissors | 3 = PlayerDidntPress or GameDidntStart
    private int ScorePlayerOne;
    private int ScorePlayerTwo;
    public WaitForSeconds waitForInput;
    public WaitForSeconds waitForCountdown;
    public WaitForSeconds waitForAnimation;
    GameObject thePlayer1;          //Erstellung eines Gameobjects (um später auf Werte (0,1,2) zuzugreifen)
    GameObject thePlayer2;          //Erstellung eines Gameobjects (um später auf Werte (0,1,2) zuzugreifen)
    public bool QisPressable;
    private bool showGUI_Points = false;
    private bool showGUI_Three = false;
    private bool showGUI_Two = false;
    private bool showGUI_One = false;
    private bool showGUI_GO = false;
    private string PlayerOneName;
    private string PlayerTwoName;

    void Awake()
    {
        QisPressable = false;
        ScorePlayerOne = 0;
        ScorePlayerTwo = 0;
        InputPlayerOne = 3;
        InputPlayerTwo = 3;
        PlayerOneName = PlayerPrefs.GetString("PlayerOneName");
        PlayerTwoName = PlayerPrefs.GetString("PlayerTwoName");
    }

    // Use this for initialization
    void Start()
    {
        waitForInput = new WaitForSeconds(1.5F);
        waitForCountdown = new WaitForSeconds(1);
        waitForAnimation = new WaitForSeconds(7.5F);
        thePlayer1 = GameObject.Find("vr_cartoon_hand_prefab_Left");    //Gameobject mit linker Hand verknüpft
        thePlayer2 = GameObject.Find("vr_cartoon_hand_prefab right");   //Gameobject mit rechter Hand verknüpft
        handanimations = thePlayer1.GetComponent<Handanimations>();     //Gameobject mit Scripts verknüpft (Get.Component) um auf Zahlen zuzugreifen
        handanimations2 = thePlayer2.GetComponent<Handanimations2>();   //Gameobject mit Scripts verknüpft (Get.Component) um auf Zahlen zuzugreifen
        StartCoroutine(WaitForAnimationAtStart());
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && (QisPressable == true))
        {
            if (showGUI_Points == false)
            {
                showGUI_Points = true;
            }

            QisPressable = false;
            StartCoroutine(WaitCoroutine());
        }
    }
    
    void OnGUI()
    {
        GUI.skin.label.fontSize = 150;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.skin.box.fontSize = 20;
        
        if (showGUI_Points == true)
        {
            GUI.contentColor = Color.white;
            GUI.Box(new Rect(270, 850, 350, 35), PlayerOneName + ": " + ScorePlayerOne + " points");
            GUI.Box(new Rect(1280, 850, 350, 35), PlayerTwoName + ": " + ScorePlayerTwo + " points");
        }

        if (showGUI_Three == true)
        {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(920, 200, 500, 500), "3");
        }

        if (showGUI_Two == true)
        {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(920, 200, 500, 500), "2");
        }

        if (showGUI_One == true)
        {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(920, 200, 500, 500), "1");
        }

        if (showGUI_GO == true)
        {
            GUI.contentColor = Color.green;
            GUI.Label(new Rect(835, 200, 500, 500), "GO!");
        }
    }

    IEnumerator WaitForAnimationAtStart()
    {
        yield return waitForAnimation;
        QisPressable = true;
    }

    IEnumerator WaitCoroutine()         //wartet immer bei yield für x time
    {
        showGUI_Three = true;
        yield return waitForCountdown;
        showGUI_Three = false;

        showGUI_Two = true;
        yield return waitForCountdown;
        showGUI_Two = false;

        showGUI_One = true;
        yield return waitForCountdown;
        showGUI_One = false;

        showGUI_GO = true;
        yield return waitForInput;
        showGUI_GO = false;

        getBothPlayersInput();

        if ((InputPlayerOne == 3) && (InputPlayerTwo == 3))
        {//Beide Spieler haben nicht gedrückt.
            //Nothing happens. Case relevant.
        }

        else if (InputPlayerOne == 3)
        {//Spieler 1 hat nichts gedrückt. Er verliert.
            ScorePlayerTwo = ScorePlayerTwo + 1;
        }

        else if (InputPlayerTwo == 3)
        {//Spieler 2 hat nichts gedrückt. Er verliert.
            ScorePlayerOne = ScorePlayerOne + 1;
        }

        else if (InputPlayerOne == InputPlayerTwo)
        {//Unentschieden
            //Nothing happens. Case relevant.
        }

        else if ((InputPlayerOne == 0) && (InputPlayerTwo == 2))
        {//Stein schlägt Schere
            ScorePlayerOne = ScorePlayerOne + 1;
        }

        else if ((InputPlayerOne == 2) && (InputPlayerTwo == 0))
        {//Schere verliert gegen Stein
            ScorePlayerTwo = ScorePlayerTwo + 1;
        }

        else if ((InputPlayerOne == 0) && (InputPlayerTwo == 1))
        {//Stein verliert gegen Papier
            ScorePlayerTwo = ScorePlayerTwo + 1;
        }

        else if ((InputPlayerOne == 1) && (InputPlayerTwo == 0))
        {//Papier schlägt Stein
            ScorePlayerOne = ScorePlayerOne + 1;
        }

        else if ((InputPlayerOne == 1) && (InputPlayerTwo == 2))
        {//Papier verliert gegen Scher
            ScorePlayerTwo = ScorePlayerTwo + 1;
        }

        else if ((InputPlayerOne == 2) && (InputPlayerTwo == 1))
        {//Schere schlägt Papier
            ScorePlayerOne = ScorePlayerOne + 1;
        }
        QisPressable = true;
    }

    public void getBothPlayersInput()
    {
        InputPlayerOne = handanimations.InputPlayerOneIsRockPaperScissors;
        InputPlayerTwo = handanimations2.InputPlayerTwoIsRockPaperScissors;
    }

    public void checkAndWriteHighscore()
    {
        //Player1
        if (ScorePlayerOne > PlayerPrefs.GetInt("HighScorePointsSave01"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));
            PlayerPrefs.SetInt("HighScorePointsSave04", PlayerPrefs.GetInt("HighScorePointsSave03"));
            PlayerPrefs.SetInt("HighScorePointsSave03", PlayerPrefs.GetInt("HighScorePointsSave02"));
            PlayerPrefs.SetInt("HighScorePointsSave02", PlayerPrefs.GetInt("HighScorePointsSave01"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerPrefs.GetString("HighScoreNameSave03"));
            PlayerPrefs.SetString("HighScoreNameSave03", PlayerPrefs.GetString("HighScoreNameSave02"));
            PlayerPrefs.SetString("HighScoreNameSave02", PlayerPrefs.GetString("HighScoreNameSave01"));

            PlayerPrefs.SetInt("HighScorePointsSave01", ScorePlayerOne);
            PlayerPrefs.SetString("HighScoreNameSave01", PlayerOneName);
        }
        else if (ScorePlayerOne > PlayerPrefs.GetInt("HighScorePointsSave02"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));
            PlayerPrefs.SetInt("HighScorePointsSave04", PlayerPrefs.GetInt("HighScorePointsSave03"));
            PlayerPrefs.SetInt("HighScorePointsSave03", PlayerPrefs.GetInt("HighScorePointsSave02"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerPrefs.GetString("HighScoreNameSave03"));
            PlayerPrefs.SetString("HighScoreNameSave03", PlayerPrefs.GetString("HighScoreNameSave02"));

            PlayerPrefs.SetInt("HighScorePointsSave02", ScorePlayerOne);
            PlayerPrefs.SetString("HighScoreNameSave02", PlayerOneName);
        }
        else if (ScorePlayerOne > PlayerPrefs.GetInt("HighScorePointsSave03"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));
            PlayerPrefs.SetInt("HighScorePointsSave04", PlayerPrefs.GetInt("HighScorePointsSave03"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerPrefs.GetString("HighScoreNameSave03"));

            PlayerPrefs.SetInt("HighScorePointsSave03", ScorePlayerOne);
            PlayerPrefs.SetString("HighScoreNameSave03", PlayerOneName);
        }
        else if (ScorePlayerOne > PlayerPrefs.GetInt("HighScorePointsSave04"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));

            PlayerPrefs.SetInt("HighScorePointsSave04", ScorePlayerOne);
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerOneName);
        }
        else if (ScorePlayerOne > PlayerPrefs.GetInt("HighScorePointsSave05"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", ScorePlayerOne);
            PlayerPrefs.SetString("HighScoreNameSave05", PlayerOneName);
        }

        //Player2
        if (ScorePlayerTwo > PlayerPrefs.GetInt("HighScorePointsSave01"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));
            PlayerPrefs.SetInt("HighScorePointsSave04", PlayerPrefs.GetInt("HighScorePointsSave03"));
            PlayerPrefs.SetInt("HighScorePointsSave03", PlayerPrefs.GetInt("HighScorePointsSave02"));
            PlayerPrefs.SetInt("HighScorePointsSave02", PlayerPrefs.GetInt("HighScorePointsSave01"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerPrefs.GetString("HighScoreNameSave03"));
            PlayerPrefs.SetString("HighScoreNameSave03", PlayerPrefs.GetString("HighScoreNameSave02"));
            PlayerPrefs.SetString("HighScoreNameSave02", PlayerPrefs.GetString("HighScoreNameSave01"));

            PlayerPrefs.SetInt("HighScorePointsSave01", ScorePlayerTwo);
            PlayerPrefs.SetString("HighScoreNameSave01", PlayerTwoName);
        }
        else if (ScorePlayerTwo > PlayerPrefs.GetInt("HighScorePointsSave02"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));
            PlayerPrefs.SetInt("HighScorePointsSave04", PlayerPrefs.GetInt("HighScorePointsSave03"));
            PlayerPrefs.SetInt("HighScorePointsSave03", PlayerPrefs.GetInt("HighScorePointsSave02"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerPrefs.GetString("HighScoreNameSave03"));
            PlayerPrefs.SetString("HighScoreNameSave03", PlayerPrefs.GetString("HighScoreNameSave02"));

            PlayerPrefs.SetInt("HighScorePointsSave02", ScorePlayerTwo);
            PlayerPrefs.SetString("HighScoreNameSave02", PlayerTwoName);
        }
        else if (ScorePlayerTwo > PlayerPrefs.GetInt("HighScorePointsSave03"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));
            PlayerPrefs.SetInt("HighScorePointsSave04", PlayerPrefs.GetInt("HighScorePointsSave03"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerPrefs.GetString("HighScoreNameSave03"));

            PlayerPrefs.SetInt("HighScorePointsSave03", ScorePlayerTwo);
            PlayerPrefs.SetString("HighScoreNameSave03", PlayerTwoName);
        }
        else if (ScorePlayerTwo > PlayerPrefs.GetInt("HighScorePointsSave04"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", PlayerPrefs.GetInt("HighScorePointsSave04"));

            PlayerPrefs.SetString("HighScoreNameSave05", PlayerPrefs.GetString("HighScoreNameSave04"));

            PlayerPrefs.SetInt("HighScorePointsSave04", ScorePlayerTwo);
            PlayerPrefs.SetString("HighScoreNameSave04", PlayerTwoName);
        }
        else if (ScorePlayerTwo > PlayerPrefs.GetInt("HighScorePointsSave05"))
        {
            PlayerPrefs.SetInt("HighScorePointsSave05", ScorePlayerOne);
            PlayerPrefs.SetString("HighScoreNameSave05", PlayerTwoName);
        }
    }
}
