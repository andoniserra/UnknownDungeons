using UnityEngine;
using System.Collections;

public class CoinMeter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
		
		foreach(GameObject GoAct in go)
		{
			Destroy(GoAct);
		}
		SymbolsAdapted.print(FilipState.myFilip.m_coins.ToString(), new Vector2(16f,0.5f));
	}
}
