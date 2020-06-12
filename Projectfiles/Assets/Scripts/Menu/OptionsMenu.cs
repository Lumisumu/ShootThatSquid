using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Text toggleButtonText;
    public GameObject soloElements;
    public GameObject coopElements;

    int newChoice;
    bool soloPaalla;

    //Audio
    public AudioMixer audioMixer;
    public Slider audioSlider;

    void Start()
    {
        newChoice = 30;

        soloElements.SetActive(true);
        coopElements.SetActive(false);
        soloPaalla = true;

        toggleButtonText.text = "SHOW GAMEPAD CONTROLS";

        //Hakee AudioMixerin volumen
        float getVolume;
        bool audioFetch = audioMixer.GetFloat("volume", out getVolume);
        audioSlider.value = getVolume;
    }

    void Update()
    {
        newChoice--;
    }

    public void ToggleControlView (){
        if(newChoice <= 0 && soloPaalla == false){
            soloElements.SetActive(true);
            coopElements.SetActive(false);
            newChoice = 30;
            soloPaalla = true;
            toggleButtonText.text = "SHOW GAMEPAD CONTROLS";
        }
        else if(newChoice <= 0 && soloPaalla == true){
            soloElements.SetActive(false);
            coopElements.SetActive(true);
            newChoice = 30;
            soloPaalla = false;
            toggleButtonText.text = "SHOW KEYBOARD CONTROLS";
        }
    }

    public void SetVolume (float volume){
		Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
	}

    public void BackToMainMenu (){
        if(newChoice <= 0){
            SceneManager.LoadScene("MainMenu");
        }
	}
}
