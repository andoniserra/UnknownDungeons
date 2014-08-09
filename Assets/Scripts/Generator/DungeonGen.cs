using UnityEngine;
using System.Collections;

public class DungeonGen : MonoBehaviour {
	
	Sprite[] muros1;
	Sprite[] suelo1;
	GameObject[,] sala = new GameObject[10,8];
	
	// Use this for initialization
	void Start () {
		for(int x = 0; x < 10; x++){
			for(int y = 0; y < 8; y++){
				sala[x,y] = new GameObject();
			}
		}
		muros1 = Resources.LoadAll<Sprite>("Resources/Sprites/muros01");
		suelo1 = Resources.LoadAll<Sprite>("Resources/Sprites/suelos01");
		generarEsquinas();
		generarMuros ();
		generarPuertas ();
		generarSuelo ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void generarEsquinas(){
		
		//Esquina izquierda abajo
		Mazmorras.colocarImagen(sala[0,0],muros1[1], new Vector2(0,0));
		//Esquina derecha abajo
		Mazmorras.colocarImagen(sala[9,0],muros1[0], new Vector2(9,0));
		//Esquina arriba derecha
		Mazmorras.colocarImagen(sala[0,7],muros1[3], new Vector2(0,7));
		//Esquina arriba izquierda
		Mazmorras.colocarImagen(sala[9,7],muros1[2], new Vector2(9,7));
		
	}
	
	private void generarPuertas(){
		//Puertas aleatorias
		int rand1 = Mathf.RoundToInt(Random.Range (1, 8)); //puerta abajo
		int rand2 = Mathf.RoundToInt(Random.Range (1, 8)); //puerta arriba
		int rand3 = Mathf.RoundToInt(Random.Range (1, 6)); //puerta derecha
		int rand4 = Mathf.RoundToInt(Random.Range (1, 6)); //puerta izquierda
		
		GameObject puerta1 = sala [rand1, 0];
		GameObject puerta2 = sala [rand2, 7];
		GameObject puerta3 = sala [9, rand3];
		GameObject puerta4 = sala [0, rand4];
		
		puerta1.tag = "puerta";
		puerta2.tag = "puerta";
		puerta3.tag = "puerta";
		puerta4.tag = "puerta";
		
		Mazmorras.colocarImagen(puerta1, muros1[9], new Vector2(rand1,0));
		Mazmorras.colocarImagen(puerta2, muros1[9], new Vector2(rand2,7));
		Mazmorras.colocarImagen(puerta3, muros1[9], new Vector2(9,rand3));
		Mazmorras.colocarImagen(puerta4, muros1[9], new Vector2(0,rand4));
		
		puerta1.transform.Rotate(Vector3.forward * -180);
		puerta3.transform.Rotate(Vector3.forward * -90);
		puerta4.transform.Rotate(Vector3.forward * -270);
		
	}
	
	private void generarMuros(){
		//Muros
		for (int x = 1; x <= 8; x++) {
			if (!sala[x,0].CompareTag("puerta")){
				Mazmorras.colocarImagen(sala[x,0], muros1[4], new Vector2(x,0));
			}
			if (!sala[x,7].CompareTag("puerta")){
				Mazmorras.colocarImagen(sala[x,7], muros1[7], new Vector2(x,7));
			}
		}
		
		for (int y = 1; y <= 6; y++) {
			if (!sala[0,y].CompareTag("puerta")){
				Mazmorras.colocarImagen(sala[0,y], muros1[6], new Vector2(0,y));
			}
			if (!sala[9,y].CompareTag("puerta")){
				Mazmorras.colocarImagen(sala[9,y], muros1[5], new Vector2(9,y));
			}
		}
	}
	//Rocas
	private void generarRocas(){
		
	}
	//Agujeros
	
	//Suelo
	private void generarSuelo(){
		for (int x = 1; x <= 8; x++){
			for (int y = 1; y <= 6; y++){
				Mazmorras.colocarImagen (sala[x,y], suelo1[2], new Vector2(x,y));
			}
		}
	}
	
	
	
	
}