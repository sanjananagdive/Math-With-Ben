using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool moveLeft;
    private bool moveRight;
    private float horizontalSpeed;
    public float speed;

    public float jumpForce;
    public bool jump=false;
    private bool isGrounded;

    public GameObject gopAudio;
    //private int direction = 1;

    public bool canMove = true;
    public bool gameover = false;
    public bool gameWin = false;

    public bool ishurt = false;
    public int playerLife = 90;
    public int hitCount=0;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public static PlayerController instance;

    public bool showWarning=false;


    private EdgeCollider2D edgeCollider;//
    private Vector2[] originalPoints;//


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
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        edgeCollider = GetComponent<EdgeCollider2D>();//
        originalPoints = edgeCollider.points;//

    }
    
    void Update()
    {
        if(canMove)
        {
            Move();
            SetAnimatorValues();
            Flip();
        }
    
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);

       


       // Raycast isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
    }
    
    public void PointerUpLeft()
    {
        moveLeft = false;
        //canMove = false;
    }
    public void PointerDownLeft()
    {
        moveLeft = true;
        //canMove = true;
    }
    public void PointerUpRight()
    {
        moveRight = false;
        //canMove = false;
    }
    public void PointerDownRight()
    {
        moveRight = true;
        //canMove = true;
    }
    
    public void TakeDamage(int damage)
    {
        hitCount++; // Increment the hit counter
        print(hitCount);
        playerLife -= damage; // Decrease the player's life

        // Check if the player's life has reached zero or below
        if (playerLife <= 0)
        {
            Die();
        }
        else
        {
            UIManager.instance.DecreaseLives();
           // UIManager.instance.DecreaseGemCount();
            StartCoroutine("PlayHurtAnim");
        }
    }


    IEnumerator PlayHurtAnim()
    {
        ishurt = true;
        anim.SetBool("hurt", true);
        
        yield return new WaitForSeconds(0.2f);

        ishurt = false;
        anim.SetBool("hurt", false);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            isGrounded= false;
            rb.velocity= Vector2.up * jumpForce;
            anim.SetBool("jump",true);
        }
    }
    public void Move()
    {
        if(moveLeft)
        {
            horizontalSpeed= -speed;
        }
        else if(moveRight)
        {
            horizontalSpeed = speed;
        }
        else
           horizontalSpeed = 0;
    }
    
    private void SetAnimatorValues()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        if (isGrounded) // Added check for isGrounded
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
            {
                anim.SetBool("walk", true);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
            }

            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("idle", false);
        }

        
    }

    private void Flip()
    {
    
        
        if(moveLeft)
        {
            spriteRenderer.flipX = true;
            edgeCollider.points = MirrorPoints(originalPoints);//
            
        }
        else if(moveRight)
        {
            spriteRenderer.flipX = false;
            edgeCollider.points = originalPoints;//
        }
    }

    Vector2[] MirrorPoints(Vector2[] points)
    {
        Vector2[] mirroredPoints = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            mirroredPoints[i] = new Vector2(-points[i].x, points[i].y);
        }
        return mirroredPoints;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))//added
        {
            isGrounded = true;
            anim.SetBool("jump", false);
        }

        if(other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Game Over");
            canMove=false;
            
            
            anim.SetTrigger("dead");
            //gameover panel pop up
            gameover = true;
            BGMusicController.instance.SetGameOver(true);

            gopAudio.GetComponent<AudioSource>().Play();
            //GameManager.instance.GameOver();
        }

        if(other.gameObject.CompareTag("Gift"))
        {
            if(GameManager.instance.gameCanWin)//
            {
                gameWin = true;
                BGMusicController.instance.SetGameOver(true);
                PlayAudioScript.instance.PlayGameWinAudio();
            }
            else
            {
                print("Collect all fruits!!");
                showWarning=true;
            }
            //gameWin = true;
            //BGMusicController.instance.SetGameOver(true);
            //PlayAudioScript.instance.PlayGameWinAudio();
        }

       
    }

    private void OnTriggerExit2D(Collider2D other) // Added OnTriggerExit2D method
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Railing"))
        {
            //hurt anim to be played
            ishurt = true;
        }
        if(col.gameObject.CompareTag("Ground"))
        {
            isGrounded= true;
            anim.SetBool("jump",false);
        }
         if(col.gameObject.CompareTag("Enemy"))
        {
           // anim.SetTrigger("hurt");
        }
    }

    public void Die()
    {
        Debug.Log("Game Over");
            
            anim.SetTrigger("dead");
            canMove=false;
            //gameover panel pop up
            gameover = true;
            BGMusicController.instance.SetGameOver(true);

            gopAudio.GetComponent<AudioSource>().Play();
    }

}
