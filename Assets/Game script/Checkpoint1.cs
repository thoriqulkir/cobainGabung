using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    public Masukpintu masukPintuScript;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            masukPintuScript.SaveCheckpoint();
        }
    }
}
