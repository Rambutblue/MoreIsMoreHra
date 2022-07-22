using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject pauseMenu;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuControl();
        }
    }
    public void Exit()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SaveRunScore();
        DataManager.instance.SaveData();
        SceneManager.LoadScene(0);
    }
    public void PauseMenuControl()
    {
        if (pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            howToPlay.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }
    public void HowToPlayControl()
    {
        if (howToPlay.activeInHierarchy)
        {
            howToPlay.SetActive(false);
        }
        else
        {
            howToPlay.SetActive(true);
        }
    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
