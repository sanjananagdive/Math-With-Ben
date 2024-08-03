using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Make sure to include this namespace

public class HintButtonSpawn : MonoBehaviour
{
    public GameObject buttonPrefab;  // Reference to the button prefab
    public Transform spawnLocation; 
    
    public Image[] gemImages;         
    public int gemsToDisable = 2; 
    //public Button hintButton;
    private bool buttonPressed= false;


    public GameObject spawnedButton;


    void Update()
    {
        bool destroyHintButton= TouchDetection.instance.correct;
        if(buttonPressed || destroyHintButton)
        {
            DestroyHintButton();
        }

        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            print("Hint TRigger!!!");
            spawnedButton.SetActive(true);
            //spawnedButton= Instantiate(buttonPrefab, spawnLocation.position, spawnLocation.rotation, spawnLocation.parent);
        }
    }

    
    public void ShowHint()
    {
        //EquationScript.instance.Hint();

        buttonPressed= true;
        print("Button Pressed");


        //DetectionScript.instance.playerEntered=true;

        int gemCount = GetNumberOfActiveGemImages(UIManager.instance.gemImages);
        print( "GemCount: " + gemCount);

        if(gemCount>=2)
        {
            UIManager.instance.DeactivateTwoGems();
            DetectionScript.instance.playerEntered=true;
            PlayAudioScript.instance.PlayHintAudio();
            //Destroy(spawnedButton);

        }
        else 
        {
            print("Insufficient Diamonds!!");
            DestroyHintButton();
            
        }
    }

    int GetNumberOfActiveGemImages(Image[] gemImages)
    {
        int activeCount = 0;
        foreach (Image img in gemImages)
        {
            if (img.gameObject.activeInHierarchy)
            {
                activeCount++;
            }
        }
        return activeCount;
    }

    public void DestroyHintButton()
    {
        spawnedButton.gameObject.SetActive(false);
    }

}
