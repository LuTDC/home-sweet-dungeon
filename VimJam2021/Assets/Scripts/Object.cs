using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField]
    private float refilTime;

    private bool canUse = true;

   public IEnumerator startTimer(){
        canUse = false;
        GetComponent<SpriteRenderer>().color = new Color(140f/255f, 140f/255f, 140f/255f, 1);

        yield return new WaitForSeconds(refilTime);

        canUse = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public bool usageStatus(){
        return canUse;
    }
}
