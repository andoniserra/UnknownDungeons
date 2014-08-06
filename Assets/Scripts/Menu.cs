using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

		/*
		GameObject kek = new GameObject();
		kek.AddComponent(typeof(SpriteRenderer));
		SpriteRenderer imagen2 = kek.GetComponent<SpriteRenderer>();
		imagen2.sprite = Simbolos.Cat;
		imagen2.sortingLayerName = "Background";
		imagen2.transform.position = Simbolos.colocar8x8(2,17);
		*/

		//Simbolos.caja(new Vector2(0,0), new Vector2(19,2));
		//Simbolos.print("abcdefghijklmnopqrstuvwxyz0123456789!?", new Vector2(0,17));

		Simbolos.caja(new Vector2 (0,0), new Vector2(19,2),"hola caracola!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
		Debug.Log("herpaderpa");
		Sprite[] letras = Resources.LoadAll<Sprite>("Sprites/tipografia");

		foreach( Sprite sp in letras){
			Debug.Log(sp.name);
		}
		Debug.Log("herpadorp");
	 */
}
