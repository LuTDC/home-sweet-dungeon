using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioManager audioManager;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartCutscene(){
        SceneManager.LoadScene("Cutscene");
    }

    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void InfiniteMode(){
        SceneManager.LoadScene("InfiniteLevel");
    }

    public void playButtonHover(){
        audioManager.Play("ButtonLow");
    }

    public void playButtonSelect(){
        audioManager.Play("ButtonHigh");
    }
}
