using UnityEngine;
using System.Collections;

public static class Simbolos {

	#region Simbolos y letras

	public static Sprite Ex {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[38];}
	}
	public static Sprite Coin {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[39];}
	}
	public static Sprite Coin2 {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[40];}
	}
	public static Sprite Heart {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[41];}
	}
	public static Sprite HeartMini {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[42];}
	}
	public static Sprite Mush {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[43];}
	}
	public static Sprite Copyright {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[44];}
	}
	public static Sprite Right {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[45];}
	}
	public static Sprite Left {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[46];}
	}
	public static Sprite Up {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[47];}
	}
	public static Sprite Down {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[48];}
	}
	public static Sprite BorderUpLeft {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[49];}
	}
	public static Sprite BorderUp {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[50];}
	}
	public static Sprite BorderUpRight {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[51];}
	}
	public static Sprite BorderLeft {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[52];}
	}
	public static Sprite BorderRight {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[53];}
	}
	public static Sprite BorderDownLeft {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[54];}
	}
	public static Sprite BorderDown {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[55];}
	}
	public static Sprite BorderDownRight {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[56];}
	}
	public static Sprite Cat {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[57];}
	}
	public static Sprite Blank {
		get{return Resources.LoadAll<Sprite>("Sprites/tipografia")[58];}
	}

	public static Sprite getLetra(char pLetra){

		int num;
		
		switch (pLetra){

			case 'a': num = 0; break;
			case 'b': num = 1; break;
			case 'c': num = 2; break;
			case 'd': num = 3; break;
			case 'e': num = 4; break;
			case 'f': num = 5; break;
			case 'g': num = 6; break;
			case 'h': num = 7; break;
			case 'i': num = 8; break;
			case 'j': num = 9; break;
			case 'k': num = 10; break;
			case 'l': num = 11; break;
			case 'm': num = 12; break;
			case 'n': num = 13; break;
			case 'o': num = 14; break;
			case 'p': num = 15; break;
			case 'q': num = 16; break;
			case 'r': num = 17; break;
			case 's': num = 18; break;
			case 't': num = 19; break;
			case 'u': num = 20; break;
			case 'v': num = 21; break;
			case 'w': num = 22; break;
			case 'x': num = 23; break;
			case 'y': num = 24; break;
			case 'z': num = 25; break;
			case '0': num = 26; break;
			case '1': num = 27; break;
			case '2': num = 28; break;
			case '3': num = 29; break;
			case '4': num = 30; break;
			case '5': num = 31; break;
			case '6': num = 32; break;
			case '7': num = 33; break;
			case '8': num = 34; break;
			case '9': num = 35; break;
			case '!': num = 36; break;
			case '?': num = 37; break;
			case ' ': num = 58; break;
			default: num = 37; break;
		}
		return Resources.LoadAll<Sprite>("Sprites/tipografia")[num];
	}

	#endregion 

	/// <summary>
	/// Escribe en las coordenadas indicadas el texto
	/// Si se llega al final de la pantalla salta a la siguiente linea
	/// </summary>
	/// <param name="pStr">String a escribir</param>
	/// <param name="pPos">Posicion en la que empezar a escribir</param>
	public static void print (string pStr, Vector2 pPos){
		Vector2 Posicion = pPos;

		for (int x = 0; x < pStr.Length; x++){
			if(Posicion.x > 19){
				Posicion = pPos - new Vector2(0,1);
			}
			colocarImagen(new GameObject(), getLetra(pStr.ToLower().ToCharArray()[x]), Posicion);
			Posicion += new Vector2(1,0);

		}
	}

	/// <summary>
	/// Escribe el texto indicado en unas coordenadas acotadas
	/// Si el texto supera el espacio de las coordenadas deja de escribirlo
	/// </summary>
	/// <param name="pStr">String a escribir.</param>
	/// <param name="pPos">Posicion inicial.</param>
	/// <param name="pPosMax">Posicion final.</param>
	public static void printAcot (string pStr, Vector2 pPos, Vector2 pPosMax){

		Vector2 Posicion = pPos;
		
		for (int x = 0; x < pStr.Length; x++){
			if(Posicion.x > pPosMax.x){
				Posicion = pPos - new Vector2(0,1);
			}
			if (Posicion.y < pPosMax.y){
				break;
			}
			colocarImagen(new GameObject(), getLetra(pStr.ToLower().ToCharArray()[x]), Posicion);
			Posicion += new Vector2(1,0);
			
		}
	}

	/// <summary>
	/// Dibuja una caja entre las coordenadas indicadas
	/// </summary>
	/// <param name="pPos1">Posicion inicia (abajo izquierda).</param>
	/// <param name="pPos2">Posicion final (arriba derecha).</param>
	public static void caja(Vector2 pPos1, Vector2 pPos2){
		//pintamos las esquinas
		colocarImagen(new GameObject(), Simbolos.BorderDownLeft, pPos1);
		colocarImagen(new GameObject(), Simbolos.BorderDownRight, new Vector2(pPos2.x, pPos1.y));
		colocarImagen(new GameObject(), Simbolos.BorderUpLeft, new Vector2(pPos1.x, pPos2.y));
		colocarImagen(new GameObject(), Simbolos.BorderUpRight, pPos2);

		//Raya abajo
		for(float x = pPos1.x + 1; x < pPos2.x; x++){
			colocarImagen(new GameObject(), Simbolos.BorderDown, new Vector2(x,pPos1.y));
		} 
		//Raya arriba
		for(float x = pPos1.x + 1; x < pPos2.x; x++){
			colocarImagen(new GameObject(), Simbolos.BorderUp, new Vector2(x,pPos2.y));
		} 
		//Raya Izquierda
		for(float y = pPos1.y + 1; y < pPos2.y; y++){
			colocarImagen(new GameObject(), Simbolos.BorderLeft, new Vector2(pPos1.x,y));
		} 
		//Raya Derecha
		for(float y = pPos1.y + 1; y < pPos2.y; y++){
			colocarImagen(new GameObject(), Simbolos.BorderRight, new Vector2(pPos2.x,y));
		} 
	}

	/// <summary>
	/// Dibuja una caja entre las coordenadas indicadas con texto en su interior
	/// </summary>
	/// <param name="pPos1">Posicion inicia (abajo izquierda).</param>
	/// <param name="pPos2">Posicion final (arriba derecha).</param>
	/// <param name="pStr">Texto a mostrar.</param>
	public static void caja(Vector2 pPos1, Vector2 pPos2, string pStr){

		Vector2 posCajaIni = pPos1;
		Vector2 posTextIni = new Vector2(pPos1.x +1, pPos2.y - 1);
		Vector2 posTextFin = new Vector2(pPos2.x-1, pPos1.y + 1);

		caja(posCajaIni, pPos2);
		printAcot(pStr, posTextIni, posTextFin);

	}

	/// <summary>
	/// Coloca la imagen en la posicion indicada.
	/// </summary>
	/// <param name="pGo">Objeto que contendra la imagen.</param>
	/// <param name="pSprite">Sprite.</param>
	/// <param name="pPos">P position.</param>
	public static void colocarImagen(GameObject pGo, Sprite pSprite, Vector2 pPos){
		colocarImagen(pGo,pSprite,pPos,"GUI",2);
	}

	public static void colocarImagen(GameObject pGo, Sprite pSprite, Vector2 pPos, string pNombreCapa, int pCapa){
		SpriteRenderer imagen = pGo.GetComponent<SpriteRenderer>();
		pGo.tag = "TempTile";
		if (imagen == null){
			pGo.AddComponent(typeof(SpriteRenderer));
			imagen = pGo.GetComponent<SpriteRenderer>();
		}
		//pGo.AddComponent(typeof(ColorChanger));
		imagen.sprite = pSprite;
		imagen.sortingLayerName = pNombreCapa;
		imagen.sortingOrder = pCapa;
		imagen.transform.position = pPos;
	}
	
}
