using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour 
{

	public int m_damage = 1;
	
	public float m_lifeSpan1 = 0.2f;
	public float m_lifeSpan2 = 1.5f;

	public bool m_eternalFlames = false;
	
	private float timePassed = 0f;
	public Sprite m_sprite1 = null;
	public Sprite m_sprite2 = null;
	private int m_state = 1;
	
	private CircleCollider2D m_collider;
	private SpriteRenderer m_renderer;
	
	

	void Awake () 
	{
		//m_sprite1 = transform.GetChild(0);
		//m_sprite2 = transform.GetChild(1);
		//m_sprite3 = transform.GetChild(2);
		
		m_collider = GetComponent<CircleCollider2D>();
		m_renderer = GetComponent<SpriteRenderer>();
		m_renderer.sprite = m_sprite1;
		if (!m_eternalFlames)
		{
			Destroy(gameObject, m_lifeSpan2);
		}
	}
	

	void Update () 
	{
		timePassed += Time.deltaTime;
		
		if (timePassed >= m_lifeSpan1 && m_state == 1)
		{
			m_state = 2;
			m_renderer.sprite = m_sprite2;
			m_collider.enabled = true;
		}
		if (m_eternalFlames && timePassed >= m_lifeSpan2 && m_state ==2)
		{
			m_state = 1;
			m_renderer.sprite = m_sprite1;
			m_collider.enabled = false;
			timePassed = 0f;
			m_lifeSpan1 = Random.Range(0.1f, 1f);
			m_lifeSpan2 = Random.Range(3f, 5f);
		}
		if (GameState.myGameState.m_enemyCount <= 0)
		{
			Destroy(gameObject, 1f);
		}
	}
	
	
	void OnTriggerEnter2D (Collider2D p_collider)
	{
		//print ("Collision!");
		if (p_collider.gameObject.tag == "Filip")
		{
			p_collider.gameObject.SendMessage( "ApplyDamage", m_damage );
		}
	}
}
