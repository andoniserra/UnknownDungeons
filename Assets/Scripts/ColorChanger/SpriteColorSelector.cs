using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class SpriteColorSelector : MonoBehaviour
{
	public Color [] colors = {new Color(1,0,0), new Color(0,1,0), new Color(0,0,1), 
		new Color(1,1,0), new Color(0,1,1), new Color(1,0,1), new Color(1,1,1)};

	public Color m_sceneColor;

	public bool m_fixedColor = false;

	private int m_index = -1;


	public void RandomColor()
	{
		scs.m_sceneColor = colors[Random.Range (0, colors.Length)];
		print(scs.m_sceneColor);
	}


	public void NextColor()
	{
		m_index += 1;
		if ( m_index >= colors.Length )
		{
			m_index = 0;
		}
		scs.m_sceneColor = colors[m_index];
		print(scs.m_sceneColor);
	}


	public static SpriteColorSelector scs;
	
	
	// Al cargar el script comprobamos si ya existe una instancia
	void Awake () 
	{
		// Si no existe
		if (scs == null)
		{
			// Marcamos el objeto para no ser destruido al cambiar de escena
			DontDestroyOnLoad(gameObject);
			// Asignamos esta instancia a la instancia unica Singleton
			scs = this;
		}
		// Si ya existia
		else if (scs != this)
		{
			// Destruimos este objeto
			Destroy(gameObject);
		}
	}


	public static void SetSceneColor ( int p_index )
	{
		scs.m_sceneColor = scs.colors[p_index];
	}
}