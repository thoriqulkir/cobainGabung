using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunciRS : MonoBehaviour
{
    public Masukpintu masukPintuScript;
    
    public bool playerisclose;

    void Start()
    {
        masukPintuScript = FindObjectOfType<Masukpintu>();
        if (masukPintuScript == null)
        {
            Debug.LogError("Masukpintu script not found");
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            Debug.Log("Mendapat Kunci Ruang Rumah Sakit");
            PlayerPrefs.SetInt("KunciRSDiambil", 1); // Simpan status kunci
            PlayerPrefs.Save();
            //this.gameObject.SetActive(false); // Sembunyikan atau hapus objek kunci

            // Panggil metode SaveCheckpoint dari Masukpintu
            if (masukPintuScript != null)
            {
                Masukpintu.kunciRSDiambil = true;
                masukPintuScript.SaveCheckpoint();
            }
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
