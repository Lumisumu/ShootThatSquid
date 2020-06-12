using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    //Komponentit
	public GameObject gameUI;
    public GameObject pauseMenuUI;

    int newChoice; //Odottaa ennen seuraavaa kaskya
    bool GameIsPaused;

    void Start () {
        newChoice = 10;
        GameIsPaused = false;
        Resume();
	}
	

	void Update () {
        newChoice--;

        //Esc Pause
        if(Input.GetKeyDown(KeyCode.Escape) && GameIsPaused == false){
            if(newChoice <= 0){
                newChoice = 30;
                PauseGame();
            }
        }
        //Gamepad Pause
        else if(Input.GetKey(KeyCode.JoystickButton7) && GameIsPaused == false){
            if(newChoice <= 0){
                newChoice = 30;
                PauseGame();
            }
        }

        //Esc Resume
        else if(Input.GetKeyDown(KeyCode.Escape) && GameIsPaused == true){
            if(newChoice <= 0){
                newChoice = 30;
                Resume();
            }
        }
        //Gamepad Resume
        else if(Input.GetKey(KeyCode.JoystickButton7) && GameIsPaused == true){
            if(newChoice <= 0){
                newChoice = 30;
                Resume();
            }
        }
	}

    //Pauseta peli
    void PauseGame()
    {
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }

    //Jatka pelia
    public void Resume () {
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Lopeta peli
	public void QuitGame () {
        Debug.Log("PauseMenu: Back");
        SceneManager.LoadScene("MainMenu");
    }
}
