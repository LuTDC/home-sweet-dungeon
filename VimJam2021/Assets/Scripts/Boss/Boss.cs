using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private Animator animator;

    private UIController controller;
    private LevelManager manager;
    private AudioManager audioManager;

    private float hp = 100f;
    private float patience = 100f;

    private float speed = 4f;
    private float xMove, yMove;

    [SerializeField]
    private FireBall fireBallPrefab;
    [SerializeField]
    private FireSlash fireSlashPrefab;

    private bool isAngry = false;

    private bool isBook = false, isPan = false, isBear = false, isCoffee = false, isSwitch = false, isCoach = false;

    private GameObject activityObject;

    private bool isBeingDamaged = false;

    private bool canAttack = true;
    private float coolDown = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controller = FindObjectOfType<UIController>();
        manager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.Play("BossTheme");
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.getGameOver() && !controller.getNextLevel() && controller.getGameStatus()){ 
            Move();
            if(canAttack) Attack();

            if(manager.getNumberEnemies() == 0) NextLevel();

            decreasePatience();

            checkPatience();
        }
        else{
            rigidBody2d.velocity = new Vector2(0, 0);
            animator.SetBool("isIdle", true);
        }
    }

    private void Move(){
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        rigidBody2d.velocity = new Vector2(speed*xMove, speed*yMove);

        if(xMove != 0 || yMove != 0) animator.SetBool("isIdle", false);
        else animator.SetBool("isIdle", true);

        animator.SetFloat("Horizontal", xMove);
        animator.SetFloat("Vertical", yMove);
    }

    private void Attack(){
        if(Input.GetButtonDown("Attack")){
            if(coolDown == 0.7f) Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
            else Instantiate(fireSlashPrefab, transform.position, Quaternion.identity);

            audioManager.Play("BossFire");
            StartCoroutine(coolDownTimer());
        }
    }

    private IEnumerator coolDownTimer(){
        canAttack = false;

        yield return new WaitForSeconds(coolDown);

        canAttack = true;
    }

    public void setPowerUp(int powerUpIndex){
        if(powerUpIndex == 0) coolDown = 0.7f;
        else coolDown = 3f;
    }

    public void Damage(float damageValue){
        if(isAngry) damageValue*=2;

        hp -= damageValue;
        if(hp < 0) hp = 0;

        controller.loseHp(hp);

        if(hp == 0) GameOver();
        else if(!isBeingDamaged) StartCoroutine(damageBlink());
    }

    private IEnumerator damageBlink(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Color normalColor = new Color(1, 1, 1, 1);
        Color transparentColor = new Color(1, 1, 1, 0);

        isBeingDamaged = true;

        int q = 2;

        //FindObjectOfType<CameraBehaviour>().TriggerShake();

        while(q >= 0){
            spriteRenderer.color = transparentColor;

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = normalColor;

            yield return new WaitForSeconds(0.1f);

            q--;
        }

        isBeingDamaged = false;
    }

    private void GameOver(){
        animator.SetTrigger("Die");
        audioManager.Play("BossDie");
        controller.GameOver();
    }

    private void NextLevel(){
        controller.NextLevel();
    }

    private void decreasePatience(){
        patience -= manager.getNumberEnemies()*0.005f;

        if(patience < 0) patience = 0;

        if(patience < 75f){
            isAngry = true;
        }

        controller.changePatience(patience);
    }

    private void increasePatience(int value){
        patience += value;
        if(patience > 100) patience = 100;

        if(patience >= 75f){
            isAngry = false;
        }

        controller.changePatience(patience);
    }

    private void checkPatience(){
        if(Input.GetButtonDown("Renew")){
            if(isBook && activityObject.transform.parent.GetComponent<Object>().usageStatus()){
                increasePatience(5);
                StartCoroutine(activityObject.transform.parent.GetComponent<Object>().startTimer());
                audioManager.Play("Renew");
            }
            else if(isPan && activityObject.transform.parent.GetComponent<Object>().usageStatus()){
                increasePatience(15);
                StartCoroutine(activityObject.transform.parent.GetComponent<Object>().startTimer());
                audioManager.Play("Renew");
            }
            else if(isBear && activityObject.transform.parent.GetComponent<Object>().usageStatus()){
                increasePatience(25);
                StartCoroutine(activityObject.transform.parent.GetComponent<Object>().startTimer());
                audioManager.Play("Renew");
            }
            else if(isCoffee && activityObject.transform.parent.GetComponent<Object>().usageStatus()){
                increasePatience(35);
                StartCoroutine(activityObject.transform.parent.GetComponent<Object>().startTimer());
                audioManager.Play("Renew");
            }
            else if(isSwitch && activityObject.transform.parent.GetComponent<Object>().usageStatus()){
                increasePatience(60);
                StartCoroutine(activityObject.transform.parent.GetComponent<Object>().startTimer());
                audioManager.Play("Renew");
            }
            else if(isCoach && activityObject.transform.parent.GetComponent<Object>().usageStatus()){
                increasePatience(75);
                StartCoroutine(activityObject.transform.parent.GetComponent<Object>().startTimer());
                audioManager.Play("Renew");
            }
        }
    }

    public float getHp(){
        return hp;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Books"){
            isBook = true;
            activityObject = other.gameObject;
        }
        else if(other.tag == "Pan"){
            isPan = true;
            activityObject = other.gameObject;
        }
        else if(other.tag == "Bear"){
            isBear = true;
            activityObject = other.gameObject;
        }
        else if(other.tag == "Coffee"){
            isCoffee = true;
            activityObject = other.gameObject;
        }
        else if(other.tag == "Switch"){
            isSwitch = true;
            activityObject = other.gameObject;
        }
        else if(other.tag == "Coach"){
            isCoach = true;
            activityObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Books") isBook = false;
        else if(other.tag == "Pan") isPan = false;
        else if(other.tag == "Bear") isBear = false;
        else if(other.tag == "Coffee") isCoffee = false;
        else if(other.tag == "Switch") isSwitch = false;
        else if(other.tag == "Coach") isCoach = false;

        if(!isBook && !isPan && !isBear && !isCoffee && !isSwitch && !isCoach) activityObject = null;
    }
}
