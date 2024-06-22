using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DapatRamuan : MonoBehaviour
{
    private bool pipePuzzleCompleted = false;

    public Masukpintu masukPintuScript;
    public Popup popup;
    public Monolog monolog;
    
    // Start is called before the first frame update
    void Start()
    {
        pipePuzzleCompleted = PlayerPrefs.GetInt("PipePuzzleCompleted", 0) == 1;
        if (pipePuzzleCompleted)
        {
            Masukpintu.cairanGembokDiambil = true;
            popup.StartPopUp();
            monolog.StartMonolog();
        }
    }
}
