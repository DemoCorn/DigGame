using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject boss;
    public Vector2[] bossPositions = new Vector2[6];
    int index;
    public Vector2 currentPosition;
    public bool inRadius;

    public float teleportCooldown;
    public float teleportCooldownRequirment;


    // Start is called before the first frame update
    void Start()
    {
        Teleport();
        teleportCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRadius == true)
        {
            teleportCooldown += Time.deltaTime;
            if (teleportCooldown >= teleportCooldownRequirment)
            {
                Teleport();
                inRadius = false;
                teleportCooldown = 0;
            }
        }
    }

    void Teleport()
    {
        index = Random.Range(0, bossPositions.Length);
        currentPosition = bossPositions[index];
        transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Teleport();
            //Debug.Log("Player");
            inRadius = true;
        }
    }
}
