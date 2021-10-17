using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    private bool beginGame = false;
    private bool isGameOver = false;
    private bool nextLevel = false;

    private int coinsAmount = 100;

    private LevelManager manager;
    private AudioManager audioManager;

    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider patienceSlider;

    [SerializeField]
    private GameObject gameOverSCreen;
    [SerializeField]
    private GameObject nextLevelScreen;
    [SerializeField]
    private GameObject infiniteEndScreen;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private GameObject shopScreen;

    [SerializeField]
    private TextMeshProUGUI coinsNumber;

    [SerializeField]
    private GameObject normalIcon, angryIcon;

    [SerializeField]
    private GameObject booksBaloon, panBaloon, bearBaloon, coffeeBaloon, switchBaloon, coachBaloon;

    [SerializeField]
    private GameObject booksButton, panButton, bearButton, coffeeButton, switchButton, coachButton; 

    [SerializeField]
    private GameObject[] powerUps = new GameObject[2];

    private int powerUpIndex = 0;
    private int powerUpAmount = 1;

    private float frameSize;

    void Start(){
        gameOverSCreen.SetActive(false);
        nextLevelScreen.SetActive(false);
        shopScreen.SetActive(true);

        normalIcon.SetActive(true);
        angryIcon.SetActive(false);

        booksBaloon.SetActive(false);
        panBaloon.SetActive(false);
        bearBaloon.SetActive(false);
        coffeeBaloon.SetActive(false);
        switchBaloon.SetActive(false);
        coachBaloon.SetActive(false);

        powerUps[1].SetActive(false);
        powerUps[1].GetComponent<Image>().color = new Color(115f/255f, 115f/255f, 115f/255f, 1);

        frameSize = powerUps[0].transform.localScale.x;
        powerUps[0].transform.localScale = new Vector2(frameSize*1.1f, frameSize*1.1f);

        coinsNumber.text = coinsAmount + "";

        manager = FindObjectOfType<LevelManager>();

        audioManager = FindObjectOfType<AudioManager>();

        if(manager.GetGameMode()) infiniteEndScreen.SetActive(false);
    }

    void Update(){
        if(FindObjectOfType<Boss>().getHp() < 50 && powerUpAmount != 2){
            powerUps[1].SetActive(true);
            powerUpAmount = 2;

            audioManager.Play("Phase2");
        }

        if(powerUpAmount == 2) scrollPowerUp();
    }

    public void loseHp(float newValue){
        hpSlider.value = newValue;
    }

    public void changePatience(float newValue){
        patienceSlider.value = newValue;

        Color newColor;

        if(patienceSlider.value < 75f){
            newColor = new Color(228f/255f, 202f/255f, 72f/255f, 1);

            normalIcon.SetActive(false);
            angryIcon.SetActive(true);
        }
        else{
            newColor = new Color(75f/255f, 105f/255f, 47f/255f, 1);

            normalIcon.SetActive(true);
            angryIcon.SetActive(false);
        }

        patienceSlider.gameObject.transform.GetChild(1).GetComponent<Image>().color = newColor;
    }

    public void GameOver(){
        if(manager.GetGameMode()) gameOverSCreen.SetActive(true);
        else{
            infiniteEndScreen.SetActive(true);
            pointsText.text = "Points: " + manager.GetNumberPoints();
        }

        audioManager.Stop("BossTheme");
        audioManager.Play("DefeatTheme");

        isGameOver = true;
    }

    public void NextLevel(){
        nextLevelScreen.SetActive(true);

        audioManager.Stop("BossTheme");
        audioManager.Play("BossVictory");
        audioManager.Play("VictoryTheme");

        nextLevel = true;
    }

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void ToNextLevel(){
        if(SceneManager.GetActiveScene().name != "Level06") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else SceneManager.LoadScene("Credits");
    }

    public void StartGame(){
        shopScreen.SetActive(false);
        beginGame = true;
    }

    public bool getGameOver(){
        return isGameOver;
    }

    public bool getNextLevel(){
        return nextLevel;
    }

    public bool getGameStatus(){
        return beginGame;
    }

    public void buyBooks(){
        if(coinsAmount >= 40){
            manager.activateObject(0);

            coinsAmount -= 40;
            coinsNumber.text = coinsAmount + "";

            audioManager.Play("Coins");

            booksButton.SetActive(false);
        }
    }

    public void showBooks(){
        booksBaloon.SetActive(true);
    }

    public void hideBooks(){
        booksBaloon.SetActive(false);
    }

    public void buyPan(){
        if(coinsAmount >= 50){
            manager.activateObject(1);

            coinsAmount -= 50;
            coinsNumber.text = coinsAmount + "";

            audioManager.Play("Coins");

            panButton.SetActive(false);
        }
    }

    public void showPan(){
        panBaloon.SetActive(true);
    }

    public void hidePan(){
        panBaloon.SetActive(false);
    }

    public void buyBear(){
        if(coinsAmount >= 60){
            manager.activateObject(2);

            coinsAmount -= 60;
            coinsNumber.text = coinsAmount + "";

            audioManager.Play("Coins");

            bearButton.SetActive(false);
        }
    }

    public void showBear(){
        bearBaloon.SetActive(true);
    }

    public void hideBear(){
        bearBaloon.SetActive(false);
    }

    public void buyCoffee(){
        if(coinsAmount >= 70){
            manager.activateObject(3);

            coinsAmount -= 70;
            coinsNumber.text = coinsAmount + "";

            audioManager.Play("Coins");

            coffeeButton.SetActive(false);
        }
    }

    public void showCoffee(){
        coffeeBaloon.SetActive(true);
    }

    public void hideCoffee(){
        coffeeBaloon.SetActive(false);
    }

    public void buySwitch(){
        if(coinsAmount >= 80){
            manager.activateObject(4);

            coinsAmount -= 80;
            coinsNumber.text = coinsAmount + "";

            audioManager.Play("Coins");

            switchButton.SetActive(false);
        }
    }

    public void showSwitch(){
        switchBaloon.SetActive(true);
    }

    public void hideSwitch(){
        switchBaloon.SetActive(false);
    }

    public void buyCoach(){
        if(coinsAmount >= 90){
            manager.activateObject(5);

            coinsAmount -= 90;
            coinsNumber.text = coinsAmount + "";

            audioManager.Play("Coins");

            coachButton.SetActive(false);
        }
    }

    public void showCoach(){
        coachBaloon.SetActive(true);
    }

    public void hideCoach(){
        coachBaloon.SetActive(false);
    }

    public void playButtonHover(){
        audioManager.Play("ButtonLow");
    }

    public void playButtonSelect(){
        audioManager.Play("ButtonHigh");
    }

    private void scrollPowerUp(){
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0){
            powerUps[powerUpIndex].transform.localScale = new Vector2(frameSize, frameSize);
            powerUps[powerUpIndex].GetComponent<Image>().color = new Color(115f/255f, 115f/255f, 115f/255f, 1);

            powerUpIndex++;

            if(powerUpIndex > 1) powerUpIndex = 0;

            audioManager.Play("ButtonHigh");

            powerUps[powerUpIndex].transform.localScale = new Vector2(frameSize*1.1f, frameSize*1.1f);
            powerUps[powerUpIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            powerUps[powerUpIndex].transform.localScale = new Vector2(frameSize, frameSize);
            powerUps[powerUpIndex].GetComponent<Image>().color = new Color(115f/255f, 115f/255f, 115f/255f, 1);

            powerUpIndex--;

            if(powerUpIndex < 0) powerUpIndex = 1;

            audioManager.Play("ButtonHigh");

            powerUps[powerUpIndex].transform.localScale = new Vector2(frameSize*1.1f, frameSize*1.1f);
            powerUps[powerUpIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        FindObjectOfType<Boss>().setPowerUp(powerUpIndex);
    }
}
