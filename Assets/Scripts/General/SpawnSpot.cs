using UnityEngine;
using System.Collections;

public class SpawnSpot : MonoBehaviour 
{
	public static SpawnSpot theSpawnSpot = null;

	public Transform m_spawnSpotNorth;
	public Transform m_spawnSpotEast;
	public Transform m_spawnSpotSouth;
	public Transform m_spawnSpotWest;


	void Awake () 
	{
		// Si no existe
		if (theSpawnSpot == null)
		{
			// Marcamos el objeto para no ser destruido al cambiar de escena
			//DontDestroyOnLoad(gameObject);
			// Asignamos esta instancia a la instancia unica Singleton
			theSpawnSpot = this;
		}
		// Si ya existia
		else if (theSpawnSpot != this)
		{
			// Destruimos este objeto
			Destroy(gameObject);
		}
	}


	public static void SpawnFilip ( GLOBALS.Direction p_direction )
	{
		//print ("spawningFilip");
		if (p_direction == GLOBALS.Direction.North)
		{
			//print("norte");
			FilipState.myFilip.transform.position = 
				theSpawnSpot.m_spawnSpotSouth.transform.position;
			FilipState.myFilip.FaceDirection(GLOBALS.Direction.North);
		}
		if (p_direction == GLOBALS.Direction.East)
		{
			//print("este");
			FilipState.myFilip.transform.position = 
				theSpawnSpot.m_spawnSpotWest.transform.position;
			FilipState.myFilip.FaceDirection(GLOBALS.Direction.East);
		}
		if (p_direction == GLOBALS.Direction.South)
		{
			//print("sur");
			FilipState.myFilip.transform.position = 
				theSpawnSpot.m_spawnSpotNorth.transform.position;
			FilipState.myFilip.FaceDirection(GLOBALS.Direction.South);
		}
		if (p_direction == GLOBALS.Direction.West)
		{
			//print("oeste");
			FilipState.myFilip.transform.position = 
				theSpawnSpot.m_spawnSpotEast.transform.position;
			FilipState.myFilip.FaceDirection(GLOBALS.Direction.West);
		}
	}
}
