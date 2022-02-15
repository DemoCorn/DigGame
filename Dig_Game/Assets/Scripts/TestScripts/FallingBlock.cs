using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public bool objectTrigger = true;
    public float speed;
    public float yValue;
    public float xValue;

    private Vector3 moveVector;


    private void BeginFalling()
    {
        transform.Translate(moveVector * speed * Time.deltaTime);
    }

    public void Start()
    {
        moveVector = new Vector3(xValue, yValue, 0);
        Debug.Log(moveVector);
    }

    private void Update()
    {
        if(objectTrigger == true)
        {
            BeginFalling();
        }

        if (Physics2D.OverlapCircle(transform.position , 0.5f))
        {
            objectTrigger = false;
        }
    }
}
