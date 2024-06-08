using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    // float player
    public float horizontalAxis;
    Vector2 direction;
    public float speed = 10f;
 
    // private
    private Rigidbody2D rb;
    private Animator anim;

    // Void start
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update void
    void Update()
    {
        Run();
    }
    void Run()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        direction = new Vector2 (horizontalAxis, 0);
        anim.SetBool("Run", false);
        transform.Translate(direction * Time.deltaTime * speed);

         if (horizontalAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("Run", true); 
        }

        if (horizontalAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Run", true);
        }
    }
}
