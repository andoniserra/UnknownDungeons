using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour 
{
	public int m_damage = 5;
	public float m_lifeSpan = 1f;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, m_lifeSpan);
	}
	
	// Update is called once per frame
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
