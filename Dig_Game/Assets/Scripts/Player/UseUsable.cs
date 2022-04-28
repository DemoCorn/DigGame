using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseUsable : MonoBehaviour
{
    public Usable[] item = new Usable[3];
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            item[0].effect.GetComponent<UsableEffect>().Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            item[1].effect.GetComponent<UsableEffect>().Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            item[2].effect.GetComponent<UsableEffect>().Activate();
        }
    }
}
