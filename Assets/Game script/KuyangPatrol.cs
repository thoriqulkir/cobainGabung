using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuyangPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentpoint;
    public float speed;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        currentpoint = pointA.transform;
    }

    private void Update() 
    {
        Vector2 point = currentpoint.position - transform.position;
        if (currentpoint == pointA.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentpoint.position) < 5 && currentpoint == pointB.transform)
        {
            flip();
            currentpoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentpoint.position) < 5 && currentpoint == pointA.transform)
        {
            flip();
            currentpoint = pointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
