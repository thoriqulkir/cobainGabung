using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClock : MonoBehaviour
{
    [SerializeField]
    private Transform hourHand, minuteHand;

    [SerializeField]
    private GameObject winText;

    private void Start()
    {
        winText.SetActive(false);
    }

    private void rotateClockwise(Transform hand, float angle)
    {
        hand.Rotate(Vector3.back, angle);
    }

    private void rotateCounterClockwise(Transform hand, float angle)
    {
        hand.Rotate(Vector3.forward, angle);
    }

    // pergeseran jarum panjang dan pendek
    public void rotateHandClockwise()
    {
        rotateClockwise(minuteHand, 30 /*30 derajat maksudnya jarum menit loncat setiap kelipatan 5 menit*/);
        rotateClockwise(hourHand, 2.5f /*tiap 2.5 derajat*/);
        checkWinCondition();
    }

    public void rotateHandCounterClockwise()
    {
        rotateCounterClockwise(minuteHand, 30);
        rotateCounterClockwise(hourHand, 2.5f);
        checkWinCondition();
    }
    
    // jawaban target jam yang akan dituju
    private void checkWinCondition()
    {
        if ((Mathf.Round(minuteHand.rotation.eulerAngles.z * 2) / 2) == 180 /*nambahnya sesuai arah jarum jam (clockwise)*/
            && (Mathf.Round(hourHand.rotation.eulerAngles.z * 2) / 2) == 75f /*nambahnya berlawanan arah jarum jam (counter-clockwise)*/)
        {
            winText.SetActive(true);
        }
    }
}
