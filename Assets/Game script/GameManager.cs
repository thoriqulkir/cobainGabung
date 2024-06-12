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
        
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("CheckpointReached", 0) == 1)
        {
            // Load the checkpoint scene, for example "Ruangtengah"
            Masukpintu.LoadCheckpoint();
            SceneManager.LoadScene("Perumahan");
            Debug.Log("Loading saved game...");
        }
        else
        {
            // Display message that no saved game exists
            if (messageText != null)
            {
                messageText.text = "Save Game Not Found";
                StartCoroutine(HideMessageAfterDelay(1f));
            }
            Debug.Log("No saved game");
        }
    }
    
    public void NewGame()
    {
        // Reset all PlayerPrefs data
        PlayerPrefs.DeleteAll();

        // Reset static variables if needed
        Masukpintu.kunciKamarDiambil = false;
        Masukpintu.lockpickDiambil = false;
        SimpleLockScript.isPuzzleSolved = false;

        // Load the initial scene
        SceneManager.LoadScene("Cutscene");

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
