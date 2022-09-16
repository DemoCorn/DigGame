using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    [SerializeField] private GameObject healthBox;

    [SerializeField] private Animator brokenBarrel;
    private bool isDestroyed = false;


    // Update is called once per frame
    void Update()
    {
        if (isDestroyed == false)
        {
            if (healthBox == null)
            {
                CinemachineShakeCam.Instance.ShakeCamera(.8f, .05f);
                brokenBarrel.SetBool("isDestroyed", true);
                isDestroyed = true;
            }
        }
        
    }
}
