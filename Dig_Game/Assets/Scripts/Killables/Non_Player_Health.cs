using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_Player_Health : MonoBehaviour
{
	[SerializeField] ParticleSystem particle;
	[SerializeField] float health = 15.0f;
	[SerializeField] float immunityTime = 0.0f;
	[SerializeField] float spawnNewEnemyChance = 10.0f;
	SpriteRenderer sprite;
	BoxCollider2D boxCollider;
	[HideInInspector] public bool mImmune = false;

    private void Awake()
    {
		particle = GetComponentInChildren<ParticleSystem>();
		sprite = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
    }

	public void Hit(float fDamage)
	{
		if (!mImmune)
		{
			health -= fDamage;
			if (health <= 0)
			{
				StartCoroutine(Destroy());
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

	private IEnumerator Destroy()
    {
		if (particle)
		{
			particle.Play();
			sprite.enabled = false;
			boxCollider.enabled = false;
			yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
		}

		Destroy(gameObject);
    }

}
