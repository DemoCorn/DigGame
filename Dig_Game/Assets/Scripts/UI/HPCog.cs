using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCog : MonoBehaviour
{
    public float rotationGoal;
    public float initialRotation;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float goalRotation = rotationGoal - initialRotation;
        float rotationAmount = currentHealth / maxHealth * goalRotation;

        rotationAmount = initialRotation + (goalRotation - rotationAmount);

        while (rotationAmount < 0.0f)
        {
            rotationAmount += 360.0f;
        }

        while (rotationAmount > 360.0f)
        {
            rotationAmount -= 360.0f;
        }
        
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, rotationAmount);
    }
}
