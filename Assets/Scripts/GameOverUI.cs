using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour {


    public void Retry() {
        SceneManager.LoadScene(1);
        GameMaster.Restart();
    }

    public void Menu() {
        GameMaster.Menu();
        SceneManager.LoadScene(0);
        AudioManager.instance.StopSound("GameOver");
        AudioManager.instance.PlaySound("Background");
    }

    public void Quit() {
        Application.Quit();
    }

}
