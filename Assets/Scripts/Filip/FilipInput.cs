using UnityEngine;
using System.Collections;

public class FilipInput : MonoBehaviour 
{		
	private FilipState m_filipState;
	private bool pausa = false;

	void Awake () 
	{
		m_filipState = GetComponent<FilipState>();	
	}
	

	void Update () 
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector2 inputAxis = new Vector2(inputX, inputY).normalized;
		
		m_filipState.Walk(inputAxis);

		if (Input.GetButtonDown("Fire"))
		{
			m_filipState.Attack();
		}

		// Mientras este el boton pulsado, defendemos
		if (Input.GetButtonDown("Defend"))
		{
			m_filipState.Defend();
		}

		// Cuando se suelta el boton, dejamos de defender
		if (Input.GetButtonUp("Defend"))
		{
			m_filipState.StopDefend();
		}
		
		// Cambiar de arma
		if (Input.GetButtonDown("Select"))
		{
			m_filipState.ChangeWeapon();
		}		

	}
}

