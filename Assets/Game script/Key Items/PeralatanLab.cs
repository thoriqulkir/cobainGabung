using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PeralatanLab : MonoBehaviour
{
    [SerializeField]
    private string pipePuzzleSceneName = "Pipes Puzzle";
    
    [SerializeField]
    private GameObject monologObject;
    private bool isClose = false;
    private bool pipePuzzleCompleted = false;

    private Monolog monolog;

    void Start()
    {
        pipePuzzleCompleted = PlayerPrefs.GetInt("PipePuzzleCompleted", 0) == 1;
        if (pipePuzzleCompleted)
        {
            Masukpintu.cairanGembokDiambil = true;
        }

        monolog = monologObject.GetComponent<Monolog>();
        monolog.OnMonologCompleted += HandleMonologCompleted;
    }

    private void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E) && !pipePuzzleCompleted)
        {
            monolog.StartMonolog();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isClose = false;
        }
    }

    private void HandleMonologCompleted()
    {
        if (!pipePuzzleCompleted)
        {
            OpenPipePuzzle();
        }
    }

    void OpenPipePuzzle()
    {
        // Simpan nama scene saat ini
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Pindah ke scene puzzle
        SceneManager.LoadScene(pipePuzzleSceneName);
    }
}
