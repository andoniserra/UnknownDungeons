using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private float timeUpdate = 0.0f;
	private float leerControl = 0.1f;
	private int posFlec = 0;
	private GameObject flecha;
	private int playerPos = 0;

	// Use this for initialization
	void Start () {

		flecha = new GameObject();
		flecha.AddComponent(typeof(SpriteRenderer));
		pintarMenu();

	}

	// Update is called once per frame
	void Update () {

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
		pintarflecha();
		
		if (Input.GetButtonDown("Fire")){
			Debug.Log("fire!!!!");
		}

		if (Input.GetButtonDown("Defend")){
			Debug.Log("defend");
		}

		if (Input.GetButtonDown("Select")){
			Debug.Log("select");
		}

		if (Input.GetButtonDown("Start")){
			Debug.Log("start");
		}



	}

	private void pintarMenu(){
		Simbolos.caja(new Vector2 (4,11), new Vector2(15,15));
		Simbolos.print("start", new Vector2(7,14));
		Simbolos.print("options",new Vector2(7,13));
		Simbolos.print("halp!", new Vector2(7,12));
		Simbolos.colocarImagen(new GameObject(), Simbolos.Cat, new Vector2(12,12));
	}

	private void pintarflecha(){
		Simbolos.colocarImagen(flecha, Simbolos.Right, new Vector2(6,14 - posFlec));
	}

}
