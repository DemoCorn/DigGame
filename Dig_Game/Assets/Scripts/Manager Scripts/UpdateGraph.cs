using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class UpdateGraph : MonoBehaviour
{
    AstarPath pathFinder;
    float updateTimer = 5.0f;
    private void Awake()
    {
        pathFinder = GetComponent<AstarPath>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateScan", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScan()
    {
        pathFinder.Scan();
    }

}
