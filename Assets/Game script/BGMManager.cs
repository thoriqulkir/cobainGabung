using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    public AudioSource audioSource;
    public AudioClip defaultClip;
    public AudioClip secondaryClip;
    public AudioClip thirdClip;
    public AudioClip fourthClip;
    public AudioClip fifthClip;


    private HashSet<string> dirumahDanDiluar = new HashSet<string> { "Kamartidur", "Kamartidur2", "Kamarmandi", "Ruangtengah", "Ruangtengah2", "Perumahan", "Kota", "KotaDariSekolah", 
    "Kota Dari RS", "Perumahan Dari Kota", "RumahSakit", "RumahSakitKeluar"};
    private HashSet<string> khususCutscenePerum = new HashSet<string> { "Cutscene Perumahan"};
    private HashSet<string> lorongSekolahRumkit = new HashSet<string> { "Sekolah1", "Sekolah1DariLt2", "Sekolah1DariWarehouse", "Sekolah2", "Sekolah2DariKelas1", "Sekolah2DariKelas2", "Sekolah2DariKepsek", "Sekolah2DariLt3", 
    "Sekolah3", "Sekolah3DariKelas3", "Sekolah3DariKelas4", "Sekolah3DariPerpus", "RSLt1", "RSLt1 Dari Lt2", "RSLt1 Dari RuangDokter", "RSLt2", "RSLt2 Dari Lab", "RSLt2 Dari Lt3", "RSLt2 Dari RP1", "RSLt2 Dari RP2", "RSLt3", "RSLt3 Dari KamarMayat", 
    "RSLt3 Dari RP3", "RSLt3 Dari RP4"};
    private HashSet<string> ruanganSekolahRumkit = new HashSet<string> { "Perpustakaan", "Warehouse", "Kelas1", "Kelas2", "Kelas3", "Kelas4", "Kepsek", "RuangDokter", 
    "KamarMayat", "Laboratorium", "RuangPasien1", "RuangPasien2", "RuangPasien3", "RuangPasien4", "Laboratorium Done"};
    private HashSet<string> sceneEnding = new HashSet<string> { "Ruangtengah akhir (monolog)", "Ruangtengah akhir (objektif)", "Kamartidur akhir", "Credits"};

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckAndPlayMusic(scene.name);
    }

    void CheckAndPlayMusic(string sceneName)
    {
        if (dirumahDanDiluar.Contains(sceneName))
        {
            if (audioSource.clip != defaultClip)
            {
                audioSource.clip = defaultClip;
                audioSource.Play();
            }
        }
        else if (khususCutscenePerum.Contains(sceneName))
        {
            if (audioSource.clip != secondaryClip)
            {
                audioSource.clip = secondaryClip;
                audioSource.Play();
            }
        }
        else if (lorongSekolahRumkit.Contains(sceneName))
        {
            if (audioSource.clip != thirdClip)
            {
                audioSource.clip = thirdClip;
                audioSource.Play();
            }
        }
        else if (ruanganSekolahRumkit.Contains(sceneName))
        {
            if (audioSource.clip != fourthClip)
            {
                audioSource.clip = fourthClip;
                audioSource.Play();
            }
        }
        else if (sceneEnding.Contains(sceneName))
        {
            if (audioSource.clip != fifthClip)
            {
                audioSource.clip = fifthClip;
                audioSource.Play();
            }
        }
        else
        {
            // This is for scenes like Scene5 or any other scenes not mentioned in the sets
            audioSource.Stop();
        }
    }
}
