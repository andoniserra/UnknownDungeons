using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour 
{
	public int m_coinsPerCycle = 5;
	public float m_secondsCycle = 2f;
	public Transform m_coin;
	public float m_coinLifeSpan = 3f;

	private float m_timer = 0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Clock())
		{
			for (int i = 1; i<= m_coinsPerCycle; i++)
			{
				StartCoroutine("WaitAndCoin",m_secondsCycle);
			}
		}
	}


	private bool Clock ()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_secondsCycle)
		{
			m_timer = 0f;
			return true;
		}
		else
		{
			return false;
		}
	}


	IEnumerator WaitAndCoin ( float p_time )
	{
		float randomOffsetTime = Random.Range(0f,p_time);
		yield return new WaitForSeconds(randomOffsetTime);

		Camera cam = Camera.main;
		float sizeX = cam.orthographicSize;
		float sizeY = sizeX * cam.aspect;

		//float sizeX = Screen.size.x / 2; 
		//float sizeY = Screen.size.y / 2;
		float randomOffsetX = Random.Range(-sizeX,sizeX);
		float randomOffsetY = Random.Range(-sizeY,sizeY);
		Vector3 coinPosition = new Vector3 (
			transform.position.x + randomOffsetX,
			transform.position.y + randomOffsetY,
			0);
		Transform coin = Instantiate(m_coin,coinPosition,transform.rotation) as Transform;
		Destroy(coin.gameObject, m_coinLifeSpan);
	}
}
