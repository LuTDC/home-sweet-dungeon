using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int numberEnemies;

    [SerializeField]
    private GameObject[] objects = new GameObject[6];

    [SerializeField]
    private GameObject[] players = new GameObject[3];

    private bool[] objectStatus = new bool[6];

    private bool isInfinite = false;
    private bool canSpawn = true;
    private int previousIndex = -1;
    private int points = 0;

    private UIController controller;

    void Start(){
        for(int i = 0; i < 6; i++){
            objects[i].SetActive(false);
            objectStatus[i] = false;
        }

        if(SceneManager.GetActiveScene().name == "InfiniteLevel") isInfinite = true;

        controller = FindObjectOfType<UIController>();
    }

    void Update(){
        if(isInfinite && controller.getGameStatus()) SpawnEnemies();
    }

    public void decreaseNumberEnemies(){
        if(!isInfinite) numberEnemies--;
        else points++;
    }

    public int getNumberEnemies(){
        return numberEnemies;
    }

    public int GetNumberPoints(){
        return points;
    }

    public bool GetGameMode(){
        if(isInfinite) return false;
        return true;
    }

    public void activateObject(int index){
        objects[index].SetActive(true);
        objectStatus[index] = true;
    }

    private void SpawnEnemies(){
        if(canSpawn){
            int r = Random.Range(0, 3);

            while(r == previousIndex) r = Random.Range(0, 3);

            previousIndex= r;
            Instantiate(players[r], new Vector3(-4.8f, -4.8f, 1.15f), Quaternion.identity);
            Instantiate(players[r], new Vector3(1f, -4.8f, 1.15f), Quaternion.identity);
            Instantiate(players[r], new Vector3(5.4f, -4.8f, 1.15f), Quaternion.identity);

            StartCoroutine(PauseSpawn());
        }
    }

    private IEnumerator PauseSpawn(){
        canSpawn = false;

        yield return new WaitForSeconds(10);

        canSpawn = true;
    }
}
