using UnityEngine;
using System.Collections;

public class DungeonGen : MonoBehaviour {
	
	Sprite[] muros1;
	Sprite[] suelo1;
	GameObject[,] sala = new GameObject[10,8];
	public Transform filip;
	
	// Use this for initialization
	void Start () {
		for(int x = 0; x < 10; x++){
			for(int y = 0; y < 8; y++){
				sala[x,y] = new GameObject();
			}
		}
		muros1 = Resources.LoadAll<Sprite>("Tiles/muros01");
		suelo1 = Resources.LoadAll<Sprite>("Tiles/suelos01");
		Instantiate (filip);
		generarEsquinas();
		generarMuros ();
		generarPuertas ();
		generarRocas ();
		generarAgujeros ();
		generarSuelo ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void generarEsquinas(){
		
		//Esquina izquierda abajo
		Mazmorras.colocarImagen(sala[0,0],muros1[0], new Vector2(0,0));
		//Esquina derecha abajo
		Mazmorras.colocarImagen(sala[9,0],muros1[2], new Vector2(9,0));
		//Esquina arriba derecha
		Mazmorras.colocarImagen(sala[0,7],muros1[7], new Vector2(0,7));
		//Esquina arriba izquierda
		Mazmorras.colocarImagen(sala[9,7],muros1[9], new Vector2(9,7));
		
	}
	
	private void generarPuertas(){
		//Puerta Abajo
		int rdm = Mathf.RoundToInt(Random.Range (1, 8));

		GameObject puertaDown = sala [rdm, 0];
		puertaDown.tag = "Door";
		puertaDown.name = "Door";
		Mazmorras.colocarImagen(puertaDown, muros1[6], new Vector2(rdm,0));
		puertaDown.transform.Rotate(Vector3.forward * -180);

		//Puerta Arriba
		rdm = Mathf.RoundToInt(Random.Range (1, 8));

		GameObject puertaUp = sala[rdm,7];
		puertaUp.tag = "Door";
		puertaUp.name = "Door";
		Mazmorras.colocarImagen(sala[rdm,7], muros1[6], new Vector2(rdm,7));


		//Puerta Derecha
		rdm = Mathf.RoundToInt(Random.Range (1, 6));

		GameObject puertaRight = sala[9,rdm];
		puertaRight.tag = "Door";
		puertaRight.name = "Door";
		Mazmorras.colocarImagen(puertaRight, muros1[6], new Vector2(9,rdm));
		puertaRight.transform.Rotate(Vector3.forward * -90);

		//Puerta Izquierda
		rdm = Mathf.RoundToInt(Random.Range (1, 6));

		GameObject puertaLeft = sala[0,rdm];
		puertaLeft.tag = "Door";
		puertaLeft.name = "Door";
		Mazmorras.colocarImagen(puertaLeft, muros1[6], new Vector2(0,rdm));
		puertaLeft.transform.Rotate(Vector3.forward * -270);
				
	}
	
	private void generarMuros(){
		//Muros
		for (int x = 1; x <= 8; x++) {
			//Muro Abajo
			if (!sala[x,0].CompareTag("Door")){
				GameObject muroDown = sala[x,0];
				muroDown.tag = "Wall";
				muroDown.name = "Wall";
				Mazmorras.colocarImagen(muroDown, muros1[1], new Vector2(x,0));
			}
			//Muro Arriba
			if (!sala[x,7].CompareTag("Door")){
				GameObject muroUp = sala[x,7];
				muroUp.tag = "Wall";
				muroUp.name = "Wall";
				Mazmorras.colocarImagen(muroUp, muros1[8], new Vector2(x,7));
			}
		}
		
		for (int y = 1; y <= 6; y++) {
			if (!sala[0,y].CompareTag("Door")){
				GameObject muroLeft = sala[0,y];
				muroLeft.tag = "Wall";
				muroLeft.name = "Wall";
				Mazmorras.colocarImagen(muroLeft, muros1[3], new Vector2(0,y));
			}
			if (!sala[9,y].CompareTag("Door")){
				GameObject muroRight = sala[9,y];
				muroRight.tag = "Wall";
				muroRight.name = "Wall";
				Mazmorras.colocarImagen(muroRight, muros1[4], new Vector2(9,y));

			}
		}
	}
	//Rocas
	private void generarRocas(){
		int numRocas = Mathf.RoundToInt(Random.Range (0, 7));

		while (numRocas != 0) {
			int xRoca = Mathf.RoundToInt(Random.Range (1, 8));
			int yRoca = Mathf.RoundToInt(Random.Range (1, 6));

			if (!((sala[xRoca+1, yRoca].CompareTag("Door")) || (sala[xRoca, yRoca+1].CompareTag("Door")) || (sala[xRoca-1, yRoca].CompareTag("Door")) || (sala[xRoca, yRoca-1].CompareTag("Door")) )){
					GameObject rock =  sala[xRoca,yRoca];
					rock.name = "Rock";
					rock.tag = "Rock";
					Mazmorras.colocarImagen(rock, suelo1[2], new Vector2(xRoca,yRoca));
					numRocas = numRocas -1;
			}
		}
	}

	//Agujeros
	private void generarAgujeros(){
		int numHole = Mathf.RoundToInt(Random.Range (0, 2));

		while (numHole != 0) {
			int xHole = Mathf.RoundToInt(Random.Range (1, 8));
			int yHole = Mathf.RoundToInt(Random.Range (1, 6));
			
			if (!((sala[xHole+1, yHole].CompareTag("Door")) || (sala[xHole+1, yHole].CompareTag("Rock")) || (sala[xHole, yHole+1].CompareTag("Door")) || (sala[xHole-1, yHole].CompareTag("Door")) || (sala[xHole, yHole-1].CompareTag("Door")) || (sala[xHole+1, yHole].CompareTag("Wall")) || (sala[xHole+1, yHole-1].CompareTag("Door")) || (sala[xHole+1, yHole+1].CompareTag("Door")) )){
				if (!sala[xHole+2, yHole].CompareTag("Door")){
					GameObject hole =  sala[xHole,yHole];
					GameObject hole2 =  sala[xHole+1,yHole];
					hole.name = "Hole";
					hole.tag = "Hole";
					hole2.tag = "Hole";
					Mazmorras.colocarImagen(hole, suelo1[0], new Vector2(xHole,yHole));
					Mazmorras.colocarImagen(hole2, suelo1[1], new Vector2(xHole+1,yHole));
					numHole = numHole -1;
				}
			}
		}
	}
	//Suelo
	private void generarSuelo(){
		for (int x = 1; x <= 8; x++){
			for (int y = 1; y <= 6; y++){
				if (!(sala[x,y].CompareTag("Rock") || (sala[x,y].CompareTag("Hole")) )){
					GameObject floor = sala[x,y];
					floor.tag = "Floor";
					floor.name = "Floor";
					Mazmorras.colocarImagen (floor, suelo1[3], new Vector2(x,y));

				}
			}
		}
	}
	
	
	
	
}