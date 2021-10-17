using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteManager : MonoBehaviour
{
    [SerializeField]
    private int numberEnemies;

    [SerializeField]
    private GameObject[] objects = new GameObject[6];

    private bool[] objectStatus = new bool[6];

    void Start(){
        for(int i = 0; i < 6; i++){
            objects[i].SetActive(false);
            objectStatus[i] = false;
        }
    }

    public void decreaseNumberEnemies(){
        numberEnemies++;
    }

    public int getNumberEnemies(){
        return numberEnemies;
    }

    public void activateObject(int index){
        objects[index].SetActive(true);
        objectStatus[index] = true;
    }
}
