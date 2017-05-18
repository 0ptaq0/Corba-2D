using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;
    private AudioSource source;
    public float volume = 0.7f;
    public float pitch = 1;
    public bool loop = false;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play() {

        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.Play();
  
    }

    public void Stop() {
        source.Stop();
    }
}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    void Awake()
    {
        if (instance != null)
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else { 
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    [SerializeField]
    Sound[] sounds;
	
	void Start () {
        for (int i = 0; i < sounds.Length; i++) {
            GameObject _go = new GameObject("Sound" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].setSource(_go.AddComponent<AudioSource>());
        }
        instance.PlaySound("Background");
	}

    public void PlaySound(string _name) {

        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == _name) {
                sounds[i].Play();
            }
        }
    }

    public void StopSound(string _name) {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
            }
        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}
