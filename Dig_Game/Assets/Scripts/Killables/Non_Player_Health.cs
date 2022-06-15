using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Non_Player_Health : MonoBehaviour
{
	[SerializeField] ParticleSystem particle;
	[SerializeField] float health = 15.0f;
	[SerializeField] float maxHealth;
	[SerializeField] float immunityTime = 0.3f;
	SpriteRenderer sprite;
	Collider2D colliderObject;
	[HideInInspector] public bool mImmune = false;

	[SerializeField] private GameObject popup;

	private void Start()
    {
		particle = GetComponentInChildren<ParticleSystem>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		colliderObject = GetComponent<Collider2D>();
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
			ShowDamage( fDamage.ToString());
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

	void ShowDamage(string text)
    {
		if(popup)
        {
			GameObject prefab = Instantiate(popup, transform.position, Quaternion.identity);
			prefab.GetComponentInChildren<TextMeshPro>().text = text;
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

	public float GetHealth()
    {
		return health;
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
			colliderObject.enabled = false;
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
