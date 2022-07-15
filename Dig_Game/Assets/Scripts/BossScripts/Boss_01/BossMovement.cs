using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject bossMinion;
    public Vector2[] bossPositions = new Vector2[6];
    int index;
    public Vector2 currentPosition;
   
    public ParticleSystem teleportParticles;

   


    // Start is called before the first frame update
    void Start()
    {
        Teleport();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Teleport()
    {
        index = Random.Range(0, bossPositions.Length);
        currentPosition = bossPositions[index];
        transform.position = currentPosition;
        teleportParticles.Play();
    }

    
}
