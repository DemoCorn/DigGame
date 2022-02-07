using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject boss;
    public Vector2 position1;
    public Vector2[] bossPositions = new Vector2[4];
    int index;
    public Vector2 currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        Teleport();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Teleport()
    {
        index = Random.Range(0, bossPositions.Length);
        currentPosition = bossPositions[index];
        transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Teleport();
            Debug.Log("Player");
        }
    }
}
