using UnityEngine;
using System.Collections;

public class InfoBar : MonoBehaviour {

	private GameObject barBackGround;

	private GameObject Heart1;
	private GameObject Heart2;
	private GameObject Heart3;
	private GameObject Heart4;
	private GameObject Heart5;
	private GameObject[] hearts;


	private GameObject SwordLvl;
	private GameObject Bowlvl;
	private GameObject Magiclvl;
	private GameObject Shieldlvl;

	private GameObject CoinsU;
	private GameObject CoinsD;
	private GameObject CoinsC;

	private GameObject Level;
	private GameObject LevelU;
	private GameObject LevelD;

	private GameObject[] Life;


	// Use this for initialization
	void Start () {

		GameObject BarraInfo = new GameObject("InfoBar");

		#region Fondo
		barBackGround = new GameObject("Fondo");
		Simbolos.colocarImagen(barBackGround,Resources.Load<Sprite>("Sprites/GUIBack"),new Vector2(-10,-7));
		barBackGround.transform.parent = BarraInfo.transform;
		barBackGround.AddComponent(typeof(ColorChanger));
		#endregion

		#region Vida
		GameObject Corazones = new GameObject("Corazones");

		Heart1 = new GameObject("Heart1");
		Heart1.AddComponent(typeof(SpriteRenderer));
		Heart1.AddComponent(typeof(ColorChanger));
		Heart1.transform.parent = Corazones.transform;

		Heart2 = new GameObject("Heart2");
		Heart2.AddComponent(typeof(SpriteRenderer));
		Heart2.AddComponent(typeof(ColorChanger));
		Heart2.transform.parent = Corazones.transform;

		Heart3 = new GameObject("Heart3");
		Heart3.AddComponent(typeof(SpriteRenderer));
		Heart3.AddComponent(typeof(ColorChanger));
		Heart3.transform.parent = Corazones.transform;

		Heart4 = new GameObject("Heart4");
		Heart4.AddComponent(typeof(SpriteRenderer));
		Heart4.AddComponent(typeof(ColorChanger));
		Heart4.transform.parent = Corazones.transform;

		Heart5 = new GameObject("Heart5");
		Heart5.AddComponent(typeof(SpriteRenderer));
		Heart5.AddComponent(typeof(ColorChanger));
		Heart5.transform.parent = Corazones.transform;

		hearts = new GameObject[5] {Heart1, Heart2, Heart3, Heart4, Heart5};

		Corazones.transform.parent = BarraInfo.transform;
		#endregion

		#region Creacion de indicadores de armas
		GameObject armas = new GameObject("Armas");

		SwordLvl = new GameObject("Sword");
		SwordLvl.AddComponent(typeof(SpriteRenderer));
		SwordLvl.AddComponent(typeof(ColorChanger));
		SwordLvl.transform.parent = armas.transform;

		Bowlvl = new GameObject("Bow");
		Bowlvl.AddComponent(typeof(SpriteRenderer));
		Bowlvl.AddComponent(typeof(ColorChanger));
		Bowlvl.transform.parent = armas.transform;

		Magiclvl = new GameObject("Magic");
		Magiclvl.AddComponent(typeof(SpriteRenderer));
		Magiclvl.AddComponent(typeof(ColorChanger));
		Magiclvl.transform.parent = armas.transform;

		Shieldlvl = new GameObject("Shield");
		Shieldlvl.AddComponent(typeof(SpriteRenderer));
		Shieldlvl.AddComponent(typeof(ColorChanger));
		Shieldlvl.transform.parent = armas.transform;

		armas.transform.parent = BarraInfo.transform;
		#endregion

		#region Nivel
		//Indicador de habitacion
		//TODO: cuando este terminado el generador de mazmorras
		Level = new GameObject("Nivel");
		LevelU = new GameObject("Unidades");
		LevelU.AddComponent(typeof(SpriteRenderer));
		LevelU.AddComponent(typeof(ColorChanger));
		LevelU.transform.parent = Level.transform;
		LevelD = new GameObject("Decenas");
		LevelD.AddComponent(typeof(SpriteRenderer));
		LevelD.AddComponent(typeof(ColorChanger));
		LevelD.transform.parent = Level.transform;

		Level.transform.parent = BarraInfo.transform;
		#endregion

		#region monedas
		GameObject Monedas = new GameObject("Monedas");
		
		//Indicador de monedas
		CoinsU = new GameObject("Unidades");
		CoinsU.AddComponent(typeof(SpriteRenderer));
		CoinsU.AddComponent(typeof(ColorChanger));
		CoinsU.transform.parent = Monedas.transform;
		CoinsD = new GameObject("Decenas");
		CoinsD.AddComponent(typeof(SpriteRenderer));
		CoinsD.AddComponent(typeof(ColorChanger));
		CoinsD.transform.parent = Monedas.transform;
		CoinsC = new GameObject("Centenas");
		CoinsC.AddComponent(typeof(SpriteRenderer));
		CoinsC.AddComponent(typeof(ColorChanger));
		CoinsC.transform.parent = Monedas.transform;

		Monedas.transform.parent = BarraInfo.transform;
		#endregion

	}
	
