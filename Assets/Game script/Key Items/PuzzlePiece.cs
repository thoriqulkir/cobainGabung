using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Masukpintu masukPintuScript;
    
    public bool playerisclose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            Debug.Log("Mendapat Potongan Puzzle");
            //this.gameObject.SetActive(false);
            Masukpintu.puzzlePieceDiambil = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerisclose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerisclose = false;
        }
    }
}