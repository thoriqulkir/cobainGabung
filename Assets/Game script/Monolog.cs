using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monolog : MonoBehaviour
{
    public GameObject dialogpanel;
    public Text diloguetext;
    public GameObject triggerisclose;
    public string[] dialog;
    private int index;

    public float wordspeed;
    public bool playerisclose;
    public bool isKeyObject = false;  // New variable to check if the object requires a key

    public event Action OnMonologCompleted;

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            if (isKeyObject == true && (Masukpintu.kunciKamarDiambil || Masukpintu.lockpickDiambil))
            {
                return; // Do not display dialog if items are taken for key objects
            }

            if (dialogpanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialogpanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if (dialogpanel.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            if (diloguetext.text == dialog[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                diloguetext.text = dialog[index];
            }
        }
        if (playerisclose == true)
        {
            triggerisclose.SetActive(true);
        }
        else
        {
            triggerisclose.SetActive(false);
        }
    }
    
    public void StartMonolog()
    {
        if (!dialogpanel.activeInHierarchy)
        {
            dialogpanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }
    
    public void zeroText()
    {
        diloguetext.text = "";
        index = 0;
        dialogpanel.SetActive(false);
        OnMonologCompleted?.Invoke();
    }
    
    IEnumerator Typing()
    {
        foreach (char letter in dialog[index].ToCharArray())
        {
            diloguetext.text += letter;
            yield return new WaitForSeconds(wordspeed);
        }
    }
    
    public void NextLine()
    {
        if (index < dialog.Length - 1)
        {
            index++;
            diloguetext.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerisclose = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerisclose = false;
            zeroText();
        }
    }
}