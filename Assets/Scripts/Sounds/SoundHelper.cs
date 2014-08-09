using UnityEngine;
using System.Collections;

public class SoundHelper : MonoBehaviour 
{
	// Singleton instance
	public static SoundHelper mySoundHelper;


	#region Audio clips
	public AudioClip m_playerHit;
	public AudioClip m_mobHit;
	public AudioClip m_swordSwing;
	public AudioClip m_arrowShot;
	public AudioClip m_magicBall;
	public AudioClip m_menuArrow;
	public AudioClip m_pickCoin;
	#endregion
	
	
	// Al cargar el script comprobamos si ya existe una instancia
	void Awake () 
	{
		// Si no existe
		if (mySoundHelper == null)
		{
			// Marcamos el objeto para no ser destruido al cambiar de escena
			DontDestroyOnLoad(gameObject);
			// Asignamos esta instancia a la instancia unica Singleton
			mySoundHelper = this;
		}
		// Si ya existia
		else if (mySoundHelper != this)
		{
			// Destruimos este objeto
			Destroy(gameObject);
		}
	}

	
	void Start () 
	{
	
	}
	

	void Update () 
	{
	
	}


	public static void PlayPlayerHit ()
	{
		//print ("sound playerHit");
		mySoundHelper.audio.clip = mySoundHelper.m_playerHit;
		mySoundHelper.audio.Play();
	}


	public static void PlayMobHit ()
	{
		//print ("sound mobHit");
		mySoundHelper.audio.clip = mySoundHelper.m_mobHit;
		mySoundHelper.audio.Play();
	}


	public static void PlaySwordSwing ()
	{
		//print ("sound swordSwing");
		mySoundHelper.audio.clip = mySoundHelper.m_swordSwing;
		mySoundHelper.audio.Play();
	}


	public static void PlayArrowShot ()
	{
		//print ("sound arrowShot");
		mySoundHelper.audio.clip = mySoundHelper.m_arrowShot;
		mySoundHelper.audio.Play();
	}

	
	public static void PlayMagicBall ()
	{
		//print ("sound magicBall");
		mySoundHelper.audio.clip = mySoundHelper.m_magicBall;
		mySoundHelper.audio.Play();
	}


	public static void PlayMenuArrow ()
	{
		mySoundHelper.audio.clip = mySoundHelper.m_menuArrow;
		mySoundHelper.audio.Play();
	}


	public static void PlayPickCoin ()
	{
		mySoundHelper.audio.clip = mySoundHelper.m_pickCoin;
		mySoundHelper.audio.Play();
	}
}
