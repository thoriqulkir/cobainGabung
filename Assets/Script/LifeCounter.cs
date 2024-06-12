using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;

    public void LoseLife(){
        livesRemaining--;

        lives[livesRemaining].enabled = false;

        if(livesRemaining==0){
            Debug.Log("You Died");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Hantu")){
            LoseLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Hantu")){
            LoseLife();
        }
    }
    
    private void Update(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoseLife();
        }
    }
}
