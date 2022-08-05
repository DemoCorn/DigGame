using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraConfiner;

    public Transform bossStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = bossStartPosition.transform.position;
            Destroy(cameraConfiner);
            
        }
    }
}
