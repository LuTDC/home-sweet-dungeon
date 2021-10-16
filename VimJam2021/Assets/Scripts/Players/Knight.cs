using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private Animator animator;

    private Boss boss;
    private UIController controller;
    private LevelManager manager;
    private AudioManager audioManager;

    private bool canAttack = false;
    private bool isAttacking = false;

    private float damageValue = 2f;

    private float hp = 20f;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        boss = FindObjectOfType<Boss>();
        controller = FindObjectOfType<UIController>();
        manager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.getGameOver() && !controller.getNextLevel() && controller.getGameStatus() && !isDead){
            Move();
            if(canAttack && !isAttacking) StartCoroutine(Attack());
        }
    }

    private void Move(){
        float previousX = transform.position.x, previousY = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, 0.01f);

        float x, y;

        if(previousX > transform.position.x) x = -1;
        else if(previousX < transform.position.x) x = 1;
        else x = 0;

        if(previousY > transform.position.y) y = -1;
        else if(previousY < transform.position.y) y = 1;
        else y = 0;

        if(x != 0 || y != 0) animator.SetBool("isIdle", false);
        else animator.SetBool("isIdle", true);

        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", y);
    }

    private IEnumerator Attack(){
        boss.Damage(damageValue);
        isAttacking = true;

        audioManager.Play("Sword");

        yield return new WaitForSeconds(3f);

        isAttacking = false;
    }

    public void Damage(float damageValue){
        if(!isDead){
            hp -= damageValue;

            if(hp <= 0) GameOver();
        }
    }

    private void GameOver(){
        animator.SetTrigger("Die");
        audioManager.Play("KnightDie");

        isDead = true;
    }

    private void Die(){
        manager.decreaseNumberEnemies();

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Boss") canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Boss") canAttack = false;
    }
}
