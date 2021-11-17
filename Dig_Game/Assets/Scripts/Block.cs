using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float health = 15.0f;

    public void Hit(float fDamage)
	{
		health -= fDamage;
		if (health <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
