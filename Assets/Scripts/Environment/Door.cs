using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public GLOBALS.Direction m_doorDirection = GLOBALS.Direction.North;

	public bool m_open = false;

	public Sprite m_closedSprite;
	public Sprite m_openSprite;

	public string m_targetScene;

	private bool m_opened = false;
	private bool m_closed = false;

	private BoxCollider2D m_closedCollider;
	private SpriteRenderer m_renderer;

	void Start ()
	{
		m_closedCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
		m_renderer = GetComponent<SpriteRenderer>();

		if (m_open)
		{
			m_closedCollider.enabled = false;
			m_opened = true;
			m_renderer.sprite = m_openSprite;
		}
		else
		{
			m_closedCollider.enabled = true;
			m_closed = true;
			m_renderer.sprite = m_closedSprite;
		}
	}


	void Update ()
	{
		if (m_open && !m_opened)
		{
			m_closedCollider.enabled = false;
			m_opened = true;
			m_renderer.sprite = m_openSprite;
		}

		if (!m_open && !m_closed)
		{
			m_closedCollider.enabled = true;
			m_closed = true;
			m_renderer.sprite = m_closedSprite;
		}
	}
}
