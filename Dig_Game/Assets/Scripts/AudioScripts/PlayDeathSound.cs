using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeathSound : MonoBehaviour
{
    [SerializeField] AudioSource deathSFX;
    [SerializeField] GameObject dyingObject;

    private bool soundBool = false;
    // Start is called before the first frame update
    void Start()
    {
        deathSFX = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dyingObject == null)
        {
            if (!soundBool)
            {
                deathSFX.Play();
                soundBool = true;
            }
            
        }
    }
}
