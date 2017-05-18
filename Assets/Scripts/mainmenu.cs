using UnityEngine;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour {

    void Start() {
        if (GameObject.FindGameObjectWithTag("GM")) {
            Destroy(GameObject.FindGameObjectWithTag("GM"));
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
