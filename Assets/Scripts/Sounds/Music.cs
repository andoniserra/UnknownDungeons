using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public static Music music;

	public AudioClip m_menuMusic;
	public AudioClip m_level01Music;

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


	public static void PlayMenuMusic ()
	{
		music.audio.clip = music.m_menuMusic;
		music.audio.Play();
	}


	public static void PlayLevel01Music ()
	{
		music.audio.clip = music.m_level01Music;
		music.audio.Play();
	}
}
