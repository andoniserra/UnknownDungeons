using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour 
{
	public Sprite m_full;
	public Sprite m_empty;

	private SpriteRenderer m_renderer;

	// Use this for initialization
	void Start () 
	{
		m_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	public void Full ()
	{
		m_renderer.sprite = m_full;
	}

	public void Empty ()
	{
		m_renderer.sprite = m_empty;
	}
}
