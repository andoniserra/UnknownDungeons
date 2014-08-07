using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public static Music music;

	void Awake () 
	{
		// Si no existe
		if (music == null)
		{
			// Marcamos el objeto para no ser destruido al cambiar de escena
			DontDestroyOnLoad(gameObject);
			// Asignamos esta instancia a la instancia unica Singleton
			music = this;
		}
		// Si ya existia
		else if (music != this)
		{
			// Destruimos este objeto
			Destroy(gameObject);
		}
	}
}
