using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_Player_Health : MonoBehaviour
{
	[SerializeField] float health = 15.0f;

    public void Hit(float fDamage)
	{
		health -= fDamage;
		if (health <= 0)
		{
			gameObject.SetActive(false);
		}
	}

	public void SetHealth(float newHealth)
    {
		health = newHealth;
	}
}
