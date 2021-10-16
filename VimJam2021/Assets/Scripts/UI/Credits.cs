using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private AudioManager audioManager;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Renew")){
            audioManager.Play("ButtonLow");
            SceneManager.LoadScene("Menu");
        }
    }
}
