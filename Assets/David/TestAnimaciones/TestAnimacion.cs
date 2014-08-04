using UnityEngine;
using System.Collections;

public class TestAnimacion : MonoBehaviour 
{
	public bool m_move = false;
	public Vector3 m_speed = new Vector3(4, 4, 0);

	Animator m_animator;



	// Use this for initialization
	void Awake () 
	{
		m_animator = GetComponentInChildren<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector2 input = new Vector2(inputX, inputY).normalized;

		if (inputX > 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 1);
		}
		if (inputX < 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 3);
		}
		if (inputY > 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 0);
		}
		if (inputY < 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 2);
		}
		if (inputX == 0 && inputY == 0)
		{
			m_animator.SetBool("andando", false);
		}
		if (Input.GetButtonDown("Fire1"))
		{
			m_animator.SetTrigger("ataque");
		}
		if (Input.GetButtonDown("Fire2"))
		{
			m_animator.SetTrigger("herido");
		}

		if (m_move)
		{
			Vector3 movement = new Vector3(
				input.x * m_speed.x * Time.deltaTime,
				input.y * m_speed.y * Time.deltaTime,
				0f);
			transform.Translate(movement);
		}

	}
}
