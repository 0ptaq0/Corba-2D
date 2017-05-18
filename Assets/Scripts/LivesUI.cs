using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    private Text lives;


	// Use this for initialization
	void Start () {
        lives = GetComponent<Text>();		
	}
	
	// Update is called once per frame
	void Update () {
        lives.text = "LIVES: " + GameMaster.LivesLeft;
	}
}
