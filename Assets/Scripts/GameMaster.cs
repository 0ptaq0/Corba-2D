using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	void Awake () {
        if (gm != null)
        {
            if (gm != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this);
        }
    }
    public GameObject GameoverUI;
    public GameObject LivesUI;
    public GameObject VictoryUI;
    public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 3.5f;
	public Transform spawnPrefab;
	public CameraShake cameraShake;
    public static int LivesLeft = 3;

    void Start(){
		if (cameraShake == null)
			Debug.LogError ("No cameraShake referenced in GM");
        Time.timeScale = 1;
        LivesLeft = 3;
}

	public IEnumerator _RespawnPlayer () {

        AudioManager.instance.PlaySound("Respawn");
        
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;
		Destroy(clone, 3f);
	}

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        LivesLeft--;
        if (LivesLeft == 0)
        {
            gm.StartCoroutine(gm.GameOver());
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }


	public static void KillEnemy (Enemy enemy){
		gm._KillEnemy (enemy);
	}

	public void _KillEnemy(Enemy _enemy){
        AudioManager.instance.PlaySound("EnemyDestroyed");
        GameObject _clone = Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
		Destroy (_clone, 4f);
		cameraShake.Shake (_enemy.shakeAmt, _enemy.shakeLen);
		Destroy (_enemy.gameObject);
	}

    public IEnumerator GameOver() {
        LivesUI.SetActive(false);
        GameoverUI.SetActive(true);
        AudioManager.instance.StopSound("Background");
        AudioManager.instance.PlaySound("GameOver");
        yield return new WaitForSeconds(2);
        
    }

    public IEnumerator Victory() {
        LivesUI.SetActive(false);
        VictoryUI.SetActive(true);
        AudioManager.instance.StopSound("Background");
        AudioManager.instance.PlaySound("Victory");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject clone = Instantiate(spawnPrefab, player.transform.position, player.transform.rotation).gameObject;
        Destroy(clone, 3f);
        Destroy(player.gameObject);
        yield return new WaitForSeconds(2);

    }

    public IEnumerator LvlComplete() {
        AudioManager.instance.PlaySound("LevelComplete");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject clone = Instantiate(spawnPrefab, player.transform.position, player.transform.rotation).gameObject;
        Destroy(player);
        Destroy(clone, 3f);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Restart() {
        gm.LivesUI.SetActive(true);
        gm.GameoverUI.SetActive(false);
        AudioManager.instance.PlaySound("Background");
        AudioManager.instance.StopSound("GameOver");
        LivesLeft = 3;
    }

    public static void Menu() {
        AudioManager.instance.StopSound("Victory");
        AudioManager.instance.PlaySound("Background");
        gm.VictoryUI.SetActive(false);
        gm.LivesUI.SetActive(false);
        gm.GameoverUI.SetActive(false);
    }
}