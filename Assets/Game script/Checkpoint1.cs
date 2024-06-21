using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    public Masukpintu masukPintuScript;
    public GameObject PanelCheckpoint;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            masukPintuScript.SaveCheckpoint();
            PanelCheckpoint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PanelCheckpoint.SetActive(false);
        }
    }
}
