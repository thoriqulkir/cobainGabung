using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunciKamar : MonoBehaviour
{
    public GameObject PintuKamar;
    public Masukpintu masukPintuScript;
    public MonologItem monologScript;
    
    public bool playerisclose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            Debug.Log("Mendapat Kunci Kamar");

            // Panggil metode StartMonolog setelah mengambil kunci
            if (monologScript != null)
            {
                monologScript.MulaiMonologDiakhir();
            }

            // Gunakan coroutine untuk menunda penghilangan objek
            StartCoroutine(HideObjectAfterDelay());
        }
    }

    private IEnumerator HideObjectAfterDelay()
    {
        // Tunggu sebentar agar monolog bisa muncul
        yield return new WaitForSeconds(1.7f);

        this.gameObject.SetActive(false);

        Masukpintu.kunciKamarDiambil = true;
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