using UnityEngine;
using System.Collections;

public class TestAnimacion : MonoBehaviour 
{
	public bool m_move = false;
	public Vector3 m_speed = new Vector3(4, 4, 0);

	private const int m_numberOfWeapons = 4;

	[Range(0, m_numberOfWeapons -1)]
	public int m_equippedWeapon = 0;

	Animator m_animator;
	Movement m_movement;



	// Use this for initialization
	void Awake () 
	{
		m_animator = GetComponentInChildren<Animator>();
		if (m_move)
		{
			m_movement = GetComponent<Movement>();
		}

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
			// Si no estamos con el escudo, lanzamos un ataque
			if (m_equippedWeapon != 3)
			{
				m_animator.SetTrigger("ataque");
			}
			// Si es el escudo, defendemos mientras se pulse el boton
			else
			{
				m_animator.SetBool("defendiendo", true);
			}
		}
		// Cuando se suelta el boton, dejamos de defender
		if (Input.GetButtonUp("Fire1"))
		{
			m_animator.SetBool("defendiendo", false);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			m_animator.SetTrigger("herido");
		}

		// Cambiar de arma
		if (Input.GetButtonDown("Jump"))
		{
			CambiarArma();
			m_animator.SetInteger("arma", m_equippedWeapon);
		}

		if (m_move)
		{
			/*Vector3 movement = new Vector3(
				input.x * m_speed.x * Time.deltaTime,
				input.y * m_speed.y * Time.deltaTime,
				0f);
				*/
			//transform.Translate(movement);
			Vector3 movement = new Vector3(
				input.x,
				input.y,
				0f);
			m_movement.Move(movement);
		}

	}

	private void CambiarArma ()
	{
		m_equippedWeapon += 1;
		if (m_equippedWeapon >= m_numberOfWeapons)
		{
			m_equippedWeapon = 0;
		}
	}
}
