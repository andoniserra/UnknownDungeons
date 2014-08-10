using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	private float lastTime = 0.0f;
	private int posFlec = 0;
	private GameObject flecha;
	private int playerPos = 0;

	private GameObject sonidoSi;
	private GameObject musicaSi;
	private GameObject sonidoNo;
	private GameObject musicaNo;

	private GameObject creditImage;
	private GameObject halpImage;

	public static bool sound = true;
	public static bool music = true;

	private float pulsacion;
	private bool select = false;
	private bool back = false;

	public AudioSource selectSound;
	public AudioSource menuMusic;
	 
	public string m_startLevel;

	
	void Start () {
		//Imagen de los creditos
		creditImage = new GameObject();
		creditImage.AddComponent<SpriteRenderer>();
		creditImage.transform.position = new Vector2(0,0);
		//Creamos el sprite
		SpriteRenderer creditos = creditImage.GetComponent<SpriteRenderer>();
		creditos.sprite = Resources.Load<Sprite>("Sprites/creditos");
		creditos.sortingLayerName = "GUI";
		creditos.sortingOrder = 2;

		//Imagen de la ayuda
		halpImage = new GameObject();
		halpImage.AddComponent<SpriteRenderer>();
		halpImage.transform.position = new Vector2(0,0);
		//Creamos el sprite
		SpriteRenderer halp = halpImage.GetComponent<SpriteRenderer>();
		halp.sprite = Resources.Load<Sprite>("Sprites/halp");
		halp.sortingLayerName = "GUI";
		halp.sortingOrder = 2;

		// Ponemos siempre el color neutro en el menu
		SpriteColorSelector.SetSceneColor(1);

		//Generamos el menu
		pintarMenu();
	}

	void Update () {

		//Obtenemos las pulsaciones
		if (Input.GetButton("Fire")){
			select = true;
		}else{
			select = false;
		}

		if (Input.GetButton("Defend")){
			back = true;
		}else{
			back = false;
		}
		
		pulsacion = Input.GetAxis("Vertical");

		//Lo ejecutamos cada 0.1 segundos
		if (Time.time > lastTime){ 
			lastTime = Time.time + 0.1f;
			
			switch (playerPos){
			case 0: menu (); break;			//Menu inicial
			case 1: opciones(); break;		//Menu opciones
			case 2: pintarHalp(); break;	//Ayuda
			case 3: selectCredits(); break;	//Creditos
			default: playerPos = 0; break;	//Cualquier otra cosa vuelve al menu inicial
			}
		}

	}

	#region Menu Inicial

	private void menu(){
			
		//Cuando cambiemos la posicion que suene y que mueva posFlec
		if(pulsacion > 0){
			selectSound.Play();
			posFlec -= 1;
			if (posFlec < 0)
				posFlec = 2;
		}else if(pulsacion < 0){
			selectSound.Play();
			posFlec += 1;
			if (posFlec > 2)
				posFlec = 0;
		}

		//Pintamos la flecha en su sitio
		pintarflechaMenu();

		//Cuando seleccionemos algo...
		if (select){
			GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
			
			foreach(GameObject GoAct in go){
				
				Destroy(GoAct);
			}

			//Indicamos que se ha seleccionado
			playerPos = posFlec;

			// Para cargar la escena de los slimes
			if (posFlec == 0)
			{
				Application.LoadLevel(m_startLevel);
			}

			//Para cargar las opciones
			if(posFlec == 1){
				posFlec = 0;
				pintarOpciones();

				//Para mostrar la ayuda
			}else if(posFlec == 2){
				halpImage.transform.position = new Vector3(0,18,0);
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

		//Si no existe la flecha la creamos
		if(flecha == null){
			flecha = new GameObject();
			flecha.AddComponent(typeof(SpriteRenderer));
		}
		Simbolos.colocarImagen(flecha, Simbolos.Right, new Vector2(6,14 - posFlec));
	}

	#endregion

	#region Opciones
	private void opciones(){

		//Cuando cambiemos la posicion que suene y que mueva posFlec
		if(pulsacion > 0){
			selectSound.Play ();
			posFlec -= 1;
			if (posFlec < 0)
				posFlec = 2;
		}else if(pulsacion < 0){
			selectSound.Play ();
			posFlec += 1;
			if (posFlec > 2)
				posFlec = 0;
		}
	
		if (select){
			switch(posFlec){
			case 0: cambiarMusica();break;	//Activamos o desactivamos la musica
			case 1: cambiarSonido();break;	//Activamos o desactivamos el sonido
			case 2: creditos(); break;
			}

		}

		if (back){
			GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
			
			foreach(GameObject GoAct in go){
				Destroy(GoAct);
			}

			pintarMenu();
			playerPos = 0;
		}
		
		pintarflechaOpc();
	}

	private void pintarOpciones(){
		Simbolos.caja(new Vector2(0,5), new Vector2(19,14));
		Simbolos.print("music    yes  no", new Vector2(2,12));
		Simbolos.print("sound    yes  no", new Vector2(2,10));
		Simbolos.print("credits", new Vector2(2,8));

		sonidoSi = new GameObject();
		musicaSi = new GameObject();
		sonidoNo = new GameObject();
		musicaNo = new GameObject();

		if(music){
			Simbolos.colocarImagen(musicaSi, Simbolos.Left, new Vector2(14,12));
		}else{
			Simbolos.colocarImagen(musicaNo, Simbolos.Left, new Vector2(18,12));
		}
		if(sound){
			Simbolos.colocarImagen(sonidoSi, Simbolos.Left, new Vector2(14,10));
		}else{
			Simbolos.colocarImagen(sonidoNo, Simbolos.Left, new Vector2(18,10));
		}

	}

	private void pintarflechaOpc(){
		if(flecha == null){
			flecha = new GameObject();
			flecha.AddComponent(typeof(SpriteRenderer));
		}
		Simbolos.colocarImagen(flecha, Simbolos.Right, new Vector2(1,12 - (posFlec*2)));
	}
	
	private void cambiarMusica(){
		if(menuMusic.isPlaying){
			music = false;
			Simbolos.colocarImagen(musicaSi, Simbolos.Blank, new Vector2(14,12));
			Simbolos.colocarImagen(musicaNo, Simbolos.Left, new Vector2(18,12));
			menuMusic.Stop();
		}else{
			music = true;
			Simbolos.colocarImagen(musicaSi, Simbolos.Left, new Vector2(14,12));
			Simbolos.colocarImagen(musicaNo, Simbolos.Blank, new Vector2(18,12));
			menuMusic.Play();
		}
	}

	private void cambiarSonido(){
		if(selectSound.volume == 0.25f){
			sound = false;
			Simbolos.colocarImagen(sonidoSi, Simbolos.Blank, new Vector2(14,10));
			Simbolos.colocarImagen(sonidoNo, Simbolos.Left, new Vector2(18,10));
			selectSound.volume = 0f;
		}else{
			sound = true;
			Simbolos.colocarImagen(sonidoSi, Simbolos.Left, new Vector2(14,10));
			Simbolos.colocarImagen(sonidoNo, Simbolos.Blank, new Vector2(18,10));
			selectSound.volume = 0.25f;
		}
	}

	#endregion

	#region Creditos
	private void creditos(){
		GameObject[] go = GameObject.FindGameObjectsWithTag("TempTile");
		
		foreach(GameObject GoAct in go){
			Destroy(GoAct);
		}
		playerPos = 3;
	}

	private void selectCredits(){
		//Hacemos que la ventana de creditos suba poco a poco, cuando salga de la pantalla volvemos a las opciones
		creditImage.transform.position = creditImage.transform.position + new Vector3(0, Time.deltaTime * 16,0);
		if(creditImage.transform.position.y > 45){
			posFlec = 0;
			playerPos = 1;
			creditImage.transform.position = new Vector3(0,0,0);
			pintarOpciones();
		}
	}
	#endregion
	

	private void pintarHalp(){

		//Mostramos la ayuda y cuando pulsemos una tecla volvemos al menu
		if (back || select){

			halpImage.transform.position = new Vector3(0,0,0);
			
			pintarMenu();
			playerPos = 0;
		}
	}

}
