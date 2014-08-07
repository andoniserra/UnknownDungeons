using UnityEngine;
using System.Collections;

public class TestAnimacion : MonoBehaviour 
{
	public bool m_move = false;
	public Vector3 m_speed = new Vector3(4, 4, 0);

	private const int m_numberOfWeapons = 3;

	[Range(0, m_numberOfWeapons -1)]
	public int m_equippedWeapon = 0;



	private Animator m_animator;
	private Movement m_movement;

	private FilipState m_filipState;



	// Use this for initialization
	void Awake () 
	{
		m_animator = GetComponentInChildren<Animator>();
		if (m_move)
		{
			m_movement = GetComponent<Movement>();
		}
		m_filipState = GetComponent<FilipState>();

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
			//m_filipState.FaceDirection(1);
		}
		if (inputX < 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 3);
			//m_filipState.FaceDirection(3);
		}
		if (inputY > 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 0);
			//m_filipState.FaceDirection(0);
		}
		if (inputY < 0)
		{
			m_animator.SetBool("andando", true);
			m_animator.SetInteger("direccion", 2);
			//m_filipState.FaceDirection(2);
		}
		if (inputX == 0 && inputY == 0)
		{
			m_animator.SetBool("andando", false);
		}
		if (Input.GetButtonDown("Fire1"))
		{
			m_filipState.Attack();
		}
		// Cuando se suelta el boton, dejamos de defender
		if (Input.GetButtonUp("Fire2"))
		{
			m_animator.SetBool("defendiendo", false);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			m_animator.SetBool("defendiendo", true);
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

	void OnTriggerEnter2D (Collider2D p_collider)
	{
		//print ("triggered!");
		if (p_collider.gameObject.tag == "Door")
		{
			print ("It's a door!");
			Door door = p_collider.gameObject.GetComponent<Door>();
			Application.LoadLevel(door.m_targetScene);
		}
	}
}
