using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frame : MonoBehaviour
{
    [SerializeField]
    private GameObject nextFrame;

    private Cutscene cutscene;

    void Start(){
        cutscene = FindObjectOfType<Cutscene>();
    }

    private void activateNext(){
        if(nextFrame == this.gameObject) SceneManager.LoadScene("Level01");

        nextFrame.SetActive(true);
        nextFrame.GetComponent<Animator>().SetBool("Out", true);

        cutscene.setSkip();
    }
}
