using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public Player_Movement pm;

    public GameObject jumpDust;
    public Transform jumpPoint;
    public GameObject landingDust;
    public Transform landingPoint;

    public float landingDustHight;

    bool firstLoop;
    bool highV;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pm.getGrounded())
        {
            jumpDust.transform.position = jumpPoint.position;
            jumpDust.SetActive(true);
        }


        if (pm.getGrounded())
        {
            firstLoop = true;

            if (highV)
            {
                landingDust.transform.position = landingPoint.position;
                landingDust.SetActive(true);
            }

            highV = false;
        }

        if (!pm.getGrounded() && firstLoop)
        {
            firstLoop = false;

            Invoke("DistanceCheck", landingDustHight);
        }
    }

    void DistanceCheck()
    {
        if (!pm.getGrounded())
        {
            highV = true;
        }
    }
}