using UnityEngine;
using System.Collections;

public enum DragonAction
{
	Wait,
	Fly,
	SpitFire,
	Flamethrower,
	Fall,
	Grounded,
	Rise
}


public class DragonAI : MonoBehaviour 
{
	public int m_flamethrowerShoots = 4;
	public float m_travelLength = 48f;
	public float m_maxLeftPosition = 0;
	public float m_maxRightPosition = 0;
	public bool m_maxLeftReached = false;
	public bool m_maxRightReached = false;

	// Tiempo en segundos entre las acciones
	public float m_cooldown = 0.5f;
	private float m_defaultCooldown;
	
	public float m_flyCooldown = 0.5f;
	private float m_defaultFlyCooldown;

	public float m_groundedCooldown = 3f;
	private float m_defaultGroundedCooldown;
	
	
	public DragonAction m_action = DragonAction.Wait;
	
	// Destino elegido para moverse
	public GLOBALS.Direction m_flyDirection = GLOBALS.Direction.West;
	
	private Movement m_movement;
	
	private MobState m_mobState;

	public Transform m_fireBall;

	
	void Start () 
	{
		m_maxLeftPosition = transform.position.x - (m_travelLength * GLOBALS.UNITS_TO_PIXELS);
		//print (m_maxLeftPosition);
		m_maxRightPosition = transform.position.x + (m_travelLength * GLOBALS.UNITS_TO_PIXELS);
		//print (m_maxRightPosition);
		m_defaultCooldown = m_cooldown;
		m_defaultFlyCooldown = m_flyCooldown;
		m_defaultGroundedCooldown = m_groundedCooldown;
		m_movement = GetComponent<Movement>();
		m_mobState = GetComponent<MobState>();
	}
	
	
	void Update () 
	{
		if (Clock() && m_action != DragonAction.Grounded)
		{
			// Si nos han dado, no calculamos otro movimiento, 
			// esperamos siguiente turno
			if (!m_mobState.m_beenHit && !m_mobState.m_dead &&
			    m_action != DragonAction.Fall && m_action != DragonAction.Rise)
			{
				DecideAction();
			}
			else if (m_action != DragonAction.Fall && m_action != DragonAction.Rise)
			{
				m_action = DragonAction.Wait;
			}
			
		}
		
		if (m_action == DragonAction.Fly || m_action == DragonAction.SpitFire)
		{
			if (FlyClock())
			{
				m_action = DragonAction.Wait;
			}
			else
			{
				Fly(m_flyDirection);
			}
		}

		if (m_action == DragonAction.Fall)
		{
			if (FlyClock())
			{
				m_action = DragonAction.Grounded;
			}
			else
			{
				Fly(GLOBALS.Direction.South);
			}
		}

		if (m_action == DragonAction.Rise)
		{
			if (FlyClock())
			{
				m_action = DragonAction.Wait;
			}
			else
			{
				Fly(GLOBALS.Direction.North);
			}
		}

		if (m_action == DragonAction.Grounded)
		{
			if (GroundedClock())
			{
				Rise ();
			}
		}
	}
	
	
	private bool Clock()
	{
		m_cooldown -= Time.deltaTime;
		if (m_cooldown <= 0f)
		{
			m_cooldown = m_defaultCooldown;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	
	private bool FlyClock()
	{
		m_flyCooldown -= Time.deltaTime;
		if (m_flyCooldown <= 0f)
		{
			m_flyCooldown = m_defaultFlyCooldown;
			return true;
		}
		else
		{
			return false;
		}
	}


	private bool GroundedClock()
	{
		m_groundedCooldown -= Time.deltaTime;
		if (m_groundedCooldown <= 0f)
		{
			m_groundedCooldown = m_defaultGroundedCooldown;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	
	private void DecideAction ()
	{
		int randomAction = Random.Range(0,3);
		switch (randomAction)
		{
		/*case 0:
			m_action = DragonAction.Wait;
			break;
			*/
		case 0:
			m_action = DragonAction.Fly;
			break;
		case 1:
			m_action = DragonAction.SpitFire;
			break;
		case 2:
			m_action = DragonAction.Flamethrower;
			break;
		default:
			m_action = DragonAction.Fly;
			break;
		}
		
		if (m_action == DragonAction.Fly)
		{
			m_flyDirection = DecideDirection();
		}
		if (m_action == DragonAction.SpitFire)
		{
			SpitFire();
		}
		if (m_action == DragonAction.Flamethrower)
		{
			ShootFlamethrower();
		}
	}
	
	
	private GLOBALS.Direction DecideDirection ()
	{
		GLOBALS.Direction direction;

		if (m_maxLeftReached)
		{
			direction = GLOBALS.Direction.East;
			m_maxLeftReached = false;
		}
		else if (m_maxRightReached)
		{
			direction = GLOBALS.Direction.West;
			m_maxRightReached = false;
		}
		else
		{
			int randomDir = Random.Range(0,2);

			switch (randomDir)
			{
			case 0:
				direction = GLOBALS.Direction.West;
				break;
			case 1:
				direction = GLOBALS.Direction.East;
				break;
				
			default:
				direction = GLOBALS.Direction.West;
				break;
			}
		}
		return direction;
	}
	
	
	private void Fly ( GLOBALS.Direction p_direction )
	{
		if (!m_maxLeftReached && !m_maxRightReached)
		{
			Vector2 axisValue = Vector2.zero;
			
			switch (p_direction)
			{
			case GLOBALS.Direction.North:
				axisValue = new Vector2 (0f,1f);
				break;
			case GLOBALS.Direction.East:
				axisValue = new Vector2 (1f,0f);
				break;
			case GLOBALS.Direction.South:
				axisValue = new Vector2 (0f,-1f);
				break;
			case GLOBALS.Direction.West:
				axisValue = new Vector2 (-1f,0f);
				break;
			}
			
			m_movement.Move(axisValue);
		}

		if (transform.position.x <= m_maxLeftPosition)
		{
			m_maxLeftReached = true;
		}
		if (transform.position.x >= m_maxRightPosition)
		{
			m_maxRightReached = true;
		}
	}


	private void SpitFire ()
	{
		Vector3 pos = new Vector3(
			transform.position.x,
			transform.position.y - (GLOBALS.UNITS_TO_PIXELS * 16));
		Instantiate(m_fireBall, pos, transform.rotation);
	}


	private void ShootFlamethrower ()
	{
		Vector3 pos = new Vector3(
			transform.position.x,
			transform.position.y - (GLOBALS.UNITS_TO_PIXELS * 16));
		for (int i = 1; i<= m_flamethrowerShoots; i++)
		{
			StartCoroutine("WaitAndFlame", pos);
		}
	}


	public void Fall()
	{
		m_action = DragonAction.Fall;
	}

	public void Rise()
	{
		m_action = DragonAction.Rise;
		m_mobState.Rise();
	}


	IEnumerator WaitAndFlame ( Vector3 p_position )
	{
		float randomOffsetTime = Random.Range(0f, m_defaultCooldown);
		yield return new WaitForSeconds(randomOffsetTime);
		Transform fireball = Instantiate(m_fireBall, p_position, transform.rotation) as Transform;
		float rotation = Random.Range (-45f, 45f);
		//fireball.GetComponent<FireBall>().m_leavesFlame=false;
		fireball.transform.Rotate(Vector3.forward * rotation);
	}
}
