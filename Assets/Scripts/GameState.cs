using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour 
{
	// Singleton instance
	public static GameState myGameState;

	public GameObject[] m_enemyList;

	public int m_enemyCount = 0;

	public GameObject[] m_doorList;

	private bool m_doorsOpen = false;
	
	
	// Al cargar el script comprobamos si ya existe una instancia
	void Awake () 
	{
		// Si no existe
		if (myGameState == null)
		{
			// Marcamos el objeto para no ser destruido al cambiar de escena
			DontDestroyOnLoad(gameObject);
			// Asignamos esta instancia a la instancia unica Singleton
			myGameState = this;
			//!!!!!!!!!!
		}
		// Si ya existia
		else if (myGameState != this)
		{
			// Destruimos este objeto
			Destroy(gameObject);
		}
	}


	void Start()
	{
		AddAllEnemies();
		AddAllDoors();
		m_doorsOpen = false;

		// TODO: Comprobar nivel en el que estamos para cambiar la musica si hace falta
		Music.PlayLevel01Music();

	}
	

	void Update () 
	{
		if (m_enemyCount <= 0 && !m_doorsOpen)
		{
			print ("All dead!");
			foreach (GameObject go in m_doorList)
			{
				go.GetComponent<Door>().m_open = true;
			}
			m_doorsOpen = true;
		}
	}


	private void AddAllEnemies ()
	{
		m_enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		m_enemyCount = m_enemyList.Length;
	}


	private void AddAllDoors ()
	{
		m_doorList = GameObject.FindGameObjectsWithTag("Door");
	}


	public static void EnemyDead ()
	{
		myGameState.m_enemyCount -= 1;
	}


	public static void PlayerDead ()
	{
		Application.LoadLevel("TestGameOver");
	}


	public static void LoadScene ( string p_scene )
	{
		Application.LoadLevel(p_scene);
	}


	void OnLevelWasLoaded(int level) 
	{
		GameState.myGameState.AddAllEnemies();
		GameState.myGameState.AddAllDoors();
		m_doorsOpen = false;
	}
}
