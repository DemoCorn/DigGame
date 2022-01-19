using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
