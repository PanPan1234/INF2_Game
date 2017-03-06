using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handanimations : MonoBehaviour {

    Animator anim;
    private int Idle = Animator.StringToHash("Idle");
    private int Fist = Animator.StringToHash("Fist");
    private int Spread = Animator.StringToHash("Spread");
    private int Peace = Animator.StringToHash("Peace");
    public bool ButtonIsPressable;
    public bool QisPressable;
    public int InputPlayerOneIsRockPaperScissors; // 0 = Rock | 1 = Paper | 2 = Scissors | 3 = Default
    public WaitForSeconds waitForInput;
    public WaitForSeconds waitForCountdown;
    public WaitForSeconds waitForAnimation;
    public AudioSource stoneSound;
    public AudioSource paperSound;
    public AudioSource scissorSound;

    void Awake() //vor dem Spiel initialisiert
    {
        ButtonIsPressable = false;
        QisPressable = false;
        InputPlayerOneIsRockPaperScissors = 3;
    }

    void Start() //mit Spielstart initialisiert
    {
        anim = GetComponent<Animator>();                
        waitForInput = new WaitForSeconds(1.5F);        //Go 1,5sec wartet für Input
        waitForCountdown = new WaitForSeconds(3);       //Countdown 321
        waitForAnimation = new WaitForSeconds(7.5F);    //wartet auf Kameraintro (ca.8Sec)
        StartCoroutine(WaitForAnimationAtStart());
    }

    void Update() //Eine Abfrage pro Frame
    {
        if (Input.GetKeyDown(KeyCode.Q) && (QisPressable == true))
        {
            QisPressable = false;
            StartCoroutine(WaitCoroutine());
            anim.SetTrigger(Idle);
            InputPlayerOneIsRockPaperScissors = 3;
        }

        else if (Input.GetKeyDown(KeyCode.A) && (ButtonIsPressable == true))
        {
            ButtonIsPressable = false;
            InputPlayerOneIsRockPaperScissors = 0;
        }

        else if (Input.GetKeyDown(KeyCode.S) && (ButtonIsPressable == true))
        {
            ButtonIsPressable = false;
            InputPlayerOneIsRockPaperScissors = 1;
        }

        else if (Input.GetKeyDown(KeyCode.D) && (ButtonIsPressable == true))
        {
            ButtonIsPressable = false;
            InputPlayerOneIsRockPaperScissors = 2;
        }
    }

    IEnumerator WaitForAnimationAtStart()
    {
        yield return waitForAnimation;
        QisPressable = true;
    }
    
    IEnumerator WaitCoroutine()
    {
        yield return waitForCountdown;      //Warten, bis Countdown GO ist

        ButtonIsPressable = true;           //dann Buttons freigegeben (A,S,D)
        yield return waitForInput;          //warten auf Input (1,5sec lang)
        ButtonIsPressable = false;          //danach Buttons gesperrt

        //Je nach Input wird Animation+Sound abgespielt
        if (InputPlayerOneIsRockPaperScissors == 0)
        {
            stoneSound.Play();
            anim.SetTrigger(Fist);
        }
        else if(InputPlayerOneIsRockPaperScissors == 1)
        {
            paperSound.Play();
            anim.SetTrigger(Spread);
        }
        else if (InputPlayerOneIsRockPaperScissors == 2)
        {
            scissorSound.Play();
            anim.SetTrigger(Peace);
        }

        QisPressable = true;    //Q freigegeben, um neue Runde zu starten
    }
}