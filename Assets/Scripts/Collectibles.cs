using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{

    public GameObject sourceAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //GetComponent<AudioSource>().Play();
            //sourceAudio.GetComponent<AudioSource>().Play();

            PlayAudioScript.instance.PlayCollectedAudio();
            
            if(gameObject.CompareTag("Banana"))
            {
                GameManager.instance.IncrementBananaScore();
            }
            else if(gameObject.CompareTag("Watermelon"))
            {
                Debug.Log("Watermelon collected");
                GameManager.instance.IncrementWatermelonScore();
            }
            else if(gameObject.CompareTag("Strawberry"))
            {
                Debug.Log("Strawberry collected");
                GameManager.instance.IncrementStrawberryScore();
            }
            else if(gameObject.CompareTag("Apple"))
            {
                Debug.Log("Apple collected");
                GameManager.instance.IncrementAppleScore();
            }
            //GameManager.instance.IncrementBananaScore();
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
