using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRetire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PLayer on campfire");
            collision.GetComponent<Player_Health>().hasRetired = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PLayer off campfire");
            collision.GetComponent<Player_Health>().hasRetired = false;
        }
    }


}