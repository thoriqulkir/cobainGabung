using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialoguesystem
{
    public class Dialogholder : MonoBehaviour
    {
        [SerializeField] private GameObject backgorund;
        [SerializeField] private string scenename;
        private void Awake() 
        {
            StartCoroutine(dialogsequence());
        }
        private IEnumerator dialogsequence() 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<Dialoglines>().finished);
            }
            gameObject.SetActive(false);
            backgorund.SetActive(false);
            SceneManager.LoadScene(scenename);
        }
        private void deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
