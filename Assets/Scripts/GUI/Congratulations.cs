using UnityEngine;
using System.Collections;

public class Congratulations : MonoBehaviour 
{

	public string m_menuLevel = "Menu";
	
	
	void Start () 
	{
		pintarCongratulations();
	}
	
	
	void Update () 
	{
		Menu ();		
	}
	
	
	private void pintarCongratulations()
	{
		Simbolos.print("CONGRATULATIONS!!", new Vector2(1.5f,13f));
		Simbolos.print("You beat the dragon", new Vector2(0.5f,10f));
		Simbolos.print("and escaped from", new Vector2(2f,8.5f));
		Simbolos.print("the UNKNOWN DUNGEONS", new Vector2(0f,7f));
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
		Destroy(FilipState.myFilip.gameObject);
		Application.LoadLevel(m_menuLevel);
	}
}
