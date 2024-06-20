using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescenebutton : MonoBehaviour
{
    public void movetoscene(int namescenename) 
    {  
        SceneManager.LoadScene(namescenename);
    }
}
