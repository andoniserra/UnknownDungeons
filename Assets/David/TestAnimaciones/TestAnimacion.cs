using UnityEngine;
using System.Collections;

public class TestAnimacion : MonoBehaviour 
{
	Animator m_animator;
	// Use this for initialization
	void Awake () 
	{
		m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

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

	}
}
