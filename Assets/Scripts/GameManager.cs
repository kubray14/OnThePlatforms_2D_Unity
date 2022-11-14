using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    GameObject howPanel;

    [SerializeField]
    AudioSource GameMusic;


    int soundCount = 0;
    public static bool isRestart = false;
    public static bool onOff = true;

    public void exitGame()
    {
        Application.Quit();
    }

    public void howToPlay()
    {
        startPanel.SetActive(false);
        howPanel.SetActive(true);
    }
    public void back()
    {
        startPanel.SetActive(true);
        howPanel.SetActive(false);
    }
    public void restartGame()
    {
        isRestart = true;      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
        Player.finished = false;

    }

    public void finished()
    {
     
        Player.finished = false;
        SceneManager.LoadScene(0);

    }

    public void soundOffOn()
    {
        soundCount++;
        if (soundCount % 2 == 0)
        {
            GameMusic.mute = true;
            onOff = true;
        }
        else
        {
            GameMusic.mute = false;
            onOff = false;
        }
    }
}
