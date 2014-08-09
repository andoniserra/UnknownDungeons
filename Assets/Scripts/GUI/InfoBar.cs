using UnityEngine;
using System.Collections;

public class InfoBar : MonoBehaviour {

	private GameObject barBackGround;
	
	private GameObject SwordLvl;
	private GameObject Bowlvl;
	private GameObject Magiclvl;
	private GameObject Shieldlvl;

	private GameObject CoinsU;
	private GameObject CoinsD;
	private GameObject CoinsC;

	private GameObject Level;


	// Use this for initialization
	void Start () {

		#region Fondo
		barBackGround = new GameObject();
		Simbolos.colocarImagen(barBackGround,Resources.Load<Sprite>("Sprites/GUIBack"),new Vector2(-10,-7));
		#endregion

		#region Creacion de indicadores de armas

		SwordLvl = new GameObject();
		SwordLvl.AddComponent(typeof(SpriteRenderer));
		Bowlvl = new GameObject();
		Bowlvl.AddComponent(typeof(SpriteRenderer));
		Magiclvl = new GameObject();
		Magiclvl.AddComponent(typeof(SpriteRenderer));
		Shieldlvl = new GameObject();
		Shieldlvl.AddComponent(typeof(SpriteRenderer));

		#endregion

		//Indicador de habitacion

		//Indicador de monedas
	}
	
	// Update is called once per frame
	void Update () {

		//Espada -3, -7.5
		SwordLvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_swordLvl];
		Simbolos.colocarImagen(SwordLvl, SwordLvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-3f, -7.5f), "GUI", 3);

		//arco -1.75, -7.5
		Bowlvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_arrowLvl];
		Simbolos.colocarImagen(Bowlvl, Bowlvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-1.75f, -7.5f), "GUI", 3);

		//Magia -0.3, -7.5
		Magiclvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_magicLvl];
		Simbolos.colocarImagen(Magiclvl, Magiclvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-0.3f, -7.5f), "GUI", 3);

		//Escudo 1.45, -7.5
		Shieldlvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_shieldLvl];
		Simbolos.colocarImagen(Shieldlvl, Shieldlvl.GetComponent<SpriteRenderer>().sprite, new Vector2(1.45f, -7.5f), "GUI", 3);

		int udds, dcns, cnts;
		udds = (FilipState.myFilip.m_coins % 100) % 10;
		dcns = (FilipState.myFilip.m_coins / 10) % 10;
		cnts = FilipState.myFilip.m_coins / 100;


		/*
		//TODO: mostrar bien las monedas

		Coins.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_swordLvl];
		Simbolos.colocarImagen(SwordLvl, SwordLvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-3, -7.5f), "GUI", 3);


		Level.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_coins];
		Simbolos.colocarImagen(SwordLvl, SwordLvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-3, -7.5f), "GUI", 3);
		*/
	}

}
