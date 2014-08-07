using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class ColorChanger : MonoBehaviour 
{
	private SpriteRenderer m_renderer;
	private bool changeColorOnRuntime = false;


	void Start () 
	{
		m_renderer = gameObject.GetComponent<SpriteRenderer>();
		//print (SpriteColorSelector.scs.m_sceneColor);
		m_renderer.color = SpriteColorSelector.scs.m_sceneColor;
		changeColorOnRuntime = SpriteColorSelector.scs.m_fixedColor;
	}
	

	void Update () 
	{
		if (!changeColorOnRuntime)
		{
			m_renderer.color = SpriteColorSelector.scs.m_sceneColor;
		}
	}
}
