using UnityEngine;
using System.Collections;

public static class Mazmorras {
	
	private static Vector3 colocar16x16(float posX, float posY){
		return new Vector3((-72 + (posX*16)) * GLOBALS.UNITS_TO_PIXELS,(-48 + (posY * 16)) * GLOBALS.UNITS_TO_PIXELS);
	}
	
	/// <summary>
	/// Coloca la imagen en la posicion indicada.
	/// </summary>
	/// <param name="pGo">Objeto que contendra la imagen.</param>
	/// <param name="pSprite">Sprite.</param>
	/// <param name="pPos">P position.</param>
	public static void colocarImagen(GameObject pGo, Sprite pSprite, Vector2 pPos){
		SpriteRenderer imagen = pGo.GetComponent<SpriteRenderer>();
		if (imagen == null){
			pGo.AddComponent(typeof(SpriteRenderer));
			imagen = pGo.GetComponent<SpriteRenderer>();
		}
		imagen.sprite = pSprite;
		//imagen.sortingLayerName = "Background";
		imagen.transform.position = Mazmorras.colocar16x16(pPos.x,pPos.y);

		if (pGo.CompareTag ("Wall") || pGo.CompareTag ("Rock") || pGo.CompareTag("Door")) {
					pGo.AddComponent (typeof(BoxCollider2D));
					imagen.sortingLayerName = "Walls";
		} else if (pGo.CompareTag ("Door")) {
				pGo.AddComponent (typeof(Door));
		}

		pGo.AddComponent (typeof(ColorChanger));
	}

	
	
	
	
}