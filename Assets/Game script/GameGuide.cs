using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameGuide : MonoBehaviour
{
    public GameObject gameGuidePanel;
    
    public void ShowGameGuide()
    {
        gameGuidePanel.SetActive(true);
        Time.timeScale = 0f;
        if (Input.GetMouseButton(0))
        {
            Close();
        }
    }

    public void Close()
    {
        gameGuidePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
