using UnityEngine;
using System.Collections;

public class FilipState : MonoBehaviour 
{
	public static FilipState myFilip = null;

	// Si Filip se debe mover o no (debug de animaciones)
	public bool m_move = true;

	// Vida de Filip
	public int m_fullHP = 5; 	//Numero de corazones
	public int m_hp = 3;		//Vida

	// Dinero de Filip
	public int m_coins = 0;

	// Numero de armas
	private const int m_numberOfWeapons = 3;
	
	// Arma equipada por defecto (0-espada, 1-arco, 2-magia)
	[Range(0, m_numberOfWeapons -1)]
	public int m_equippedWeapon = 0;
	
	// Prefabs de las armas
	public Transform m_arrow;
	public float m_arrowStart = 10f;
	public int m_arrowLvl = 1;
	public Transform m_swordSwing;
	public float m_swordRange = 18f;
	public int m_swordLvl = 1;
	public Transform m_magicBall;
	public float m_magicBallRange = 24f;
	public int m_magicLvl = 1;
	public int m_shieldLvl = 1;

	/******************************
	 *    Estados de Filip
	 ******************************/
	public int m_facingDirection = 2;

	public bool m_attacking = false;
	public float m_attackCooldown = 1f;
	private float m_attackDefaultCooldown;

	public bool m_defending = false;

	public bool m_beenHit = false;
	public float m_beenHitCooldown = 1f;
	private float m_beenHitDefaultCooldown;

	public bool m_canMove = true;
	public float m_canMoveCooldown = 0.1f;
	private float m_canMoveDefaultCooldown;

	public bool m_dead = false;
	/*****************************/

	// Referencia al animator para desencadenar las animaciones
	private Animator m_animator;

	// Referencia al script de movimiento pixelado
	private Movement m_movement;


	void Awake () 
	{
		// Si no existe
		if (myFilip == null)
		{
			//print ("dont destroy" + gameObject.GetInstanceID());
			// Marcamos el objeto para no ser destruido al cambiar de escena
			DontDestroyOnLoad(transform.gameObject);
			// Asignamos esta instancia a la instancia unica Singleton
			myFilip = this;
		}
		// Si ya existia
		else if (myFilip != this)
		{
			//print ("destroy!" + gameObject.GetInstanceID());
			// Destruimos este objeto
			Destroy(gameObject);
		}

		// Almacenamos los CDs por si se han ajustado en el editor
		m_attackDefaultCooldown = m_attackCooldown;
		m_beenHitDefaultCooldown = m_beenHitCooldown;
		m_canMoveDefaultCooldown = m_canMoveCooldown;

		m_animator = GetComponentInChildren<Animator>();
		m_movement = GetComponent<Movement>();
	}
	

	void Update () 
	{
		if (m_dead)
		{
			Destroy(gameObject, 1);
		}
		
		if (m_attacking)
		{
			// Cuando el cooldown se acaba, desactivamos el estado Atacando
			// Filip ya puede volver a atacar
			if (AttackClock())
			{
				m_attacking = false;
			}
		}

		if (m_beenHit)
		{
			// Cuando el cooldown se acaba, desactivamos el estado Alcanzado
			// Filip puede volver a ser dañado
			if (BeenHitClock())
			{
				m_beenHit = false;
			}
		}

		if (!m_canMove)
		{
			// Cuando el cooldown se acaba, activamos canMove
			// Filip ya se puede mover
			if (CanMoveClock())
			{
				m_canMove = true;
			}
		}
	}


	void OnCollisionStay2D (Collision2D p_collision)
	{
		//print ("Collision!");
		if (p_collision.gameObject.tag == "Enemy")
		{
			ApplyDamage(1);
		}
	}
	
	
	void OnTriggerEnter2D (Collider2D p_collider)
	{
		//print ("triggered!");
		if (p_collider.gameObject.tag == "Door")
		{
			//print ("It's a door!");
			Door door = p_collider.gameObject.GetComponent<Door>();

			GameState.LoadScene(door.m_targetScene, door.m_doorDirection);

			//Application.LoadLevel(door.m_targetScene);
		}

		if (p_collider.gameObject.tag == "Pickable")
		{
			myFilip.m_coins += 1;
			Destroy(p_collider.gameObject);
			SoundHelper.PlayPickCoin();
		}
	}


