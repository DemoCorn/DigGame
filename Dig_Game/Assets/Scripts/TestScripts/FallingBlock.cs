using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public bool objectTrigger = true;
    public float speed;
    public float yValue;
    public float xValue;

    private float rayDistance;

    private Vector3 moveVector;


    public void BeginFalling()
    {
        transform.Translate(moveVector * speed * Time.deltaTime);
    }

    public void Start()
    {
        rayDistance = transform.localScale.y * 0.5f;
        moveVector = new Vector3(xValue, yValue, 0);
        Debug.Log(moveVector);
    }

    private void Update()
    {
        if(objectTrigger == true)
        {
            BeginFalling();

            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, yValue), rayDistance);
            //objectTrigger = Physics2D(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + 0.01);

            //if (Physics2D.OverlapCircle(transform.position , 0.5f, 8))
            if (hit.transform != null)
            {
                objectTrigger = false;
                transform.position = new Vector3((float)System.Math.Round(transform.position.x), (float)System.Math.Round(transform.position.y));
            }
        }
    }
}
