using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
	// Movement Component to call in order to move the sprite
	private Movement m_moveScript;


	void Awake() 
	{
		m_moveScript = this.transform.GetComponent<Movement>();
	}
	

	void Update () 
	{
		// Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// Normalized to prevent excessive speed in diagonal movement
		Vector2 input = new Vector2(inputX, inputY).normalized;

		// Call the move with the axis values
		if (!(input.Equals(Vector2.zero)))
		{
			m_moveScript.Move(input);
		}
	}
}
