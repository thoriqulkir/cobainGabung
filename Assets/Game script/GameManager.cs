using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text messageText;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("CheckpointReached", 0) == 1)
        {
            string lastScene = PlayerPrefs.GetString("LastScene");
            SceneManager.LoadScene(lastScene);
            StartCoroutine(LoadPlayerPosition());

            Debug.Log("Loading saved game...");
        }
        else
        {
            // Display message that no saved game exists
            if (messageText != null)
            {
                messageText.text = "Save Game Tidak Ditemukan";
                StartCoroutine(HideMessageAfterDelay(1f));
            }
            Debug.Log("No saved game");
        }
    }

    private IEnumerator LoadPlayerPosition()
    {
        yield return new WaitForSeconds(0.1f); // Tunggu hingga scene selesai dimuat

        Masukpintu.LoadCheckpoint();
    }
    
    public void NewGame()
    {
        // Reset all PlayerPrefs data
        PlayerPrefs.DeleteAll();

        // Reset static variables if needed
        Masukpintu.kunciKamarDiambil = false;
        Masukpintu.lockpickDiambil = false;
        SimpleLockScript.isPuzzleSolved = false;
        Masukpintu.kunciKepsekDiambil = false;
        Masukpintu.kunciRSDiambil = false;
        Masukpintu.puzzlePieceDiambil = false;
        Masukpintu.jigsawPuzzleCompleted = false;
        Masukpintu.kunciLabDiambil = false;
        Masukpintu.cairanGembokDiambil = false;

        // Load the initial scene
        SceneManager.LoadScene("Cutscene awal");

        Debug.Log("Game Started");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();

        // If running in the Unity editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Tunggu selama beberapa detik (waktu nyata)
        if (messageText != null)
        {
            messageText.text = ""; // Sembunyikan pesan dengan mengosongkan teks
        }
    }
}
