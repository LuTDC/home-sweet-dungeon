using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCredits : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z);

        if(transform.position.y >= 32f) SceneManager.LoadScene("Menu");

        Debug.Log(transform.position.y);
    }
}
