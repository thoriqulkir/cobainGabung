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
    public static bool puzzlePieceDiambil = false;
    public static bool jigsawPuzzleCompleted = false;
    public static bool kunciLabDiambil = false;
    public static bool cairanGembokDiambil = false;

    public SimpleLockScript simpleLockScript;

    public MonologPuzzle monologPuzzle;

    public JigsawScript jigsawScript;

    [SerializeField]
    private GameObject fakeJigsawCanvas;

    [SerializeField]
    private GameObject realJigsawCanvas;

    void Start()
    {
        fakeJigsawCanvas.SetActive(false);
        realJigsawCanvas.SetActive(false);
        
        if (PlayerPrefs.GetInt("PipePuzzleCompleted", 0) == 1)
        {
            cairanGembokDiambil = true;
            SaveCheckpoint();
            // Reset status puzzle jika diperlukan
            // PlayerPrefs.SetInt("PipePuzzleCompleted", 0);
            // PlayerPrefs.Save();
        }
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
            scenetoload = "RumahSakit";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuMasukRS>())
        {
            if (kunciRSDiambil)
            {
                scenetoload = "RSLt1";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }
        }
        else if (col.GetComponent<PintuRSLt1>())
        {
            isClose = true;
            bolehmasuk = false;

            jigsawScript = col.GetComponent<JigsawScript>();
        }
        else if (col.GetComponent<TanggaRSKeLt2>())
        {
            scenetoload = "RSLt2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<TanggaRSKeLt1>())
        {
            scenetoload = "RSLt1 Dari Lt2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRuangDokter>())
        {
            scenetoload = "RSLt1 Dari RuangDokter";
            bolehmasuk = true;
        }
        else if (col.GetComponent<TanggaRSKeLt3>())
        {
            scenetoload = "RSLt3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRSLt2>())
        {
            if (kunciLabDiambil)
            {
                scenetoload = "Laboratorium";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }
        }
        else if (col.GetComponent<PintuKeRP1>())
        {
            scenetoload = "RuangPasien1";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeRP2>())
        {
            scenetoload = "RuangPasien2";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeRP3>())
        {
            scenetoload = "RuangPasien3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeRP4>())
        {
            scenetoload = "RuangPasien4";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRSLt3>())
        {
            if (cairanGembokDiambil)
            {
                scenetoload = "KamarMayat";
                bolehmasuk = true;
            }
            else
            {
                bolehmasuk = false;
            }
        }
        else if (col.GetComponent<TanggaRS3KeLt2>())
        {
            scenetoload = "RSLt2 Dari Lt3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKamarMayat>())
        {
            scenetoload = "RSLt3 Dari KamarMayat";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRP3>())
        {
            scenetoload = "RSLt3 Dari RP3";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRP4>())
        {
            scenetoload = "RSLt3 Dari P4";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRP1>())
        {
            scenetoload = "RSLt2 Dari P1";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuRP2>())
        {
            scenetoload = "RSLt2 Dari P1";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuLab>())
        {
            scenetoload = "RSLt2 Dari Lab";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeluarRS>())
        {
            scenetoload = "RumahSakitKeluar";
            bolehmasuk = true;
        }
        else if (col.GetComponent<Brankas>())
        {
            isClose = true;
        }
        else if (col.GetComponent<KeKotaDariRS>())
        {
            scenetoload = "Kota Dari RS";
            bolehmasuk = true;
        }
        
        else if (col.GetComponent<LorongDariKota>())
        {
            scenetoload = "Perumahan Dari Kota";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeRuangAkhir>())
        {
            scenetoload = "Ruangtengah akhir";
            bolehmasuk = true;
        }
        else if (col.GetComponent<PintuKeKamarAkhir>())
        {
            scenetoload = "Kamartidur akhir";
            bolehmasuk = true;
        }
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
            || col.GetComponent<PintuWarehouse>() || col.GetComponent<JalanKeRS>() || col.GetComponent<PintuMasukRS>() || col.GetComponent<PintuRSLt1>()
            || col.GetComponent<TanggaRSKeLt2>() || col.GetComponent<TanggaRSKeLt1>() || col.GetComponent<PintuRuangDokter>() || col.GetComponent<TanggaRSKeLt3>() 
            || col.GetComponent<PintuRSLt2>() || col.GetComponent<PintuKeRP1>() || col.GetComponent<PintuKeRP2>() || col.GetComponent<PintuKeRP3>() 
            || col.GetComponent<PintuKeRP4>() || col.GetComponent<PintuRSLt3>() || col.GetComponent<TanggaRS3KeLt2>() || col.GetComponent<PintuKamarMayat>() 
            || col.GetComponent<PintuRP3>() || col.GetComponent<PintuRP4>() || col.GetComponent<PintuRP1>() || col.GetComponent<PintuRP2>() 
            || col.GetComponent<PintuLab>() || col.GetComponent<PintuKeluarRS>() || col.GetComponent<Brankas>() || col.GetComponent<KeKotaDariRS>() 
            || col.GetComponent<LorongDariKota>() || col.GetComponent<PintuKeRuangAkhir>() || col.GetComponent<PintuKeKamarAkhir>())
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

            if (jigsawPuzzleCompleted)
            {
                bolehmasuk = true;
                SceneManager.LoadScene("RuangDokter"); // Pindah ke scene tujuan
            }
            else if (puzzlePieceDiambil)
            {
                OpenRealPuzzle(); // Buka puzzle asli jika item telah ditemukan
            }
            else
            {
                OpenFakePuzzle(); // Buka puzzle palsu jika item belum ditemukan
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

    private void OpenFakePuzzle()
    {
        fakeJigsawCanvas.SetActive(true); // Menampilkan Canvas fakeJigsaw
        Time.timeScale = 0f; // Memberhentikan waktu di game
    }

    private void OpenRealPuzzle()
    {
        realJigsawCanvas.SetActive(true); // Menampilkan Canvas realJigsaw
        Time.timeScale = 0f; // Memberhentikan waktu di game

        Debug.Log("Initializing puzzle");
        jigsawScript.InitializePuzzle();
        jigsawScript.OnPuzzleCompleted += HandleJigsawPuzzleCompleted;
    }

    public void CloseFakePuzzleAndStartMonolog()
    {
        fakeJigsawCanvas.SetActive(false); // Menyembunyikan Canvas fakeJigsaw
        Time.timeScale = 1f; // Melanjutkan waktu di game

        // Memulai monolog setelah fakeJigsawCanvas ditutup
        if (monologPuzzle != null)
        {
            monologPuzzle.StartMonolog(() =>
            {
                monologPuzzle.dialogpanel.SetActive(false);
                // Callback setelah monolog selesai
            });
        }
    }

    private void HandleJigsawPuzzleCompleted()
    {
        jigsawScript.OnPuzzleCompleted -= HandleJigsawPuzzleCompleted;
        
        jigsawScript.StopInteract();
        realJigsawCanvas.SetActive(false); // Menyembunyikan Canvas realJigsaw
        Time.timeScale = 1f; // Melanjutkan waktu di game
        jigsawPuzzleCompleted = true; // Set jigsawPuzzleCompleted true setelah puzzle selesai
        SaveCheckpoint();
        SceneManager.LoadScene("RuangDokter"); // Pindah ke scene tujuan
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
        else
        {
            StartCoroutine(ShowMonologAfterSceneLoad());
        }
    }

    private IEnumerator ShowMonologAfterSceneLoad()
    {
        yield return null; // Tunggu satu frame untuk memastikan scene telah dimuat
        monologPuzzle.MulaiMonologDiakhir();
    }

    public void SaveCheckpoint()
    {
        PlayerPrefs.SetInt("CheckpointReached", 1);
        PlayerPrefs.SetInt("PuzzleSolved", SimpleClock.isPuzzleSolved ? 1 : 0);
        PlayerPrefs.SetInt("KunciRSDiambil", kunciRSDiambil ? 1 : 0);
        PlayerPrefs.SetInt("PuzzleSolved", jigsawPuzzleCompleted ? 1 : 0);
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
            jigsawPuzzleCompleted = PlayerPrefs.GetInt("PuzzleSolved", 0) == 1;

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