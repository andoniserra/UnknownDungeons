using UnityEngine;
using System.Collections;

/// <summary>
/// Veeeery basic Zombie AI, just walks towards the player
/// </summary>
public class ZombieAI : MonoBehaviour 
{
	// The target (player) to pursue
	public Transform m_victim;

	// (debug) Target's stored position
	public Vector3 m_target;

	// (debug) Zombie's position
	public Vector3 m_position;

	// (debug) Movement vector to target's position
	public Vector3 m_movement;
	
	// Movement Component to call in order to move the sprite
	private Movement m_moveScript;

	
	void Awake() 
	{
		m_moveScript = this.GetComponent<Movement>();
	}
	

	void Update () 
	{
		if (m_victim)
		{
			// Get player's position and steer towards it
			m_target = m_victim.transform.position;
			m_position = this.transform.position;

			m_movement = m_target - m_position;

			// Normalized to get a Vector of magnitude 1 similar to player's input values
			Vector2 movement2D = new Vector2(m_movement.x,m_movement.y).normalized;

			// Call the move
			if (!(movement2D.Equals(Vector2.zero)))
			{
				m_moveScript.Move(movement2D);
			}
		}	
	}
}
