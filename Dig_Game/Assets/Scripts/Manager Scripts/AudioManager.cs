using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Player_Movement pm;

    public AudioSource footsteps;
    
    private void Update()
    {
        if (pm.getGrounded() && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            footsteps.enabled = true;
        }

        else
        {
            footsteps.enabled = false;
        }
    }

}
