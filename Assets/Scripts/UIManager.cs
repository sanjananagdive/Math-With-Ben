using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    private Animator goanim;
    private Animator gwanim;

    public Image[] healthImages;
    public Image[] gemImages;

    public GameObject gopAudio;

    public static UIManager instance;

    public GameObject warningPanel;



    void Awake()
    {
        if(instance==null)
        {
            instance=this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        goanim = gameOverPanel.GetComponent<Animator>();
        gwanim = gameWinPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.gameover)
        {
            Invoke("PlayGameOverPanelAnim", 0.5f);
        }

        if(PlayerController.instance.gameWin)
        {
            //Invoke("PlayGameOverPanelAnim", 0.5f);
            gameWinPanel.SetActive(true);
            PlayGameWinPanelAnim();
        }
        if(PlayerController.instance.showWarning)
        {
            StartCoroutine("ShowWarning");
        }

        MinusLife();
    }

    void PlayGameOverPanelAnim()
    {
        //gameOverPanel.GetComponent<AudioSource>().Play();
        goanim.SetTrigger("gameover");
        Invoke("PlaySkullAnim", 0.5f);
    }
    void PlaySkullAnim()
    {
        //goanim.SetTrigger("skull");
    }
    void PlayGameWinPanelAnim()
    {
        Debug.Log("GameWin");
        gwanim.SetTrigger("Win");

    }

    public void DecreaseLives()
    {
        int hitCount = PlayerController.instance.hitCount;
        if (hitCount - 1 < healthImages.Length)
        {
            healthImages[hitCount - 1].gameObject.SetActive(false);
        }

    }

    public void SetImagesActive(bool isActive)
    {
        foreach (Image img in gemImages)
        {
            img.gameObject.SetActive(isActive);
        }
    }

    /*public void SetImagesActive(bool isActive)
    {
        foreach (Image img in healthImages)
        {
            img.gameObject.SetActive(isActive);
        }
    }*/

    public void MinusLife()//
    {
        int myInt = TouchDetection.instance.wrongAnsCount;
        //public GameObject gopAudio;
        // Check if the player's life has reached zero or below
        if (myInt >=3)
        {
            //gameOver
            print("Game Over");
            PlayGameOverPanelAnim();
            BGMusicController.instance.SetGameOver(true);
            gopAudio.GetComponent<AudioSource>().Play();

            //GOPManager.instance.PlayGameOverAudio();


            
        }
        else if (myInt > 0)
        {
            // Ensure myInt-1 is within the bounds of the healthImages array
            /*if (myInt-1 < healthImages.Length)
            {
                
                healthImages[myInt-1].gameObject.SetActive(false);
            }*/
            if (myInt-1 < gemImages.Length)
            {
                
                gemImages[myInt-1].gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("myInt-1 is out of bounds of the healthImages array.");
            }
  
        }

    }
    public void DeactivateTwoGems()
    {
        int deactivatedCount = 0;

            foreach (Image img in gemImages)
            {
                if (img.gameObject.activeInHierarchy && deactivatedCount < 2)
                {
                    img.gameObject.SetActive(false);
                    deactivatedCount++;
                }

                if (deactivatedCount >= 2)
                {
                    break;
                }
            }
    }

    IEnumerator ShowWarning()
    {
        warningPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        warningPanel.gameObject.SetActive(false);
        PlayerController.instance.showWarning=false;
    }

}
