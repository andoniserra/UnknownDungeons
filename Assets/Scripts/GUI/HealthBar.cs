using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour 
{
	//public Transform m_filip;

	//private FilipState m_filipState;
	private int m_health = 0;
	private Heart m_heart1 = null;
	private Heart m_heart2 = null;
	private Heart m_heart3 = null;
	private Heart m_heart4 = null;
	private Heart m_heart5 = null;
	

	void Start () 
	{
		//m_filipState = m_filip.GetComponent<FilipState>();
		m_heart1 = transform.GetChild(0).GetComponent<Heart>();
		m_heart2 = transform.GetChild(1).GetComponent<Heart>();
		m_heart3 = transform.GetChild(2).GetComponent<Heart>();
		m_heart4 = transform.GetChild(3).GetComponent<Heart>();
		m_heart5 = transform.GetChild(4).GetComponent<Heart>();
	}
	

	void Update () 
	{
		m_health = FilipState.myFilip.m_hp;
		
		if (m_health == 0)
		{
			m_heart1.Empty();
			m_heart2.Empty();
			m_heart3.Empty();
			m_heart4.Empty();
			m_heart5.Empty();
		}

		if (m_health == 1)
		{
			m_heart1.Full();
			m_heart2.Empty();
			m_heart3.Empty();
			m_heart4.Empty();
			m_heart5.Empty();
		}

		if (m_health == 2)
		{
			m_heart1.Full();
			m_heart2.Full();
			m_heart3.Empty();
			m_heart4.Empty();
			m_heart5.Empty();
		}

		if (m_health == 3)
		{
			m_heart1.Full();
			m_heart2.Full();
			m_heart3.Full();
			m_heart4.Empty();
			m_heart5.Empty();
		}

		if (m_health == 4)
		{
			m_heart1.Full();
			m_heart2.Full();
			m_heart3.Full();
			m_heart4.Full();
			m_heart5.Empty();
		}

		if (m_health == 5)
		{
			m_heart1.Full();
			m_heart2.Full();
			m_heart3.Full();
			m_heart4.Full();
			m_heart5.Full();
		}
	}
}
