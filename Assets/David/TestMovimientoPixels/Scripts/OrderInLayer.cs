using UnityEngine;
using System.Collections;

public class OrderInLayer : MonoBehaviour 
{

	SpriteRenderer m_renderer;


	void Awake () 
	{
		m_renderer = this.transform.GetComponent<SpriteRenderer>();
	}
	

	void LateUpdate () 
	{
		//renderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteRenderer.bounds.min).y * -1;
		m_renderer.sortingOrder = (int)((this.transform.position.y) * -10);
	}
}
