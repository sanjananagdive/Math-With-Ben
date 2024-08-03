using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationScript : MonoBehaviour
{
    public GameObject ans;
    private Animator ansAnim;

    public GameObject score;
    private Animator scoreAnim;

    //public int equationId;

    public bool movePlatform = false;
    public bool hint;

    public bool setPlatformActive=false;
    public bool playCorrectAnsAudio= false;

    public bool hintWasCalled= false;

    public static EquationScript instance;

    void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ansAnim = ans.GetComponent<Animator>();
        scoreAnim = score.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hint = DetectionScript.instance.playerEntered;
        if(hint && !PlayerController.instance.gameover)
        {
            Hint();
        }
        else
        {
            CancelHint();
        }

        bool cancelHint= TouchDetection.instance.cancelHint;
        if(cancelHint)
        {
            CancelHint();
        }

        if(TouchDetection.instance.correct)
        {

            //also check if gameobject has been touched
            GameObject potentialTouched = TouchDetection.instance.touchedObject;
            if(IsChildOf(potentialTouched, gameObject))
            {
                CorrectAnswer();


            }
            //CorrectAnswer();
            
        }
        
        
    }

    public void Hint()
    {
       ansAnim.SetBool("Jiggle", true);
       scoreAnim.SetBool("Jiggle",true);

       hintWasCalled = true;

    }
    public void CancelHint()
    {
       ansAnim.SetBool("Jiggle", false);
       scoreAnim.SetBool("Jiggle",false);

    }
    
    public void CorrectAnswer()
    {
        //Renderer rend= ans.GetComponent<Renderer>();
        //rend.material.color = Color.green;

    
        ansAnim.SetBool("Jiggle", false);
        ansAnim.SetTrigger("Correct");

        

        //PlayAudioScript.instance.PlayCorrectAnsAudio();
        if (PlayAudioScript.instance != null)
        {
            PlayAudioScript.instance.PlayCorrectAnsAudio();
        }
        else
        {
            Debug.LogError("PlayAudioScript instance is not initialized.");
        }


        //DestroyEq(GameObject eqToDestroy);
        Destroy(gameObject, 1.8f);

        
       // DestroyEq();

        setPlatformActive = true;

        //MovePlatform();
        Invoke("MovePlatform", 0.5f);

    }



    void MovePlatform()
    {
        movePlatform= true;
        //setPlatActive = true;
    }
    

    bool IsChildOf(GameObject child, GameObject parent)
    {
        if (child == null || parent == null)
        {
            return false;
        }

        Transform currentParent = child.transform.parent;

        // Traverse up the hierarchy to check if the parent is found
        while (currentParent != null)
        {
            if (currentParent.gameObject == parent)
            {
                return true;
            }
            currentParent = currentParent.parent;
        }

        return false;
    }
}
