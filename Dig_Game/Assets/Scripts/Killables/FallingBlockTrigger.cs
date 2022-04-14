using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingBlockTrigger : MonoBehaviour
{
    public GameObject[] fallingObjects;
    public bool switc = true;

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            foreach (GameObject i in fallingObjects)
            {
                i.GetComponent<FallingBlock>().objectTrigger = true;
            }
        }
    }
}
