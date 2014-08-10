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

	public int m_rooms = 0;

	private bool m_doorsOpen = false;

	private GLOBALS.Direction m_lastDoorDirection = GLOBALS.Direction.South;
	
	
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

		SpriteColorSelector.SetSceneColor(0);
		// TODO: Comprobar nivel en el que estamos para cambiar la musica si hace falta
		Music.PlayLevel01Music();

	}
	

	void Update () 
	{
		if (m_enemyCount <= 0 && !m_doorsOpen)
		{
			print ("All Dead!");
			foreach (GameObject go in m_doorList)
			{
				go.GetComponent<Door>().m_open = true;
			}
			m_doorsOpen = true;
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			foreach (GameObject go in m_doorList)
			{
				go.GetComponent<Door>().m_open = true;
			}
			//TakeMeToTheDragon();
			m_doorsOpen = true;
		}

		//if (GameState.myGameState.m_rooms >=5 )
		//{
		//	TakeMeToTheDragon();
		//}
	}

	/*
	private void TakeMeToTheDragon ()
	{
		print ("Take me to the Dragon!");
		foreach (GameObject go in m_doorList)
		{
			go.GetComponent<Door>().m_targetScene = "TestDragon";
		}
	}
	*/


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


	public static void LoadScene ( string p_scene, GLOBALS.Direction p_doorDirection )
	{
		//print ("LoadScene");
		myGameState.m_lastDoorDirection = p_doorDirection;
		if (p_scene == "Random")
		{
			int randomInt = Random.Range(0,6);
			switch (randomInt)
			{
			case 0:
				Application.LoadLevel("TestSlime");
				break;
			case 1:
				Application.LoadLevel("TestBat");
				break;
			case 2:
				Application.LoadLevel("TestSet02");
				break;
			case 3:
				Application.LoadLevel("TestSet02_2");
				break;
			case 4:
				Application.LoadLevel("TestSet03");
				break;
			case 5:
				Application.LoadLevel("TestSet03_2");
				break;
			default:
				Application.LoadLevel("TestSlime");
				break;
			}
		}
		else
		{
			Application.LoadLevel(p_scene);
		}
	}


	void OnLevelWasLoaded(int level) 
	{
		//print ("OnLevelWasLoaded");
		if (Application.loadedLevelName != "TestGameOver")
		{
			GameState.myGameState.AddAllEnemies();
			GameState.myGameState.AddAllDoors();
			m_doorsOpen = false;
			// Recolocamos a Filip
			SpawnSpot.SpawnFilip(myGameState.m_lastDoorDirection);
		}

		switch (Application.loadedLevelName)
		{
		case "TestDragon":
			SpriteColorSelector.SetSceneColor(3);
			SpawnSpot.SpawnFilip(GLOBALS.Direction.North);
			break;
		case "TestSlime":
			SpriteColorSelector.SetSceneColor(0);
			break;
		case "TestBat":
			SpriteColorSelector.SetSceneColor(0);
			break;
		case "TestSet02":
			SpriteColorSelector.SetSceneColor(1);
			break;
		case "TestSet02_2":
			SpriteColorSelector.SetSceneColor(1);
			break;
		case "TestSet03":
			SpriteColorSelector.SetSceneColor(2);
			break;
		case "TestSet03_2":
			SpriteColorSelector.SetSceneColor(2);
			break;
		default:
			SpriteColorSelector.SetSceneColor(1);
			break;
		}

		if (Application.loadedLevelName != "TestDragon" && 
		    Application.loadedLevelName != "TestGameOver")
		{
			m_rooms += 1;
		}
	}
}
