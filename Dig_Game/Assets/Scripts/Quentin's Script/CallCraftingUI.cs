using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCraftingUI : MonoBehaviour
{
    [SerializeField] private GameObject craftingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ShowCraftingScreen");
            craftingScreen.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("DisableCraftingScreen");
            craftingScreen.SetActive(false);
           
        }
    }
}
