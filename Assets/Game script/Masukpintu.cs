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
    public static bool kunciKepsekDiambil = false;
    public static bool kunciRSDiambil = false;

    public SimpleLockScript simpleLockScript;

    public MonologPuzzle monologPuzzle;

    void Start()
    {
        // kunciRSDiambil = PlayerPrefs.GetInt("KunciRSDiambil", 0) == 1;
    }

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

            simpleLockScript = col.GetComponent<SimpleLockScript>();
            monologPuzzle = FindObjectOfType<MonologPuzzle>();
            simpleLockScript.monologPuzzle = monologPuzzle;
        }
        else if (col.GetComponent<LorongKota>())
        {
            scenetoload = "Kota";
            bolehmasuk = true;
        }
        else if (col.GetComponent<GerbangSekolah>())
        {
            scenetoload = "Sekolah1";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuSekolahKeKota>())
        {
            scenetoload = "KotaDariSekolah";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuSekolahLt1>())
        {
            if (PlayerPrefs.GetInt("ClockPuzzleSolved", 0) == 1)
            {
                scenetoload = "Warehouse";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }
        }
        else if (col.GetComponent<TanggaSekolahKeLt2>())
        {
            isClose = true;
            scenetoload = "Sekolah2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<TanggaKeLt2DariLt3>())
        {
            scenetoload = "Sekolah2DariLt3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<TanggaSekolahKeLt1>())
        {
            scenetoload = "Sekolah1DariLt2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<TanggaSekolahKeLt3>())
        {
            scenetoload = "Sekolah3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuSekolahLt2>())
        {
            if (kunciKepsekDiambil)
            {
                scenetoload = "Kepsek";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }
        }
        else if (col.GetComponent<PintuKeKelas1>())
        {
            scenetoload = "Kelas1";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeKelas2>())
        {
            scenetoload = "Kelas2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeKelas3>())
        {
            scenetoload = "Kelas3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeKelas4>())
        {
            scenetoload = "Kelas4";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuSekolahLt3>())
        {
            isClose = true;
            bolehmasuk = false;

            simpleLockScript = col.GetComponent<SimpleLockScript>();
            monologPuzzle = FindObjectOfType<MonologPuzzle>();
            simpleLockScript.monologPuzzle = monologPuzzle;
        }
        else if (col.GetComponent<PintuKelas1>())
        {
            scenetoload = "Sekolah2DariKelas1";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKelas2>())
        {
            scenetoload = "Sekolah2DariKelas2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKelas3>())
        {
            scenetoload = "Sekolah3DariKelas3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKelas4>())
        {
            scenetoload = "Sekolah3DariKelas4";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKepsek>())
        {
            scenetoload = "Sekolah2DariKepsek";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuPerpus>())
        {
            scenetoload = "Sekolah3DariPerpus";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuWarehouse>())
        {
            scenetoload = "Sekolah1DariWarehouse";
            bolehmasuk = true;
        }
        else if (col.GetComponent<JalanKeRS>())
        {
            scenetoload = "kota";
            bolehmasuk = true;
        }
        // else if (col.GetComponent<PintuRS>())
        // {
        //     if (kunciRSDiambil)
        //     {
        //         sceneToLoad = "RumahSakit";
        //         bolehMasuk = true;
        //     }
        //     else
        //     {
        //         bolehMasuk = false;
        //     }
        // }
    }

    private void OnTriggerExit2D(Collider2D col) 
    {
        if (col.GetComponent<PintuKamar>() || col.GetComponent<PintuTengah>() || col.GetComponent<PintuKamarmandi>() 
            || col.GetComponent<PintuTengah2>() || col.GetComponent<PintuRumah>() || col.GetComponent<PintuTengah3>()
            || col.GetComponent<LorongKota>() || col.GetComponent<GerbangSekolah>() || col.GetComponent<PintuSekolahKeKota>()
            || col.GetComponent<TanggaSekolahKeLt2>() || col.GetComponent<PintuSekolahLt1>() || col.GetComponent<PintuSekolahLt3>()
            || col.GetComponent<TanggaKeLt2DariLt3>() || col.GetComponent<TanggaSekolahKeLt1>() || col.GetComponent<TanggaSekolahKeLt3>() 
            || col.GetComponent<PintuSekolahLt2>() || col.GetComponent<PintuKeKelas1>() || col.GetComponent<PintuKeKelas2>() 
            || col.GetComponent<PintuKeKelas3>() || col.GetComponent<PintuKeKelas4>() || col.GetComponent<PintuKelas1>() || col.GetComponent<PintuKelas2>() 
            || col.GetComponent<PintuKelas3>() || col.GetComponent<PintuKelas4>() || col.GetComponent<PintuKepsek>() || col.GetComponent<PintuPerpus>() 
            || col.GetComponent<PintuWarehouse>() || col.GetComponent<JalanKeRS>() )
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
            // Save lives remaining before loading new scene
            LifeCounter lifeCounter = FindObjectOfType<LifeCounter>();
            if (lifeCounter != null)
            {
                PlayerPrefs.SetInt("LivesRemaining", lifeCounter.livesRemaining);
            }

            SceneManager.LoadScene(scenetoload);
        }
    }

    private void HandlePuzzleCompleted()
    {
        simpleLockScript.OnPuzzleCompleted -= HandlePuzzleCompleted;

        // Muat scene berdasarkan identifier unik dari puzzle
        string sceneToLoad = PlayerPrefs.GetString(simpleLockScript.puzzleIdentifier + "_SceneToLoad", "");
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
            bolehmasuk = true;
        }
    }

    public void SaveCheckpoint()
    {
        PlayerPrefs.SetInt("CheckpointReached", 1);
        PlayerPrefs.SetInt("PuzzleSolved", SimpleClock.isPuzzleSolved ? 1 : 0);
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

        // Simpan posisi pemain
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

    public static void LoadCheckpoint()
    {
        if (PlayerPrefs.GetInt("CheckpointReached", 0) == 1)
        {
            SimpleLockScript.isPuzzleSolved = PlayerPrefs.GetInt("PuzzleSolved", 0) == 1;
            kunciRSDiambil = PlayerPrefs.GetInt("KunciRSDiambil", 0) == 1;

            // Load posisi pemain
            Vector3 playerPosition;
            playerPosition.x = PlayerPrefs.GetFloat("PlayerPosX", 0);
            playerPosition.y = PlayerPrefs.GetFloat("PlayerPosY", 0);
            playerPosition.z = PlayerPrefs.GetFloat("PlayerPosZ", 0);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = playerPosition;

            Debug.Log("Save Loaded");
        }
    }
}