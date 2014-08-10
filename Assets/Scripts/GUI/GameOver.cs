using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
	public string m_menuLevel = "Menu";


	void Start () 
	{
		pintarGameOver();
	}
	

	void Update () 
	{
		Menu ();		
	}


	private void pintarGameOver()
	{
		Simbolos.print("GAME OVER", new Vector2(5.5f,9f));
	}


	private void Menu ()
	{
		if (Input.GetButtonDown("Fire"))
		{
			SoundHelper.PlayMenuArrow();

			StopCoroutine("WaitAndDestroy");
			StartCoroutine("WaitAndDestroy", 1f);
		}
	}


	IEnumerator WaitAndDestroy ( float p_time )
	{
		yield return new WaitForSeconds(p_time);

		GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
		
		foreach(GameObject GoAct in go)
		{
			Destroy(GoAct);
		}
		Destroy(Music.music.gameObject);
		Destroy(GameState.myGameState.gameObject);
		Application.LoadLevel(m_menuLevel);
	}

}
