using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {

	public Vector2 m_direction = Vector2.up;




	// Speed IN PIXELS PER SECOND!
	public Vector2 m_speed = new Vector2(16, 16);
	
	// Pools to store uncommited movement
	private float m_movePoolX = 0f;
	private float m_movePoolY = 0f;
	
	// Movements to commit
	private Vector3 m_calculatedMovement;
	private Vector3 m_correctionMovement;
	
	//private CharacterController2D m_controller;
	
	
	void Awake () 
	{
		//m_controller = this.transform.GetComponent<CharacterController2D>();
	}

	void Start ()
	{

	}
	
	void Update()
	{
		Move(m_direction);
		CorrectPosition();
	}
	
	/// <summary>
	/// This method might seem redundant, but collision may alter sprite movement,
	/// so we need to correct the ending positions of the sprites.
	/// </summary>
	void CorrectPosition ()
	{
		// Correcting X position in pixels
		//float currentX = transform.position.x;
		//int correctedPixelX = Mathf.RoundToInt (currentX * 8);
		//float correctedX = correctedPixelX / 8f;
		
		// Correcting Y position in pixels
		float currentY = transform.position.y;
		int correctedPixelY = Mathf.RoundToInt (currentY * 8);
		float correctedY = correctedPixelY / 8f;
		
		// Applying correction movement
		/*m_correctionMovement = new Vector3(
			correctedX - transform.position.x,
			correctedY - transform.position.y,
			0);*/
		m_correctionMovement = new Vector3(
			0,
			correctedY - transform.position.y,
			0);
		transform.Translate(m_correctionMovement);
	}
	
	/// <summary>
	/// Moves the sprite by pixels.
	/// We can not just move and correct the position, because we will be losing
	/// part of the movement, and with certain speed values the sprite won't move at all.
	/// So we store partial movements not committed in a pool, so we can acummulate those
	/// with the next ones.
	/// </summary>
	/// <param name="p_movement">P_movement. Vector2 specifying the direction to move</param>
	public void Move ( Vector2 p_movement )
	{
		// Calculate movement in PIXELS
		//int pixelMovementX = calcMoveAmountX(p_movement.x * m_speed.x);
		int pixelMovementY = calcMoveAmountY(p_movement.y * m_speed.y);
		
		// Translate the movement into Unity UNITS
		//float unitMovementX = pixelMovementX / 8f;
		float unitMovementY = pixelMovementY / 8f;
		
		/*m_calculatedMovement = new Vector3(
			unitMovementX,
			unitMovementY,
			0);*/
		m_calculatedMovement = new Vector3(
			0,
			unitMovementY,
			0);
		//transform.Translate(movement);
		//transform.position=transform.position + m_calculatedMovement;
		
		// We actually call CharacterController2D.move to avoid jiterring
		// because it precalculates collisions
		//m_controller.move(m_calculatedMovement);
		transform.Translate(m_calculatedMovement);
		CorrectPosition();
	}
	
	
	private int calcMoveAmountX ( float p_movespeed )
	{
		//calc how much to move
		float moveAmount = Time.deltaTime * p_movespeed;
		//if there is something in the pool, add it to moveAmount and then empty the pool
		if(m_movePoolX != 0)
		{
			moveAmount += m_movePoolX;
			m_movePoolX = 0;
		}
		//round up the number to nearest int, save in 'moveAmountInt'
		int moveAmountInt = Mathf.FloorToInt(moveAmount+0.5f);
		
		//add whatever isn't casted into the int to the movepool (including negative numbers)
		m_movePoolX += (moveAmount-moveAmountInt);
		
		return moveAmountInt;
	}
	
	
	private int calcMoveAmountY ( float p_movespeed )
	{
		//calc how much to move
		float moveAmount = Time.deltaTime * p_movespeed;
		//if there is something in the pool, add it to moveAmount and then empty the pool
		if(m_movePoolY != 0)
		{
			moveAmount += m_movePoolY;
			m_movePoolY = 0;
		}
		//round up the number to nearest int, save in 'moveAmountInt'
		int moveAmountInt = Mathf.FloorToInt(moveAmount+0.5f);
		
		//add whatever isn't casted into the int to the movepool (including negative numbers)
		m_movePoolY += (moveAmount-moveAmountInt);
		
		return moveAmountInt;
	}
}
