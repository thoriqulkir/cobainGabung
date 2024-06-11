using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    //public MonologPuzzle monologScript;

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
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuTengah3>())
        {
            // if (!lockpickDiambil)
            // {
            //     monologScript.dialog = new string[] { "Pintu ini terkunci dengan kombinasi." };
            //     monologScript.playerisclose = true; // Pastikan player sedang dekat dengan pintu
            //     monologScript.dialogpanel.SetActive(true); // Tampilkan panel dialog
            // }
            isClose = true;
            bolehmasuk = false; // Set agar pemain tidak langsung pindah scene
        }
    }
    
    private void OnTriggerExit2D(Collider2D col) 
    {
        if (col.GetComponent<PintuKamar>() || col.GetComponent<PintuTengah>() || col.GetComponent<PintuKamarmandi>() 
            || col.GetComponent<PintuTengah2>() || col.GetComponent<PintuRumah>() || col.GetComponent<PintuTengah3>())
        {
            bolehmasuk = false;
            isClose = false;
            // if (monologScript != null)
            // {
            //     monologScript.playerisclose = false;
            //     monologScript.zeroText(); // Reset teks monolog saat pemain menjauhi pintu
            // }
            
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
            SceneManager.LoadScene(scenetoload);
        }
    }

    private void HandlePuzzleCompleted()
    {
        // Puzzle selesai, izinkan masuk
        simpleLockScript.OnPuzzleCompleted -= HandlePuzzleCompleted; // Unsubscribe dari event

        // Simpan status pintu terbuka
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
}