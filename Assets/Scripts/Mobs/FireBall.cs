using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public int m_damage = 4;
	public float m_projectileLifeSpan = 3f;
	public Transform m_flame;
	
	
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
		if (p_collider.gameObject.tag == "Filip")
		{
			p_collider.gameObject.SendMessage( "ApplyDamage", m_damage );
			LeaveFlame();
			Destroy(this.gameObject);
		}
	}


	void OnCollisionEnter2D (Collision2D p_collision)
	{
		print ("Collision!");
		if (p_collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
		{
			LeaveFlame();
			print ("Pared!");
			Destroy(this.gameObject);
		}
	}


	private void LeaveFlame ()
	{
		Instantiate(m_flame, transform.position, transform.rotation);
	}
}
