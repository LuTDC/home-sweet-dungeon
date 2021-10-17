using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Archer : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private Animator animator;

    private UIController controller;
    private LevelManager manager;
    private AudioManager audioManager;

    private float hp = 10f;

    [SerializeField]
    private Arrow arrowPrefab;

    private bool isPlaced = false;

    private float yPosition = -2f;

    private bool canAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controller = FindObjectOfType<UIController>();
        manager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();

        transform.parent = GameObject.Find("Players").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.getGameOver() && !controller.getNextLevel() && controller.getGameStatus()){
            if(!isPlaced) Move();
            if(canAttack) Attack();
        }
    }

    private void Move(){
        rigidBody2d.velocity = new Vector2(0, 2);

        animator.SetBool("isIdle", false);
        animator.SetFloat("Vertical", 1);

        if(transform.position.y >= yPosition){
            isPlaced = true;
            animator.SetBool("isIdle", true);
            rigidBody2d.velocity = new Vector2(0, 0);

            canAttack = true;
        }
    }

    private void Attack(){
        Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        audioManager.Play("Arrow");

        StartCoroutine(allowAttack());
    }

    private IEnumerator allowAttack(){
        canAttack = false;

        yield return new WaitForSeconds(2f);

        canAttack = true;
    }

    public void Damage(float damageValue){
        hp -= damageValue;

        if(hp <= 0) GameOver();
    }

    private void GameOver(){
        audioManager.Play("ArcherDie");
        animator.SetTrigger("Die");
    }

    private void Die(){
        manager.decreaseNumberEnemies();

        Destroy(this.gameObject);
    }
}
