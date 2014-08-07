using UnityEngine;
using System.Collections;

public class MagicBall : MonoBehaviour 
{
	public int m_damage = 3;

	public float m_lifeSpan1 = 0.2f;
	public float m_lifeSpan2 = 0.4f;
	public float m_lifeSpan3 = 1f;

	private float timePassed = 0f;
	private Transform m_sprite1 = null;
	private Transform m_sprite2 = null;
	private Transform m_sprite3 = null;
	private int m_state = 1;

	private CircleCollider2D m_collider;


	// Use this for initialization
	void Start () 
	{
		m_sprite1 = transform.GetChild(0);
		m_sprite2 = transform.GetChild(1);
		m_sprite3 = transform.GetChild(2);

		m_collider = GetComponent<CircleCollider2D>();

		Destroy(gameObject, m_lifeSpan3);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timePassed += Time.deltaTime;

		if (timePassed >= m_lifeSpan1 && m_state == 1)
		{
			m_state = 2;
			m_sprite2.renderer.enabled = true;
			Destroy(m_sprite1.gameObject);
		}

		if (timePassed >= m_lifeSpan2 && m_state == 2)
		{
			m_state = 3;
			m_sprite3.renderer.enabled = true;
			m_collider.enabled = true;
			Destroy(m_sprite2.gameObject);
		}
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
