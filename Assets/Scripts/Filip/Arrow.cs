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


	void OnCollisionEnter2D (Collision2D p_collision)
	{
		print ("Collision!");
		if (p_collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
		{
			print ("Pared!");
			Destroy(this.gameObject);
		}
	}
}
