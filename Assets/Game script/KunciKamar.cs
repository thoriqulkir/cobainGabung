using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunciKamar : MonoBehaviour
{
    public GameObject PintuKamar;
    public Masukpintu masukPintuScript;
    
    public bool playerisclose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose == true)
        {
            Debug.Log("Mendapat Kunci Kamar");

            this.gameObject.SetActive(false);

            Masukpintu.kunciKamarDiambil = true;

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
