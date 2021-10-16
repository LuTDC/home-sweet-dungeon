using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Animator animator;

    private Vector3 mousePosition;

    private float damageValue = 3f;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 90);

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePosition.x, mousePosition.y, transform.position.z), 20f*Time.deltaTime);

        if(transform.position.x == mousePosition.x){
            animator.SetTrigger("Fade");

            audioManager.Play("BossFire");
        }
    }

    private void destroyBall(){
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall" || other.tag == "Archer" || other.tag == "Knight" || other.tag == "Mage"){
            animator.SetTrigger("Fade");

            audioManager.Play("BossFire");

            if(other.tag == "Archer") other.GetComponent<Archer>().Damage(damageValue);
            else if(other.tag == "Knight") other.GetComponent<Knight>().Damage(damageValue);
            else if(other.tag == "Mage") other.GetComponent<Mage>().Damage(damageValue);
        }
    }
}
