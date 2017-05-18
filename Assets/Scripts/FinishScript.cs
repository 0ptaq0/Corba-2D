using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour {

    public static int finished = 0;
    public static GameMaster gm;
	// Use this for initialization
	void Start () {
        gm = (GameMaster)FindObjectOfType(typeof(GameMaster));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D _colInfo)
    {
        Player _player = _colInfo.GetComponent<Player>();
        if (_player != null)
        {

            if (SceneManager.GetActiveScene().name == "level3")
            {
                StartCoroutine(gm.Victory());
            }
            else
            {
                StartCoroutine(gm.LvlComplete());
            }
        }
    }

}
