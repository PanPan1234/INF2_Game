using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handanimations2 : MonoBehaviour {

    Animator anim;
    private int Idle = Animator.StringToHash("Idle");
    private int Fist = Animator.StringToHash("Fist");
    private int Spread = Animator.StringToHash("Spread");
    private int Peace = Animator.StringToHash("Peace");
    public bool ButtonIsPressable;
    public bool QisPressable;
    public int InputPlayerTwoIsRockPaperScissors; // 0 = Rock | 1 = Paper | 2 = Scissors | 3 = Default
    public WaitForSeconds waitForInput;
    public WaitForSeconds waitForCountdown;
    public WaitForSeconds waitForAnimation;
    private int HowManyPlayers;
    public AudioSource stoneSound;
    public AudioSource paperSound;
    public AudioSource scissorSound;

    void Awake() //vor dem Spiel initialisiert
    {
        ButtonIsPressable = false;
        QisPressable = false;
        InputPlayerTwoIsRockPaperScissors = 3;
    }

    void Start() //mit Spielstart initialisiert
    {
        anim = GetComponent<Animator>();
        waitForInput = new WaitForSeconds(1.5F);                //Go 1,5sec wartet für Input
        waitForCountdown = new WaitForSeconds(3);               //Countdown 321
        waitForAnimation = new WaitForSeconds(7.5F);            //wartet auf Kameraintro (ca.8Sec)
        HowManyPlayers = PlayerPrefs.GetInt("HowManyPlayers");  //Zieht aus PlayernameSchirm Info wieviele Spieler
        StartCoroutine(WaitForAnimationAtStart());
    }

    void Update() //Eine Abfrage pro Frame
    {
       if (Input.GetKeyDown(KeyCode.Q) && (QisPressable == true))
        {
            QisPressable = false;
            StartCoroutine(WaitCoroutine());
            anim.SetTrigger(Idle);

            if (HowManyPlayers == 2)
            {
                InputPlayerTwoIsRockPaperScissors = 3;
            }
            else if (HowManyPlayers == 1)                               //wenn 1 Spieler dann PC Gegner
            {
                InputPlayerTwoIsRockPaperScissors = Random.Range(0, 3); //PC Random 0-2 (exclusive 3)
            }
        }

        else if (Input.GetKeyDown(KeyCode.J) && (ButtonIsPressable == true))
        {
            ButtonIsPressable = false;
            InputPlayerTwoIsRockPaperScissors = 0;
        }

        else if (Input.GetKeyDown(KeyCode.K) && (ButtonIsPressable == true))
        {
            ButtonIsPressable = false;
            InputPlayerTwoIsRockPaperScissors = 1;
        }

        else if (Input.GetKeyDown(KeyCode.L) && (ButtonIsPressable == true))
        {
            ButtonIsPressable = false;
            InputPlayerTwoIsRockPaperScissors = 2;
        }
    }

    IEnumerator WaitForAnimationAtStart()
    {
        yield return waitForAnimation;
        QisPressable = true;
    }

    IEnumerator WaitCoroutine()
    {
        yield return waitForCountdown;  //Warten, bis Countdown GO ist

        if (HowManyPlayers == 2)        //wenn 2 Spieler, dann JKL freigegeben, sonst nicht
        {
            ButtonIsPressable = true;   //dann Buttons freigegeben (J,K,L)
        }

        yield return waitForInput;      //warten auf Input (1,5sec lang)

        ButtonIsPressable = false;      //danach Buttons gesperrt

        //Je nach Input wird Animation+Sound abgespielt
        if (InputPlayerTwoIsRockPaperScissors == 0)
        {
            stoneSound.Play();
            anim.SetTrigger(Fist);
        }
        else if (InputPlayerTwoIsRockPaperScissors == 1)
        {
            paperSound.Play();
            anim.SetTrigger(Spread);
        }
        else if (InputPlayerTwoIsRockPaperScissors == 2)
        {
            scissorSound.Play();
            anim.SetTrigger(Peace);
        }

        QisPressable = true;
    }
}