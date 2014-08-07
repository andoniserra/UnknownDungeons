using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{

	public int m_damage = 4;
	public float m_projectileLifeSpan = 3f;


	void Start () 
	{
		Destroy(gameObject, m_projectileLifeSpan);
	}
	

	void Update () 
	{
	
	}


	void OnTriggerEnter2D (Collider2D p_collider)
	{
		//print ("Collision!");
		if (p_collider.gameObject.tag == "Enemy")
		{
			p_collider.gameObject.SendMessage( "ApplyDamage", m_damage );
		}
	}
}
