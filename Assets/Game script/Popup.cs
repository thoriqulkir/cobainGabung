using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popupbox;
    public Animator anim;
    public bool playerisclose;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            popupbox.SetActive(true);
            anim.SetTrigger("pop");
        }
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("close");
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
        }
    }
}

