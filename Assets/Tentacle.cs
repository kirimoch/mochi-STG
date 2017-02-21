using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : StageObject {

    public float hitPoint;
    public float score = 100f;

    ScoreText scoreText;
    GameManager gm;
    GameObject objPlayer;

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (objPlayer == null)
        {
            objPlayer = gm.player;
        }
        if(isActive)
        {

        }

    }
}
