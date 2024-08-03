using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class TouchDetection : MonoBehaviour
{
    public bool correct;
    //private GameObject touchedObject;
    public GameObject touchedObject;
    public static TouchDetection instance;

    public int  wrongAnsCount=0;

    private AudioSource audioSource;
    public bool cancelHint=false;

    //private Camera mainCamera;
    //private CinemachineVirtualCamera vcam;

    private HashSet<GameObject> touchedWrongAnswers;//

    void Awake()
    {
        if(instance==null)
        {
            instance=this;
        }

        touchedWrongAnswers = new HashSet<GameObject>();//
    }
    void Start()
    {
        //mainCamera = Camera.main;
        //vcam = FindObjectOfType<CinemachineVirtualCamera>();
        audioSource= GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                HandleInput(touchPosition);
            }
        }

        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleInput(mousePosition);
        }




        
        
    }
    void HandleInput(Vector2 inputPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

        if (hit.collider != null)
        {
            touchedObject = hit.collider.gameObject;
            Debug.Log("Touched object: " + touchedObject.name);
            // Perform any actions you want with the touchedObject

            if (touchedObject.CompareTag("Answer"))
            {
                correct = true;
                audioSource.Play();
                cancelHint=true;
            }
           
            else if (touchedObject.CompareTag("WrongAnswer"))
            {
                //ChangeColorToRed();
                /*correct=false;
                WrongAnswer();
                wrongAnsCount++;//
                print("Wrong Answer Count: "+ wrongAnsCount);//
                audioSource.Play();*/
                if (!touchedWrongAnswers.Contains(touchedObject))
                {
                    correct = false;
                    WrongAnswer();
                    wrongAnsCount++;
                    print("Wrong Answer Count: " + wrongAnsCount);
                    audioSource.Play();

                    // Add this wrong answer to the set to ignore future touches
                    touchedWrongAnswers.Add(touchedObject);
                }
                else
                {
                    Debug.Log("This wrong answer has already been touched: " + touchedObject.name);
                }
            }
        }
    }



    void WrongAnswer()
    {
        Animator anim = touchedObject.GetComponent<Animator>();
        anim.SetTrigger("Wrong");


        //PlayerController.instance.TakeDamage(30);
    }

}

