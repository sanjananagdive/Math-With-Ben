using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int bananaScore = 0;
    private int strawberryScore = 0;
    private int watermelonScore = 0;
    private int appleScore = 0;
    public Text bananaScoreText;
    public Text strawberryScoreText;
    public Text watermelonScoreText;
    public Text appleScoreText;
    //public Text finalScoreText;

    public GameObject parentCollectible;//
    private int childCount;//
    public bool gameCanWin=false;//

    //public GameObject gopAudio;//

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        childCount= parentCollectible.transform.childCount;//
    }

    // Update is called once per frame
    void Update()
    {
        CheckScore();
    }

    public void IncrementBananaScore()
    {
        bananaScore++;
        bananaScoreText.text = bananaScore.ToString();
        print(bananaScore);
    }

    public void IncrementStrawberryScore()
    {
        strawberryScore++;
        strawberryScoreText.text = strawberryScore.ToString();
        print(strawberryScore);
    }

    public void IncrementWatermelonScore()
    {
        watermelonScore++;
        watermelonScoreText.text = watermelonScore.ToString();
        print(watermelonScore);
    }
    public void IncrementAppleScore()
    {
        appleScore++;
        appleScoreText.text = appleScore.ToString();
        print(appleScore);
    }

    public void Restart()
    {
        // Get the current active scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the scene using its index
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void NextLevel()
    {
        // Get the current active scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the scene using its index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void PlayGameAgain()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Easy()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Medium()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Hard()
    {
        SceneManager.LoadScene("Level3");
    }

    public void CheckScore()//
    {
        if(bananaScore>= childCount || watermelonScore>= childCount || strawberryScore>=childCount || appleScore>=childCount)
        {
            gameCanWin=true;
        }
    }
    /*public void FinalScore()
    {
        if(PlayerController.instance.gameWin)
        {
            int finalScore = (25*bananaScore + 35*strawberryScore);
            finalScoreText.text = finalScore.ToString();
        }
    }*/

    /*public void GameOver()
    {
        Debug.Log("Game Over function is called");

        UIManager.instance.SetImagesActive(false);

        //gameover = true;
        BGMusicController.instance.SetGameOver(true);

        gopAudio.GetComponent<AudioSource>().Play();
    }*/



}
