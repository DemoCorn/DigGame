using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;

    public Transform bossStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        
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
            boss.SetActive(true);
        }
    }
}
