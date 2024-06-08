using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monolog : MonoBehaviour
{
    public GameObject dialogpanel;
    public Text diloguetext;
    public string[] dialog;
    private int index;

    public float wordspeed;
    public bool playerisclose;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
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
    }
    public void zeroText() 
    {
        diloguetext.text = "";
        index = 0;
        dialogpanel.SetActive(false);
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
