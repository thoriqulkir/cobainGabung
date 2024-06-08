using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleLockScript : MonoBehaviour
{
    public bool interactable = true;
    public GameObject lockCanvas;
    public Text[] _text;
    public Sprite unlockSprite;

    public string password;
    public string[] lockCharacterChoices;
    public int[] _lockCharacterNumbers;
    public string _insertedPass;


    void Start()
    {
        _lockCharacterNumbers = new int[password.Length];
        UpdateUI();
    }
    

    public void ChangeInsertedPass(int position, bool isNext)
    {
        if (isNext)
        {
            _lockCharacterNumbers[position]++;
            if (_lockCharacterNumbers[position] >= lockCharacterChoices[position].Length)
            {
                _lockCharacterNumbers[position] = 0;
            }
        }
        else
        {
            _lockCharacterNumbers[position]--;
            if (_lockCharacterNumbers[position] < 0)
            {
                _lockCharacterNumbers[position] = lockCharacterChoices[position].Length - 1;
            }
        }

        CheckPass();
        UpdateUI();
    }
    

    public void CheckPass()
    {
        int passLength = password.Length;
        _insertedPass = "";
        
        for (int i = 0; i < passLength; i++)
        {
            _insertedPass += lockCharacterChoices[i][_lockCharacterNumbers[i]].ToString();
        }

        if (password == _insertedPass)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        interactable = false;
        StopInteract();

        gameObject.GetComponent<SpriteRenderer>().sprite = unlockSprite;
    }


    public void UpdateUI()
    {
        int _length = _text.Length;

        for (int i = 0; i < _length; i++)
        {
            _text[i].text = lockCharacterChoices[i][_lockCharacterNumbers[i]].ToString();
        }
    }


    private void OnMouseDown()
    {
        Interact();
    }


    public void Interact()
    {
        if (interactable)
        {
            lockCanvas.SetActive(true);
        }
    }

    public void StopInteract()
    {
        lockCanvas.SetActive(false);
    }

    public void OnNextButtonPressed(int position)
    {
        ChangeInsertedPass(position, false);
    }

    public void OnPreviousButtonPressed(int position)
    {
        ChangeInsertedPass(position, true);
    }

}
