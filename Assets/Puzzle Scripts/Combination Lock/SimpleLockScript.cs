using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleLockScript : MonoBehaviour
{
    public bool interactable = true;
    public GameObject lockCanvas;
    public Text[] _text;
    public string password;
    public string[] lockCharacterChoices;
    public int[] _lockCharacterNumbers;
    public string _insertedPass;

    public static bool isPuzzleSolved = false; // Variabel statis untuk menyimpan status puzzle

    public delegate void PuzzleCompletedHandler();
    public event PuzzleCompletedHandler OnPuzzleCompleted;

    public bool hasInteractedBefore = false;
    public MonologPuzzle monologPuzzle;

    void Start()
    {
        // Muat status puzzle dari PlayerPrefs
        isPuzzleSolved = PlayerPrefs.GetInt("PuzzleSolved", 0) == 1;

        hasInteractedBefore = PlayerPrefs.GetInt(gameObject.name + "_InteractedBefore", 0) == 1;
        
        if (isPuzzleSolved)
        {
            interactable = false;
            gameObject.SetActive(false); // Atau set state yang sesuai jika puzzle sudah selesai
        }
        else
        {
            _lockCharacterNumbers = new int[password.Length];
            UpdateUI();
        }
    }

    public void ChangeInsertedPass(int position, bool isNext)
    {
        if (isNext)
        {
            _lockCharacterNumbers[position]++;
            if (_lockCharacterNumbers[position] >= lockCharacterChoices[position].Length)
            {
                _lockCharacterNumbers[position] = 0;
            }
        }
        else
        {
            _lockCharacterNumbers[position]--;
            if (_lockCharacterNumbers[position] < 0)
            {
                _lockCharacterNumbers[position] = lockCharacterChoices[position].Length - 1;
            }
        }

        CheckPass();
        UpdateUI();
    }

    public void CheckPass()
    {
        int passLength = password.Length;
        _insertedPass = "";
        
        for (int i = 0; i < passLength; i++)
        {
            _insertedPass += lockCharacterChoices[i][_lockCharacterNumbers[i]].ToString();
        }

        if (password == _insertedPass)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        interactable = false;
        isPuzzleSolved = true;
        PlayerPrefs.SetInt("PuzzleSolved", 1); // Simpan status puzzle
        PlayerPrefs.Save();
        StopInteract();
        OnPuzzleCompleted?.Invoke();
    }

    public void UpdateUI()
    {
        int _length = _text.Length;
        for (int i = 0; i < _length; i++)
        {
            _text[i].text = lockCharacterChoices[i][_lockCharacterNumbers[i]].ToString();
        }
    }

    private void OnMouseDown()
    {
        //Interact();
    }

    public void Interact()
    {
        if (interactable)
        {
            if (!hasInteractedBefore)
            {
                // Panggil monolog di sini jika interaksi pertama kali
                if (monologPuzzle != null)
                {
                    monologPuzzle.StartMonolog(() =>
                    {
                        lockCanvas.SetActive(true);
                        Time.timeScale = 0f; // Menghentikan waktu saat puzzle aktif
                    });
                    hasInteractedBefore = true;
                    PlayerPrefs.SetInt(gameObject.name + "_InteractedBefore", 1); // Simpan status interaksi pertama kali
                    PlayerPrefs.Save();
                }
                else
                {
                    Debug.LogWarning("script MonologPuzzle tidak ditemukan");
                    lockCanvas.SetActive(true);
                    Time.timeScale = 0f; // Menghentikan waktu saat puzzle aktif
                }
            }
            else
            {
                lockCanvas.SetActive(true);
                Time.timeScale = 0f; // Menghentikan waktu saat puzzle aktif
            }
        }
    }

    public void StopInteract()
    {
        lockCanvas.SetActive(false);
        Time.timeScale = 1f; // Melanjutkan waktu saat puzzle tidak aktif
    }

    public void OnNextButtonPressed(int position)
    {
        ChangeInsertedPass(position, false);
    }

    public void OnPreviousButtonPressed(int position)
    {
        ChangeInsertedPass(position, true);
    }
}
