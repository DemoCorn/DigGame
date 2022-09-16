using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static ParticleManager instance { get; set; }

    [SerializeField] AudioSource useSFX;

    public List<ParticleSystem> ps;
    ParticleSystem.Particle[] particles;

    GameObject player;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        useSFX = GetComponentInChildren<AudioSource>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Instantiate(ps[0], player.transform.position, Quaternion.identity);
        //}   
    }

    public void effect1()
    {
        Instantiate(this.ps[0], player.transform.position, Quaternion.identity);
        if (useSFX)
        {
            useSFX.Play();
        }
    }

    public void effect2()
    {
        Instantiate(ps[1], player.transform.position, Quaternion.identity);
        if (useSFX)
        {
            useSFX.Play();
        }
    }

    public void effect3()
    {
        Instantiate(ps[2], player.transform.position, Quaternion.identity);
    }
}
