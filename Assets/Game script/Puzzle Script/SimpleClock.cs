using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleClock : MonoBehaviour
{
    [SerializeField]
    private RectTransform hourHand, minuteHand;

    [SerializeField]
    private GameObject ClockCanvas;

    public static bool isPuzzleSolved = false;
    public bool interactable = true;

    private bool hasInteractedBefore = false;
    public MonologPuzzle monologPuzzle;

    public delegate void PuzzleCompletedHandler();
    public event PuzzleCompletedHandler OnPuzzleCompleted;

    private bool isPlayerNearby = false;

    private void Start()
    {
        ClockCanvas.SetActive(false);
        hasInteractedBefore = PlayerPrefs.GetInt(gameObject.name + "_InteractedBefore", 0) == 1;
        isPuzzleSolved = PlayerPrefs.GetInt("ClockPuzzleSolved", 0) == 1; // Load status puzzle
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void rotateClockwise(RectTransform hand, float angle)
    {
        hand.Rotate(Vector3.back, angle);
    }

    private void rotateCounterClockwise(RectTransform hand, float angle)
    {
        hand.Rotate(Vector3.forward, angle);
    }

    // pergeseran jarum panjang dan pendek
    public void rotateHandClockwise()
    {
        rotateClockwise(minuteHand, 30 /*30 derajat maksudnya jarum menit loncat setiap kelipatan 5 menit*/);
        rotateClockwise(hourHand, 2.5f /*tiap 2.5 derajat*/);
        checkWinCondition();
    }

    public void rotateHandCounterClockwise()
    {
        rotateCounterClockwise(minuteHand, 30);
        rotateCounterClockwise(hourHand, 2.5f);
        checkWinCondition();
    }
    
    // jawaban target jam yang akan dituju
    private void checkWinCondition()
    {
        if ((Mathf.Round(minuteHand.rotation.eulerAngles.z * 2) / 2) == 180 /*nambahnya sesuai arah jarum jam (clockwise)*/
            && (Mathf.Round(hourHand.rotation.eulerAngles.z * 2) / 2) == 255f /*nambahnya berlawanan arah jarum jam (counter-clockwise)*/)
        {
            interactable = false;
            isPuzzleSolved = true;
            PlayerPrefs.SetInt("ClockPuzzleSolved", 1); // Simpan status puzzle
            PlayerPrefs.Save();
            StopInteract();
            OnPuzzleCompleted?.Invoke();
        }
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
                        ClockCanvas.SetActive(true);
                        Time.timeScale = 0f; // Menghentikan waktu saat puzzle aktif
                    });
                    hasInteractedBefore = true;
                    PlayerPrefs.SetInt(gameObject.name + "_InteractedBefore", 1); // Simpan status interaksi pertama kali
                    PlayerPrefs.Save();
                }
                else
                {
                    Debug.LogWarning("script MonologPuzzle tidak ditemukan");
                    ClockCanvas.SetActive(true);
                    Time.timeScale = 0f; // Menghentikan waktu saat puzzle aktif
                }
            }
            else
            {
                ClockCanvas.SetActive(true);
                Time.timeScale = 0f; // Menghentikan waktu saat puzzle aktif
            }
        }
    }

    public void StopInteract()
    {
        ClockCanvas.SetActive(false);
        Time.timeScale = 1f; // Melanjutkan waktu saat puzzle tidak aktif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
