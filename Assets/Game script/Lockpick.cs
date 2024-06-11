using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : MonoBehaviour
{
    public GameObject PintuTengah2;
    public Masukpintu masukPintuScript;
    
    public bool playerisclose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose == true)
        {
            Debug.Log("Mendapat Lockpick dari laci");

            this.gameObject.SetActive(false);

            Masukpintu.lockpickDiambil = true;

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
