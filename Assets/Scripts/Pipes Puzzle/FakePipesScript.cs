using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePipesScript : MonoBehaviour
{
    float[] _rotations = {0, 90, 180, 270};

    void Start()
    {
        int _random = Random.Range(0, _rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, _rotations[_random]);
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }
}
