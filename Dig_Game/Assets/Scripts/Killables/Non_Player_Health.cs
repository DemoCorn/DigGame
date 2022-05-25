using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_Player_Health : MonoBehaviour
{
	[SerializeField] ParticleSystem particle;
	[SerializeField] float health = 15.0f;
	[SerializeField] float maxHealth;
	[SerializeField] float immunityTime = 0.0f;
	SpriteRenderer sprite;
	Collider2D collider2D;
	[HideInInspector] public bool mImmune = false;

    private void Start()
    {
		particle = GetComponentInChildren<ParticleSystem>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		collider2D = GetComponent<Collider2D>();
		maxHealth = health;
    }
    private void Update()
    {
		DirtTransparency();
    }

    public void Hit(float fDamage)
	{
		if (!mImmune)
		{
			health -= fDamage;
			if (health <= 0)
			{
				DestroyObject();
			}
			else if(particle)
            {
				particle.Play();
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
	public void SetMaxHealth(float newHealth)
	{
		maxHealth = newHealth;
	}

	private void DestroyObject()
    {
		if (particle)
		{
			particle.Play();
			sprite.enabled = false;
			collider2D.enabled = false;
			Destroy(gameObject, particle.main.startLifetime.constantMax);
		}
		else
        {
			Destroy(gameObject);
        }
    }

	void DirtTransparency()
    {
		if (gameObject.tag == "BreakableBlock")
        {
			float alpha = health / maxHealth;
			sprite.color = new Color(1f, 1f, 1f, alpha);			
        }
    }

}
