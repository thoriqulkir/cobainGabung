using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Masukpintu : MonoBehaviour
{
    private bool bolehmasuk;
    private bool isClose;
    private string scenetoload;

    public static bool kunciKamarDiambil = false;
    public static bool lockpickDiambil = false;

    public SimpleLockScript simpleLockScript;

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.GetComponent<PintuKamar>())
        {
            if (kunciKamarDiambil)
            {
                scenetoload = "Ruangtengah";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }              
        }
        else if (col.GetComponent<PintuTengah>())
        {
            scenetoload = "Kamartidur2";
            bolehmasuk = true;  
        }
        else if (col.GetComponent<PintuKamarmandi>())
        {
            scenetoload = "Ruangtengah2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuTengah2>())
        {
            if (lockpickDiambil)
            {
                scenetoload = "Kamarmandi";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }
        }
        else if (col.GetComponent<PintuRumah>())
        {
            scenetoload = "Ruangtengah3";
            bolehmasuk = false;
        }
        else if (col.GetComponent<PintuTengah3>())
        {
            isClose = true;
            bolehmasuk = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col) 
    {
        if (col.GetComponent<PintuKamar>() || col.GetComponent<PintuTengah>() || col.GetComponent<PintuKamarmandi>() 
            || col.GetComponent<PintuTengah2>() || col.GetComponent<PintuRumah>() || col.GetComponent<PintuTengah3>())
        {
            bolehmasuk = false;
            isClose = false;
        }
    }

    private void Update() 
    {
        if (isClose && Input.GetKeyDown(KeyCode.E))
        {
            if (simpleLockScript != null && simpleLockScript.interactable)
            {
                simpleLockScript.OnPuzzleCompleted += HandlePuzzleCompleted;
                simpleLockScript.Interact();
            }
        }
        
        if (bolehmasuk && Input.GetKey(KeyCode.E))
        {
            if (scenetoload == "Cutscene Perumahan")
            {
                SaveCheckpoint();
            }
            SceneManager.LoadScene(scenetoload);
        }
    }

    private void HandlePuzzleCompleted()
    {
        simpleLockScript.OnPuzzleCompleted -= HandlePuzzleCompleted;

        if (scenetoload == "Ruangtengah")
        {
            kunciKamarDiambil = true;
        }
        else if (scenetoload == "Kamarmandi")
        {
            lockpickDiambil = true;
        }

        scenetoload = "Perumahan";
        bolehmasuk = true;
    }

    private void SaveCheckpoint()
    {
        PlayerPrefs.SetInt("CheckpointReached", 1);
        PlayerPrefs.SetInt("PuzzleSolved", SimpleLockScript.isPuzzleSolved ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Game Saved.");
    }

    public static void LoadCheckpoint()
    {
        if (PlayerPrefs.GetInt("CheckpointReached", 0) == 1)
        {
            SimpleLockScript.isPuzzleSolved = PlayerPrefs.GetInt("PuzzleSolved", 0) == 1;
            // Load other data if needed
            Debug.Log("Game Loaded.");
        }
    }
}