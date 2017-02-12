using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float parkHealth;
    private bool gameOver;
    private bool pondScumming;
    private bool bushBurning;

    public static GameManager instance = null;
    public AudioClip gameOverSound;
    public GameObject gameOverMenu;
    public Text parkHealthText;
    

	void Start ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        parkHealth = 100f;
        gameOver = false;
        pondScumming = false;
        bushBurning = false;
        gameOverMenu.SetActive(false);
	}
	
	void Update ()
    {
        if (!gameOver)
        {
            if (pondScumming)
                parkHealth -= Time.deltaTime;
            if (bushBurning)
                parkHealth -= Time.deltaTime * 2;
            UpdateHealthText();
        }
	}

    public void AddTrash()
    {
        parkHealth -= 2;
    }

    public void SubtractTrash()
    {
        parkHealth += 2;
    }

    public void StartPondWaste()
    {
        pondScumming = true;
    }

    public void StopPondWaste()
    {
        pondScumming = false;
    }

    public void StartBushBurning()
    {
        bushBurning = true;
    }

    public void StopBushBurning()
    {
        bushBurning = false;
    }

    private void UpdateHealthText()
    {
        if (parkHealth <= 0)
        {
            parkHealthText.text = "Park Health: 0";
            EndGame();
        }
        else
        {
            parkHealthText.text = "Park Health: " + Mathf.Ceil(parkHealth);
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        SoundManager.instance.PlaySingle(gameOverSound);
        gameOverMenu.SetActive(true);
    }
}
