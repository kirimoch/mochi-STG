using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{

    public float hitPoint;
    public float TentacleLength;
    public float distance;
    public float score = 100f;


    GameObject objPlayer;
    GameManager gm;

    public GameObject[] ioints;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        objPlayer = gm.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (objPlayer != null)
        {
            ioints[0].transform.LookAt(gm.player.transform);
            TentacleLength = Vector3.Distance(ioints[0].transform.position, ioints[ioints.Length - 1].transform.position);
            distance = Vector3.Distance(ioints[ioints.Length - 1].transform.position, objPlayer.transform.position);

            if (distance < 10)
            {
                if (TentacleLength < 10)
                {
                    ioints[0].transform.Translate(Vector3.forward * 5 * Time.deltaTime);
                }
            }
            else if (TentacleLength > 0)
            {
                ioints[0].transform.LookAt(ioints[ioints.Length - 1].transform.position);
                ioints[0].transform.Translate(Vector3.forward * 5 * Time.deltaTime);
            }

            for (int i = 2; i < ioints.Length; i++)
            {
                ioints[i - 1].transform.position = (ioints[i - 2].transform.position + ioints[i].transform.position) / 2;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("nnn");
        if (col.gameObject.tag == "bullet")
        {
            hitPoint = hitPoint - Player.power;
        }
        if (hitPoint <= 0)
        {
            ScoreText.totalScore = score + ScoreText.totalScore;
            Destroy(gameObject);

        }
    }
}
