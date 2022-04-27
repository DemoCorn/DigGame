using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToMain : MonoBehaviour
{
    public GameObject player;

    public Transform mainStartPosition;
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
            player.transform.position = mainStartPosition.transform.position;
            Debug.Log("player detected");
        }
    }
}
