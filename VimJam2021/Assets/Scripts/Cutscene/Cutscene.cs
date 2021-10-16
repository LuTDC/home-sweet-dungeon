using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] frames = new GameObject[3];

    private int index = 0;

    private AudioManager audioManager;

    private bool canSkip = true;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < 3; i++) frames[i].SetActive(false);

        frames[0].GetComponent<Animator>().SetBool("Out", true);

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        nextFrame();
    }

    private void nextFrame(){
        if(Input.GetButtonDown("Renew") && canSkip){
            frames[index].GetComponent<Animator>().SetBool("In", true);
            index++;

            canSkip = false;

            audioManager.Play("ButtonLow");
        }
    }

    public void setSkip(){
        canSkip = true;
    }
}