	// Update is called once per frame
	void Update () {

		//Vida -9.6, -7.6
		int corazones = FilipState.myFilip.m_fullHP;
		int vida = FilipState.myFilip.m_hp;

		for( int cor = 0; cor < corazones; cor++){

			SpriteRenderer ht = hearts[cor].GetComponent<SpriteRenderer>();
			ht.sortingLayerName = "GUI";
			ht.sortingOrder = 3;
			hearts[cor].transform.position = new Vector3(-9.6f + cor, -7.6f,0);
			//Debug.Log(FilipState.myFilip.m_hp);
			if(cor < vida){
				ht.sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[42];
			}else{
				ht.sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[41];
			}

		}


		//Espada
		SwordLvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_swordLvl];
		Simbolos.colocarImagen(SwordLvl, SwordLvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-3f, -7.5f), "GUI", 3);

		//arco
		Bowlvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_arrowLvl];
		Simbolos.colocarImagen(Bowlvl, Bowlvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-1.75f, -7.5f), "GUI", 3);

		//Magia
		Magiclvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_magicLvl];
		Simbolos.colocarImagen(Magiclvl, Magiclvl.GetComponent<SpriteRenderer>().sprite, new Vector2(-0.3f, -7.5f), "GUI", 3);

		//Escudo
		Shieldlvl.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + FilipState.myFilip.m_shieldLvl];
		Simbolos.colocarImagen(Shieldlvl, Shieldlvl.GetComponent<SpriteRenderer>().sprite, new Vector2(1.45f, -7.5f), "GUI", 3);

		//Monedas
		int udds, dcns, cnts;
		udds = (FilipState.myFilip.m_coins % 100) % 10;
		dcns = (FilipState.myFilip.m_coins / 10) % 10;
		cnts = FilipState.myFilip.m_coins / 100;
		//Centenas
		CoinsC.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + cnts];
		Simbolos.colocarImagen(CoinsC, CoinsC.GetComponent<SpriteRenderer>().sprite, new Vector2(4.4f, -7.5f), "GUI", 3);
		//Decenas
		CoinsD.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + dcns];
		Simbolos.colocarImagen(CoinsD, CoinsD.GetComponent<SpriteRenderer>().sprite, new Vector2(4.9f, -7.5f), "GUI", 3);
		//Unidades
		CoinsU.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + udds];
		Simbolos.colocarImagen(CoinsU, CoinsU.GetComponent<SpriteRenderer>().sprite, new Vector2(5.4f, -7.5f), "GUI", 3);


		int roomUs = (GameState.myGameState.m_rooms % 100) % 10;
		int roomDs = (GameState.myGameState.m_rooms / 10) % 10;
		//Mazmorra en la que estamos
		LevelD.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + roomDs];
		Simbolos.colocarImagen(LevelD, LevelD.GetComponent<SpriteRenderer>().sprite, new Vector2(8.25f, -7.5f), "GUI", 3);
		LevelU.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/tipografia")[66 + roomUs];
		Simbolos.colocarImagen(LevelU, LevelU.GetComponent<SpriteRenderer>().sprite, new Vector2(8.75f, -7.5f), "GUI", 3);


	}

}
