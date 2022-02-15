using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeyScript : MonoBehaviour

{

    public bool ctrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
        private bool isPickedUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ctrl = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ctrl = false;

            if (isPickedUp)
            {
                transform.SetParent(null);
                isPickedUp = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (ctrl && !isPickedUp && collision.gameObject.tag == "Player")
        {
            transform.SetParent(collision.transform, true);
            isPickedUp = true;
        }
    }
}