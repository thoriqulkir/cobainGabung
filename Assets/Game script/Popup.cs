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
        // Pastikan popup tidak muncul saat game di-pause
        if (PauseMenu.isPaused)
        {
            popupbox.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && playerisclose)
        {
            popupbox.SetActive(true);
            if (anim != null)
            {
                anim.SetTrigger("pop");
            }
            else
            {
                Debug.LogError("Animator is not assigned.");
            }
        }
        
        if (Input.GetMouseButton(0) && popupbox.activeSelf)
        {
            if (anim != null)
            {
                anim.SetTrigger("close");
                // Optional: Deactivate popupbox after close animation
                StartCoroutine(DeactivateAfterAnimation());
            }
            else
            {
                Debug.LogError("Animator is not assigned.");
            }
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

    // Coroutine to deactivate popupbox after animation
    private IEnumerator DeactivateAfterAnimation()
    {
        // Wait until the end of the frame to ensure animation has started
        yield return new WaitForEndOfFrame();

        // Wait until the animation is finished
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        // Deactivate the popupbox
        popupbox.SetActive(false);
    }
}