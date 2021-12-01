using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral_Logic : MonoBehaviour
{
    // Attach to mineral Object
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == false)
        {
            GameManager.Instance.AddScore(0);
        }
    }


}
