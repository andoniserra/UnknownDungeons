using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private float timeUpdate = 0.0f;
	private float leerControl = 0.1f;
	private int posFlec = 0;
	private GameObject flecha;
	private int playerPos = 0;

	private GameObject sonidoSi;
	private GameObject musicaSi;
	private GameObject sonidoNo;
	private GameObject musicaNo;


	private bool sound = true;
	private bool music = true;

	public AudioSource selectSound;
	public AudioSource menuMusic;

	// Use this for initialization
	void Start () {
		pintarMenu();
	}

	// Update is called once per frame
	void Update () {

		switch (playerPos){
			case 0: menu (); break;
			case 1: opciones(); break;
			case 2: halp(); break;
			default: playerPos = 0; break;
		}

	}

	private void menu(){
		float pulsacion = Input.GetAxis("Vertical");
		if(Time.time > timeUpdate){
			timeUpdate += leerControl;
			
			if(pulsacion > 0){
				Debug.Log ("arriba");
				posFlec -= 1;
				if (posFlec < 0)
					posFlec = 2;
			}else if(pulsacion < 0){
				Debug.Log ("abajo");
				posFlec += 1;
				if (posFlec > 2)
					posFlec = 0;
			}
		}
		pintarflechaMenu();
		
		if (Input.GetButtonDown("Fire")){
			selectSound.Play();

			GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
			
			foreach(GameObject GoAct in go){
				
				Destroy(GoAct);
			}

			playerPos = posFlec;

			if(posFlec == 1){
				posFlec = 0;
				pintarOpciones();
			}else if(posFlec == 2){
				pintarHalp();
			}
		}
	}

	private void pintarMenu(){
		posFlec = 0;
		Simbolos.caja(new Vector2 (4,11), new Vector2(15,15));
		Simbolos.print("start", new Vector2(7,14));
		Simbolos.print("options",new Vector2(7,13));
		Simbolos.print("halp!", new Vector2(7,12));
		Simbolos.colocarImagen(new GameObject(), Simbolos.Cat, new Vector2(12,12));
	}

	private void pintarflechaMenu(){

		if(flecha == null){
			flecha = new GameObject();
			flecha.AddComponent(typeof(SpriteRenderer));
		}

		Simbolos.colocarImagen(flecha, Simbolos.Right, new Vector2(6,14 - posFlec));
	}


	private void opciones(){
		float pulsacion = Input.GetAxis("Vertical");
		if(Time.time > timeUpdate){
			timeUpdate += leerControl;
			
			if(pulsacion > 0){
				posFlec -= 1;
				if (posFlec < 0)
					posFlec = 2;
			}else if(pulsacion < 0){
				posFlec += 1;
				if (posFlec > 2)
					posFlec = 0;
			}
		

			if (Input.GetButton("Fire")){
				switch(posFlec){
				case 0: cambiarMusica();break;
				case 1: cambiarSonido();break;
				case 2: break;
				}
				selectSound.Play ();
			}

			if (Input.GetButtonDown("Defend")){
				Debug.Log("Defend");
				GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
				
				foreach(GameObject GoAct in go){
					Destroy(GoAct);
				}

				pintarMenu();
				playerPos = 0;
			}
		}
		pintarflechaOpc();
	}

	private void pintarOpciones(){
		Simbolos.caja(new Vector2(0,4), new Vector2(19,13));
		Simbolos.print("music    yes  no", new Vector2(2,11));
		Simbolos.print("sound    yes  no", new Vector2(2,9));
		Simbolos.print("credits", new Vector2(2,7));

		sonidoSi = new GameObject();
		musicaSi = new GameObject();
		sonidoNo = new GameObject();
		musicaNo = new GameObject();

		if(music){
			Simbolos.colocarImagen(musicaSi, Simbolos.Left, new Vector2(14,11));
		}else{
			Simbolos.colocarImagen(musicaNo, Simbolos.Left, new Vector2(18,11));
		}
		if(sound){
			Simbolos.colocarImagen(sonidoSi, Simbolos.Left, new Vector2(14,9));
		}else{
			Simbolos.colocarImagen(sonidoNo, Simbolos.Left, new Vector2(18,9));
		}

	}

	private void pintarflechaOpc(){
		if(flecha == null){
			flecha = new GameObject();
			flecha.AddComponent(typeof(SpriteRenderer));
		}
		Simbolos.colocarImagen(flecha, Simbolos.Right, new Vector2(1,11 - (posFlec*2)));
	}

	private void cambiarMusica(){
		if(menuMusic.isPlaying){
			music = false;
			Simbolos.colocarImagen(musicaSi, Simbolos.Blank, new Vector2(14,11));
			Simbolos.colocarImagen(musicaNo, Simbolos.Left, new Vector2(18,11));
			menuMusic.Stop();
		}else{
			music = true;
			Simbolos.colocarImagen(musicaSi, Simbolos.Left, new Vector2(14,11));
			Simbolos.colocarImagen(musicaNo, Simbolos.Blank, new Vector2(18,11));
			menuMusic.Play();
		}
	}

	private void cambiarSonido(){
		if(selectSound.volume == 1f){
			sound = false;
			Simbolos.colocarImagen(sonidoSi, Simbolos.Blank, new Vector2(14,9));
			Simbolos.colocarImagen(sonidoNo, Simbolos.Left, new Vector2(18,9));
			selectSound.volume = 0f;
		}else{
			sound = true;
			Simbolos.colocarImagen(sonidoSi, Simbolos.Left, new Vector2(14,9));
			Simbolos.colocarImagen(sonidoNo, Simbolos.Blank, new Vector2(18,9));
			selectSound.volume = 1f;
		}
	}

	private void selectCredits(){

	}

	private void halp(){

		if (Input.GetButtonDown("Defend")){
			
			GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
			
			foreach(GameObject GoAct in go){
				Destroy(GoAct);
			}
			
			pintarMenu();
			playerPos = 0;
		}
	}

	private void pintarHalp(){

	}
}
