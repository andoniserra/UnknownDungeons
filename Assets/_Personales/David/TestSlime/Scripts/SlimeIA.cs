using UnityEngine;
using System.Collections;

/// <summary>
/// El Slime decide cada 3 segundos si moverse o esperar.
/// Cuando se mueve, se mueve durante 1 segundo en una dirección aleatoria
/// y luego espera al siguiente ciclo.
/// </summary>
public class SlimeIA : MonoBehaviour 
{
	public enum Direction { North, East, South, West };
	public enum Action { Wait, Jump };


	// Tiempo en segundos entre las acciones
	public float m_cooldown = 3f;
	private float m_defaultCooldown;

	public float m_jumpCooldown = 1f;
	private float m_defaultJumpCooldown;


	public Action m_action = Action.Wait;

	// Destino elegido para el salto
	public Direction m_jumpDirection = Direction.North;

	private Movement m_movement;

	private MobState m_mobState;


	void Start () 
	{
		m_defaultCooldown = m_cooldown;
		m_defaultJumpCooldown = m_jumpCooldown;
		m_movement = GetComponent<Movement>();
		m_mobState = GetComponent<MobState>();
	}
	

	void Update () 
	{
		if (Clock())
		{
			// Si nos han dado, no calculamos otro movimiento, 
			// esperamos siguiente turno
			if (!m_mobState.m_beenHit)
			{
				DecideAction();
			}
			else
			{
				m_action = Action.Wait;
			}

		}

		if (m_action == Action.Jump)
		{
			if (JumpClock())
			{
				m_action = Action.Wait;
			}
			else
			{
				Jump(m_jumpDirection);
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


	private bool JumpClock()
	{
		m_jumpCooldown -= Time.deltaTime;
		if (m_jumpCooldown <= 0f)
		{
			m_jumpCooldown = m_defaultJumpCooldown;
			return true;
		}
		else
		{
			return false;
		}
	}


	private void DecideAction ()
	{
		int randomAction = Random.Range(0,2);
		switch (randomAction)
		{
		case 0:
			m_action = Action.Wait;
			break;
		case 1:
			m_action = Action.Jump;
			break;
		default:
			m_action = Action.Wait;
			break;
		}

		if (m_action == Action.Jump)
		{
			m_jumpDirection = DecideDirection();
		}
	}


	private Direction DecideDirection ()
	{
		int randomDir = Random.Range(0,4);

		Direction direction;

		switch (randomDir)
		{
		case 0:
			direction = Direction.North;
			break;
		case 1:
			direction = Direction.East;
			break;
		case 2:
			direction = Direction.South;
			break;
		case 3:
			direction = Direction.West;
			break;
		default:
			direction = Direction.North;
			break;
		}

		return direction;
	}


	private void Jump ( Direction p_direction )
	{
		Vector2 axisValue = Vector2.zero;

		switch (p_direction)
		{
		case Direction.North:
			axisValue = new Vector2 (0f,1f);
			break;
		case Direction.East:
			axisValue = new Vector2 (1f,0f);
			break;
		case Direction.South:
			axisValue = new Vector2 (0f,-1f);
			break;
		case Direction.West:
			axisValue = new Vector2 (-1f,0f);
			break;
		}

		m_movement.Move(axisValue);
	}
}
