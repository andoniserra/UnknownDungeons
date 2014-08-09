using UnityEngine;
using System.Collections;

public class MobState : MonoBehaviour 
{
	/// <summary>
	/// HP del mob.
	/// </summary>
	public int m_hp = 10;

	/// <summary>
	/// Estado que representa cuando el mob ha sido alcanzado por un arma.
	/// </summary>
	public bool m_beenHit = false;
	public float m_beenHitCooldown = 1f;
	private float m_beenHitDefaultCooldown;

	/// <summary>
	/// Si el mob esta muerto.
	/// </summary>
	public bool m_dead = false;

	public Transform m_coin;

	private BoxCollider2D m_collider;

	// Referencia al animator para desencadenar las animaciones
	private Animator m_animator;

	void Awake () 
	{
		m_beenHitDefaultCooldown = m_beenHitCooldown;
		m_collider = GetComponent<BoxCollider2D>();
		m_animator = GetComponentInChildren<Animator>();
	}
	

	void Update () 
	{
		if (m_dead)
		{

			Destroy(gameObject, 1);
			if (m_collider.enabled)
			{
				m_collider.enabled = false;
			}
		}

		if (m_beenHit)
		{
			// Cuando el cooldown se acaba, desactivamos el estado Alcanzado
			// El mob ya puede volver a ser golpeado otra vez
			if (BeenHitClock())
			{
				m_beenHit = false;
			}
		}
	}


	/// <summary>
	/// Decrementa el cooldown del estado beenHit.
	/// reinicia el cooldown cuando se acaba.
	/// </summary>
	/// <returns><c>true</c>, si el cooldown se ha acabado, <c>false</c> si no.</returns>
	private bool BeenHitClock ()
	{
		m_beenHitCooldown -= Time.deltaTime;
		if (m_beenHitCooldown <= 0f)
		{
			m_beenHitCooldown = m_beenHitDefaultCooldown;
			return true;
		}
		else
		{
			return false;
		}
	}

	/*
	void OnTriggerEnter2D (Collider2D p_collider)
	{
		//print ("Mob Triggered!");
		if (p_collider.gameObject.tag == "FilipWeapon")
		{
			//print ("Filip me ha dado!");
			if (!m_beenHit)
			{
				m_hp -= 1;
				m_beenHit = true;
				if (m_hp <= 0)
				{
					m_dead = true;
				}
			}
		}
	}
	*/

	public void ApplyDamage ( int p_damage )
	{
		//print ( p_damage );
		//print ("Filip me ha dado!");
		if (!m_beenHit)
		{
			m_hp -= p_damage;
			m_beenHit = true;
			// Reproducir sonido mob herido
			SoundHelper.PlayMobHit();
			m_animator.SetTrigger("herido");
			if (m_hp <= 0)
			{
				m_dead = true;
				// Descontamos el enemigo de la lista de enemigos de la escena
				GameState.EnemyDead();
				StartCoroutine("WaitAndCoin",1f);
			}
		}
	}


	IEnumerator WaitAndCoin ( float p_time )
	{
		yield return new WaitForSeconds(p_time);
		Instantiate(m_coin,transform.position,transform.rotation);
	}
}
