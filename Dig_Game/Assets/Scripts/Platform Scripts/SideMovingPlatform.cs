using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovingPlatform : MonoBehaviour
{
    private float yValue;
    private float xValue;
    [SerializeField] private float yValueEndPosition;
    [SerializeField] private float xValueEndPosition;
    private Vector3 moveVector;
    private GameObject playerForTransform;
    private Vector2 posi;
    private Vector2 RangeVector;
    [SerializeField] private float Range;
    [SerializeField] private float moveSpeed;
    private Vector2 Endposition;
    private Vector2 StartPosition;
    private bool state;
    private BoxCollider2D BoxColl;
    private bool Collided;


    // Start is called before the first frame update
    void Start()
    {
        posi = transform.position;
        xValue = posi.x;
        yValue = posi.y;
        if (xValue < 0)
        {
            xValue = xValue * (-1);
        }
        moveVector = new Vector3(xValue, 0, 0);
        StartPosition = posi;
        state = false;
        playerForTransform = GameObject.FindGameObjectsWithTag("Player")[0];
        Collided = false;
        BoxColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        posi = transform.position;

        if (state == false)
        {
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
            if (posi.x >= xValueEndPosition)
            {
                state = true;
            }
        }

        if (state == true)
        {
            transform.Translate(moveVector * -moveSpeed * Time.deltaTime);
            if (posi.x <= StartPosition.x)
            {
                state = false;
            }

        }

        if (CheckGrounded() && Collided == false)
        {
            Debug.Log("Collided with moving platform");
            playerForTransform.gameObject.transform.SetParent(gameObject.transform, true);
            Collided = true;
        }
        else if ((CheckGrounded() == false) && Collided == true)
        {
            playerForTransform.gameObject.transform.parent = null;
            Collided = false;
        }
    }

    private bool CheckGrounded()
    {
        Debug.Log("ground check! moving platform");
        RaycastHit2D hit = Physics2D.BoxCast(BoxColl.bounds.center, BoxColl.bounds.size, 0f, Vector2.up, 0.2f);
        return hit;
    }
}
