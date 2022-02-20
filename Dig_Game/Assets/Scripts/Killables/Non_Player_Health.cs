using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_Player_Health : MonoBehaviour
{
	[SerializeField] float health = 15.0f;
	[SerializeField] float immunityTime = 0.0f;
	bool mImmune = false;

    public void Hit(float fDamage)
	{
		if (!mImmune)
		{
			health -= fDamage;
			if (health <= 0)
			{
				gameObject.SetActive(false);
			}
			mImmune = true;
			Invoke("StopImmunity", immunityTime);
		}
	}

	private void StopImmunity()
    {
		mImmune = false;
    }

	public void SetHealth(float newHealth)
    {
		health = newHealth;
	}
}
