
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float Speed;

    [SerializeField]
    Rigidbody2D Rb;

    [SerializeField]
    AudioSource collect;

    [SerializeField]
    AudioSource death;

    [SerializeField]
    AudioSource gameFinish;

    [SerializeField]
    AudioSource gameMusic;

    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    GameObject restartPanel;

    [SerializeField]
    GameObject NextLevelPanel;

    [SerializeField]
    GameObject FinishedPanel;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text finishPointText;

    [SerializeField]
    Text NextscoreText, NextscoreText_2;

    [SerializeField]
    Text NextscoreText2, NextscoreText2_2;

    [SerializeField]
    Text maxScoreText;

    [SerializeField]
    Button soundButton;

    string level;
    int[] scoreArray = { 25, 25, 25, 30, 35, 40, 45, 45, 50, 45};
    public static bool finished = false;
    int levelScore,score = 0;
    int maxScore=0;
    public float jumpHeight;
    public bool isJumping, isFalling = false;
    bool isGameStarted = false;
    bool isRun= false;
    bool nextLevelFreeze = false;
    void Start()
    {
       /* PlayerPrefs.SetInt("Max", maxScore);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();*/
        
        level = SceneManager.GetActiveScene().name;
        if (GameManager.isRestart == true || level != "Level1")
        {
            startPanel.SetActive(false);
            soundButton.gameObject.SetActive(true);
            isGameStarted = true;
        }
        if (level == "Level1")
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        maxScore = PlayerPrefs.GetInt("Max");
        score = PlayerPrefs.GetInt("Score");
        scoreText.text = ": "+ PlayerPrefs.GetInt("Score").ToString();
    }

    void Update()
    {
        if (GameManager.onOff)
        {
            gameMusic.mute = false;
        }
        else
        {
            gameMusic.mute = true;
        }
        if (isGameStarted == false || nextLevelFreeze == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, jumpHeight * Speed);
                isJumping = true;
                anim.SetBool("isJumping", isJumping);
            }
        }
        if (Rb.velocity.y < 0)
        {
            isFalling = true;
            anim.SetBool("isFalling", isFalling);
        }
        else
        {
            isFalling = false;
            anim.SetBool("isFalling", isFalling);
        }
        if (transform.position == new Vector3(5.57f,3.09f,0) || transform.position == new Vector3(5.72f, 3.09f, 0))
        {
            print("geçti");
        }
    }
    private void FixedUpdate()
    {
        if (isGameStarted == false || nextLevelFreeze == true)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        

        Rb.velocity = new Vector2(horizontal * Speed, Rb.velocity.y);
        animasyon(horizontal);
        turnMove(horizontal);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            
        {
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!finished)
        {
            if (collision.CompareTag("Coin"))
            {
                addScore(collision, 5);
                scoreText.text = ": " + score.ToString();
            }
            else if (collision.CompareTag("ExtraPoint"))
            {
                addScore(collision, 10);
                scoreText.text = ": " + score.ToString();
            }
            else if (collision.CompareTag("DeathStick"))
            { 
                death.Play();
                Destroy(this.gameObject);
                restartPanel.SetActive(true);
            }
            else if (collision.CompareTag("Door"))
            {
                
                
                if (SceneManager.GetActiveScene().name == "Level10")
                {
                    PlayerPrefs.SetInt("Max", score);
                    PlayerPrefs.SetInt("Score", score);

                    if (PlayerPrefs.GetInt("Max") >= maxScore)
                    {
                        maxScore = PlayerPrefs.GetInt("Max");
                    }

                    maxScoreText.text = "Highest Point: " + maxScore.ToString();
                    Rb.gravityScale = 0;
                    gameFinish.Play();
                    finishPointText.text = "Your Point: " + score.ToString();
                    NextscoreText_2.text = levelScore.ToString() + " / ";
                    NextscoreText2_2.text = scoreArray[SceneManager.GetActiveScene().buildIndex].ToString();
                    nextLevelFreeze = true;
                    anim.speed = 0;
                    FinishedPanel.SetActive(true);
                    soundButton.gameObject.SetActive(false);
                }
                else
                {
                    Rb.gravityScale = 0;
                    gameFinish.Play();
                    PlayerPrefs.SetInt("Score", score);
                    NextscoreText.text = levelScore.ToString() + " / ";
                    NextscoreText2.text = scoreArray[SceneManager.GetActiveScene().buildIndex].ToString();
                    nextLevelFreeze = true;
                    anim.speed = 0;
                    NextLevelPanel.SetActive(true);
                    soundButton.gameObject.SetActive(false);
                }
            }
            else if (collision.CompareTag("Danger") || collision.CompareTag("Enemy"))
            { 
                Destroy(this.gameObject);
                death.Play();
                restartPanel.SetActive(true);
            }

        }
    }



    void addScore(Collider2D collision, int add)
    {
        levelScore += add;
        score += add;
        collect.Play();
        Destroy(collision.gameObject);
    }
    public void startGame()
    {
        isGameStarted = true;
        startPanel.SetActive(false);
        soundButton.gameObject.SetActive(true);
    }
    void animasyon(float horizontal)
    {
        if (horizontal != 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
        anim.SetBool("isRunning", isRun);
    }
    void turnMove(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

}
