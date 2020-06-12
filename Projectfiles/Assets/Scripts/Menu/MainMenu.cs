using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //Komponentit
	public Text recordText;
	public int recordScore; //haetaan PlayerPrefs:sista nimellä HighScore (int)
	public GameObject mainMenuUI;
	public GameObject loadingScreenUI;

    //Pelikentan lataus
    AsyncOperation async;
	public string levelName;

    int newChoice; //Odottaa ennen seuraavaa kaskya


    void Start () {
        newChoice = 30;
		levelName = "MiikaTesti";
		recordScore = PlayerPrefs.GetInt("HighScore", 0);
		recordText.text = recordScore.ToString();

		loadingScreenUI.SetActive(false);
		mainMenuUI.SetActive(true);
	}

    void Update()
    {
		newChoice--;

		/* 
        //sininen = start
        if (sk.napin1tila == 1 && mainMenuPaalla == true)
        {
            if (newChoice <= 0)
            {
                newChoice = 50;
                GameStart();
            }
        }

        //musta = reset points
        else if (sk.paussi == 1 && mainMenuPaalla == true)
        {
            if (newChoice <= 0)
            {
                newChoice = 50;
                ResetPoints();
            }
        }

        //oranssi = credits
        else if (sk.P2_napin1tila == 1)
        {
            if (newChoice <= 0)
            {
                newChoice = 50;
                ToggleCredits();
            }
        }
        
        //keltainen = quit
        else if (sk.P2_napin2tila == 1 && mainMenuPaalla == true)
        {
                if (newChoice <= 0)
                {
                    newChoice = 50;
                    QuitGame();
                }
            
        }

        if(newChoice <= 0)
        {
            newChoice = 0;
        }
		
		*/
    }
        

	//MAIN MENU NAPIT
	//PLAY
	public void GameStart () {
		if(newChoice <= 0){
			mainMenuUI.SetActive(false);
			loadingScreenUI.SetActive(true);
			//SceneManager.LoadScene("MiikaTesti");
			StartLoading();
			ActivateScene();
		}
	}
	//TUTORIAL
	public void ToTutorial () {
		if(newChoice <= 0){
			SceneManager.LoadScene("Tutorial");
		}
	}
	//SETTINGS
	public void ToSettings () {
		if(newChoice <= 0){
			SceneManager.LoadScene("Settings");
		}
	}
	//CREDITS
	public void ToggleCredits () {
		if(newChoice <= 0){
			SceneManager.LoadScene("Credits");
		}
	}
	//OPTIONS
	public void ToOptions () {
		if(newChoice <= 0){
			SceneManager.LoadScene("Options");
		}
	}
	//RESET POINTS
	public void ResetPoints () {
		if(newChoice <= 0){
        	PlayerPrefs.SetInt("HighScore", 0);
			recordScore = PlayerPrefs.GetInt("HighScore", 0);
			recordText.text = recordScore.ToString();
		}
	}
	//QUIT
	public void QuitGame () {
		if(newChoice <= 0){
        	Application.Quit();
		}
	}

	//LOADING
	public void StartLoading () {
		StartCoroutine("load");
	}

	IEnumerator load () {
		Debug.LogWarning("ASYNC LOAD STARTED -" + "DO NOT EXIT PLAY MODE OR UNITY CRASHES");
		async = Application.LoadLevelAsync(levelName);
		async.allowSceneActivation = false;
		yield return async;
	}

	public void ActivateScene () {
		async.allowSceneActivation = true;
	}
}
