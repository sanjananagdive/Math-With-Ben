using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool gameOver = false;

    public static BGMusicController instance;

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if game over condition is met (e.g., gameOver becomes true)
        if (gameOver && audioSource.isPlaying)
        {
            // Stop playing the audio source
            audioSource.Stop();
        }
    }

    // Method to set the game over state (call this when game over occurs)
    public void SetGameOver(bool isGameOver)
    {
        gameOver = isGameOver;
    }
}
