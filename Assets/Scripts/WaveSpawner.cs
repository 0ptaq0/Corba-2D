using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {
	public enum SpawnState {WAITING, SPAWNED};
    public Transform _sp;
    [System.Serializable]
	public class Wave
	{
		public Transform enemy;
		public int count;
		public float rate;
		public float distanceX;
        public float distanceY;
    }

	public Wave wave;

	private SpawnState state = SpawnState.WAITING;
	public SpawnState State
	{
		get { return state; }
	}

	void Start()
	{
        
	}

	void Update()
	{
		
		if (state == SpawnState.WAITING && _sp.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x <= wave.distanceX
            && _sp.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y <= wave.distanceY)
		{
			state = SpawnState.SPAWNED;
			StartCoroutine(SpawnWave (wave));
		}
		else
		{
			return;
		}
			
	}
				

	IEnumerator SpawnWave(Wave _wave)
	{
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}
		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}
}
