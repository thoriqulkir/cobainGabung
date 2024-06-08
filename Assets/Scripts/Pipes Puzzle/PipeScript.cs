using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] _rotations = {0, 90, 180, 270};

    public float[] correctRotation;
    
    [SerializeField]
    bool isPlaced = false;

    int possibleRotation = 1;

    PuzzleManager gameManager;
    
    private void Awake()
    {
        gameManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    }

    void Start()
    {
        possibleRotation = correctRotation.Length;
        int _random = Random.Range(0, _rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, _rotations[_random]);

        if (possibleRotation > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }
        
    }

    private void OnMouseDown()
    {
        transform.Rotate(0, 0, 90);
        transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z));

        if (possibleRotation > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1]
                && isPlaced == false)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
    }
}