	public void Walk ( Vector2 p_inputAxis )
	{
		if (p_inputAxis.x > 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 1);
			FaceDirection(GLOBALS.Direction.East);
		}
		if (p_inputAxis.x < 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 3);
			FaceDirection(GLOBALS.Direction.West);
		}
		if (p_inputAxis.y > 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 0);
			FaceDirection(GLOBALS.Direction.North);
		}
		if (p_inputAxis.y < 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 2);
			FaceDirection(GLOBALS.Direction.South);
		}
		if (p_inputAxis.x == 0 && p_inputAxis.y == 0)
		{
			m_animator.SetBool("andando", false);
		}

		if (m_move && m_canMove && !m_defending)
		{
			Vector3 movement = new Vector3(
				p_inputAxis.x,
				p_inputAxis.y,
				0f);
			m_movement.Move(movement);
		}
	}


	public void Attack ()
	{
		if (!m_attacking && m_canMove && !m_defending)
		{
			m_attacking = true;
			m_canMove = false;
			switch (m_equippedWeapon)
			{

			case 0: // Espada
				int swordRotation = GetProjectileRotation(m_facingDirection);
				Vector3 swordPosition = GetProjectilePosition(
					m_facingDirection,
					m_equippedWeapon,
					m_swordRange);
				Transform sword = Instantiate(
					m_swordSwing,
					swordPosition,
					this.transform.rotation) as Transform ;
				sword.transform.Rotate(Vector3.forward * swordRotation);
				// Reproducir el sonido de la espada
				SoundHelper.PlaySwordSwing();
				break;

			case 1: // Arco
				//Vector3 direction = GetDirectionVector(m_facingDirection);
				int arrowRotation = GetProjectileRotation(m_facingDirection);
				Vector3 arrowPosition = GetProjectilePosition(
					m_facingDirection,
					m_equippedWeapon,
					m_arrowStart);
				Transform arrow = Instantiate(
					m_arrow,
					arrowPosition,
					this.transform.rotation) as Transform ;
				arrow.transform.Rotate(Vector3.forward * arrowRotation);
				//ProjectileMovement movement = projectile.GetComponent<ProjectileMovement>();
				//movement.m_direction = direction;

				// Reproducir sonido flecha
				SoundHelper.PlayArrowShot();
				break;

			case 2: // Magia
				int magicRotation = GetProjectileRotation(m_facingDirection);
				Vector3 magicPosition = GetProjectilePosition(
					m_facingDirection,
					m_equippedWeapon,
					m_magicBallRange);
				Transform magic = Instantiate(
					m_magicBall,
					magicPosition,
					this.transform.rotation) as Transform ;
				magic.transform.Rotate(Vector3.forward * magicRotation);
				// Reproducir sonido bola magica
				SoundHelper.PlayMagicBall();
				break;

			default:
				break;
			}

			m_animator.SetTrigger("ataque");
		}
	}


	public void Defend ()
	{
		if (!m_defending && m_canMove)
		{
			m_defending = true;
			m_animator.SetBool("defendiendo", true);
		}
	}
	
	
	public void StopDefend ()
	{
		if (m_defending)
		{
			m_defending = false;
			m_animator.SetBool("defendiendo", false);
		}
	}
	
	
	public void ChangeWeapon ()
	{
		m_equippedWeapon += 1;
		if (m_equippedWeapon >= m_numberOfWeapons)
		{
			m_equippedWeapon = 0;
		}
		m_animator.SetInteger("arma", m_equippedWeapon);
	}


	public void FaceDirection(GLOBALS.Direction p_direction)
	{
		switch (p_direction)
		{
		case GLOBALS.Direction.North:
			m_facingDirection = 0;
			break;
		case GLOBALS.Direction.East:
			m_facingDirection = 1;
			break;
		case GLOBALS.Direction.South:
			m_facingDirection = 2;
			break;
		case GLOBALS.Direction.West:
			m_facingDirection = 3;
			break;
		default:
			m_facingDirection = 0;
			break;
		}
	}


	private int GetProjectileRotation ( int p_direction )
	{
		int rotation = 0;
		switch (p_direction)
		{
		case 0:
			rotation = 0;
			break;
		case 1:
			rotation = -90;
			break;
		case 2:
			rotation = 180;
			break;
		case 3:
			rotation = 90;
			break;
		default:
			rotation = 0;
			break;
		}
		
		return rotation;
	}


	private Vector3 GetProjectilePosition ( int p_direction, int p_weapon, float p_range )
	{
		Vector2 position = new Vector2(0f,0f);
		switch (p_direction)
		{
		case 0:
			position.y = 1 * p_range;
			position.x = 0f * GLOBALS.UNITS_TO_PIXELS;
			position.y *= GLOBALS.UNITS_TO_PIXELS;
			break;
		case 1:
			position.x = 1 * p_range;
			position.x *= GLOBALS.UNITS_TO_PIXELS;
			position.y = -6f * GLOBALS.UNITS_TO_PIXELS;
			break;
		case 2:
			position.y = -1 * p_range;
			position.x = 0f * GLOBALS.UNITS_TO_PIXELS;
			position.y *= GLOBALS.UNITS_TO_PIXELS;
			break;
		case 3:
			position.x = -1 * p_range;
			position.x *= GLOBALS.UNITS_TO_PIXELS;
			position.y = -6f * GLOBALS.UNITS_TO_PIXELS;
			break;
		default:
			break;
		}
		
		return new Vector3(
			this.transform.position.x + position.x,
			this.transform.position.y + position.y,
			0f);
	}
	

	/*
	private Vector3 GetDirectionVector ( int p_direction )
	{
		int vectorX = 0;
		int vectorY = 0;
		switch (p_direction)
		{
		case 0:
			vectorX = 0;
			vectorY = 1;
			break;
		case 1:
			vectorX = 1;
			vectorY = 0;
			break;
		case 2:
			vectorX = 0;
			vectorY = -1;
			break;
		case 3:
			vectorX = -1;
			vectorY = 0;
			break;
		default:
			vectorX = 0;
			vectorY = 1;
			break;
		}
		
		return new Vector3(vectorX, vectorY, 0f);
	}
	*/


	/// <summary>
	/// Decrementa el cooldown del estado !canMove.
	/// reinicia el cooldown cuando se acaba.
	/// </summary>
	/// <returns><c>true</c>, si el cooldown se ha acabado, <c>false</c> si no.</returns>
	private bool CanMoveClock ()
	{
		m_canMoveCooldown -= Time.deltaTime;
		if (m_canMoveCooldown <= 0f)
		{
			m_canMoveCooldown = m_canMoveDefaultCooldown;
			return true;
		}
		else
		{
			return false;
		}
	}


	/// <summary>
	/// Decrementa el cooldown del estado Attacking.
	/// reinicia el cooldown cuando se acaba.
	/// </summary>
	/// <returns><c>true</c>, si el cooldown se ha acabado, <c>false</c> si no.</returns>
	private bool AttackClock ()
	{
		m_attackCooldown -= Time.deltaTime;
		if (m_attackCooldown <= 0f)
		{
			m_attackCooldown = m_attackDefaultCooldown;
			return true;
		}
		else
		{
			return false;
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


	public void ApplyDamage ( int p_damage )
	{
		if (!m_beenHit && !m_defending)
		{
			//print ("Damage!");
			m_hp -= p_damage;
			m_beenHit = true;
			m_canMove = false;
			m_animator.SetTrigger("herido");
			
			// reproducir sonido herido
			SoundHelper.PlayPlayerHit();
			if (m_hp <= 0)
			{
				m_dead = true;
				GameState.PlayerDead();
			}
		}
	}
}
