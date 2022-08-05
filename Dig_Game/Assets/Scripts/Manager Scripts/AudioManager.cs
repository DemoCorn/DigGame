using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource footsteps;
    
    private void Update()
    {
        if (GameManager.Instance.GetPlayerMovement().getGrounded() && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            footsteps.enabled = true;
        }

        else
        {
            footsteps.enabled = false;
        }
    }

}
