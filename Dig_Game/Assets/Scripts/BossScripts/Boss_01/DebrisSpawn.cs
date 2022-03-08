using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawn : MonoBehaviour
{
    public GameObject debris;
    public Transform debrisSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag ==("Enemy"))
        {
            launchDebris();
            Debug.Log("ldododfajdfaijdf");
        }
        launchDebris();
        Debug.Log("ldododfajdfaijdf");
    }



    public void launchDebris()
    {
        Instantiate(debris, debrisSpawnLocation.position, debrisSpawnLocation.rotation);
    }
}
