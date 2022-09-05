using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update

    static ParticleManager instance;
   
    public static ParticleManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ParticleManager();
            }
            return instance;
        }
    }

    public List<ParticleSystem> ps;
    ParticleSystem.Particle[] particles;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(ps[0], player.transform.position, Quaternion.identity);
        }   
    }

    public void effect1()
    {
        Instantiate(ps[0], player.transform.position, Quaternion.identity);
    }

    public void effect2()
    {
        Instantiate(ps[1], player.transform.position, Quaternion.identity);
    }

    public void effect3()
    {
        Instantiate(ps[2], player.transform.position, Quaternion.identity);
    }
}
