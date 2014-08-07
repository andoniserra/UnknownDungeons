using UnityEngine;
using System.Collections;

public class SoundCommander : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire"))
		{
			SoundHelper.PlayArrowShot();
		}
		
		// Mientras este el boton pulsado, defendemos
		if (Input.GetButtonDown("Defend"))
		{
			SoundHelper.PlayPlayerHit();
		}
		
		// Cuando se suelta el boton, dejamos de defender
		if (Input.GetButtonUp("Defend"))
		{
			SoundHelper.PlayMobHit();
		}
		
		// Cambiar de arma
		if (Input.GetButtonDown("Select"))
		{
			SoundHelper.PlayMagicBall();
		}
	}
}
