using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioScript : MonoBehaviour
{
    private AudioSource audioSource0;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;

    public static PlayAudioScript instance;

    void Awake()
    {
        /*if(instance ==null)
        {
            instance = this;
        }

        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length == 3)
        {
            audioSource0 = audioSources[0];
            audioSource1 = audioSources[1];
            audioSource2 = audioSources[2];
        }
        else
        {
            Debug.LogError("Not enough AudioSource components attached to the GameObject.");
        }*/

        if (instance == null)//
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep this GameObject across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        InitializeAudioSources();//
    }

    void InitializeAudioSources()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        Debug.Log("Number of AudioSource components: " + audioSources.Length);

        if (audioSources.Length == 4)
        {
            audioSource0 = audioSources[0];
            audioSource1 = audioSources[1];
            audioSource2 = audioSources[2];
            audioSource3 = audioSources[3];

            Debug.Log("Audio sources assigned successfully.");
        }
        else
        {
            Debug.LogError("Not enough AudioSource components attached to the GameObject.");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(EquationScript.instance.playCorrectAnsAudio)
        {
            audioSource.Play();
            EquationScript.instance.playCorrectAnsAudio=false;
        }*/
    }
  

    public void PlayCorrectAnsAudio()
    {
        //audioSource0.Play();
        if (audioSource0 != null)
        {
            audioSource0.Play();
        }
        else
        {
            Debug.LogError("audioSource0 is not assigned.");
        }
    }

    public void PlayCollectedAudio()
    {
        //audioSource1.Play();
        if (audioSource1 != null)
        {
            audioSource1.Play();
        }
        else
        {
            Debug.LogError("audioSource1 is not assigned.");
        }
    }

    public void PlayGameWinAudio()
    {
        //audioSource2.Play();
        if (audioSource2 != null)
        {
            audioSource2.Play();
        }
        else
        {
            Debug.LogError("audioSource2 is not assigned.");
        }
    }
    public void PlayHintAudio()
    {
        //audioSource2.Play();
        if (audioSource3 != null)
        {
            audioSource3.Play();
        }
        else
        {
            Debug.LogError("audioSource3 is not assigned.");
        }
    }
}
