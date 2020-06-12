using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour {

	int newChoice;

	void Start () {
		newChoice = 30;
	}

	void Update () {
		newChoice--;
		
		/* 
		//sininen = start
		if(sk.napin1tila == 1){
			BackToMainMenu();
		}
		*/

	}

	public void BackToMainMenu () {
		if(newChoice <= 0){
			SceneManager.LoadScene("MainMenu");
		}
	}
}
