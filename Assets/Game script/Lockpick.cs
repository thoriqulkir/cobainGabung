using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : MonoBehaviour
{
    public GameObject PintuTengah2;
    public Masukpintu masukPintuScript;
    public Monolog monologScript;
    
    public bool playerisclose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            Debug.Log("Mendapat Lockpick dari laci");

            // Panggil metode StartMonolog setelah mengambil kunci
            if (monologScript != null)
            {
                monologScript.isKeyObject = true;
                monologScript.StartMonolog();
            }

            // Gunakan coroutine untuk menunda penghilangan objek
            StartCoroutine(HideObjectAfterDelay());
        }
    }

    private IEnumerator HideObjectAfterDelay()
    {
        // Tunggu sebentar agar monolog bisa muncul
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);

        Masukpintu.lockpickDiambil = true;
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
