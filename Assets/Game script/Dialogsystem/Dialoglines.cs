using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Dialoguesystem
{
    public class Dialoglines : Dialogbaseclass
    {
        private Text textholder;

        [Header ("Text")]
        [SerializeField] private string input;
        

        [Header ("Time Parameter")]
        [SerializeField] private float delay;
        [SerializeField] private float delaylines;


        [Header ("Picture")]
        [SerializeField] private Sprite backgroundchar;
        [SerializeField] private Image imageholder;


        private void Awake() 
        {
            textholder = GetComponent<Text>();   
            textholder.text = "";

            imageholder.sprite = backgroundchar;
            imageholder.preserveAspect = true;
        }
        private void Start() 
        {
            StartCoroutine(WriteText(input, textholder, delay, delaylines));
        }
    }
}

