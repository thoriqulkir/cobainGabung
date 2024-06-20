using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public GameObject pipesHolder;
    public GameObject[] pipes;

    [SerializeField]
    int totalPipes = 0;

    [SerializeField]
    int correctedPipes = 0;

    // public string previousSceneName;

    void Start()
    {
        // previousSceneName = PlayerPrefs.GetString("PreviousScene", "");
        
        totalPipes = pipesHolder.transform.childCount;

        pipes = new GameObject[totalPipes];
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        if (correctedPipes == totalPipes)
        {
            Debug.Log("PUZZLE COMPLETED!");
            CompletePuzzle();
        }
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }

    public void CompletePuzzle()
    {
        // Simpan status bahwa puzzle telah selesai
        PlayerPrefs.SetInt("PipePuzzleCompleted", 1);
        PlayerPrefs.Save();

        // Kembali ke scene sebelumnya
        SceneManager.LoadScene("LaboratoriumDone");
    }
}
