using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeCounter : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;

    // Reference to the death screen UI
    public GameObject deathScreen;
    private GameManager gameManager;

    private void Start()
    {
        // Ensure the death screen is not active at the start
        deathScreen.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();

        // Load lives remaining from PlayerPrefs
        livesRemaining = PlayerPrefs.GetInt("LivesRemaining", lives.Length);

        // Update the UI based on the lives remaining
        UpdateLivesUI();
    }

    public void LoseLife()
    {
        livesRemaining--;

        if (livesRemaining >= 0)
        {
            lives[livesRemaining].enabled = false;
        }

        if (livesRemaining == 0)
        {
            Debug.Log("You Died");
            ShowDeathScreen();
        }

        // Save lives remaining to PlayerPrefs
        PlayerPrefs.SetInt("LivesRemaining", livesRemaining);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hantu"))
        {
            LoseLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hantu"))
        {
            LoseLife();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseLife();
        }
    }

    private void ShowDeathScreen()
    {
        // Activate the death screen UI
        deathScreen.SetActive(true);

        // Optionally, pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Reset the time scale in case it was paused
        Time.timeScale = 1f;

        // Check if checkpoint was reached
        if (PlayerPrefs.GetInt("CheckpointReached", 0) == 1)
        {
            // Reload the checkpoint scene
            Masukpintu.LoadCheckpoint();
            SceneManager.LoadScene("Perumahan");
        }
        else
        {
            // Load the initial cutscene scene
            SceneManager.LoadScene("Cutscene");
        }
    }

    public void ReturnToMenu()
    {
        // Reset the time scale in case it was paused
        Time.timeScale = 1f;

        // Load the main menu scene (replace "MainMenu" with your main menu scene name)
        SceneManager.LoadScene("MainMenu");
    }

    private void UpdateLivesUI()
    {
        // Update the UI based on the lives remaining
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].enabled = i < livesRemaining;
        }
    }
}