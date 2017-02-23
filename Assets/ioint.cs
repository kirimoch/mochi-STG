using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ioint : MonoBehaviour {

    public float TentacleLength;
    public float distance;

    GameObject objPlayer;
    GameManager gm;

    public GameObject[] ioints;

    // Use this for initialization
    void Start () {
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
                if (TentacleLength < 5)
                {
                    ioints[0].transform.Translate(Vector3.forward * 5 * Time.deltaTime);
                }
            }
            else if(TentacleLength > 0)
            {
                ioints[0].transform.Translate(Vector3.back  * Time.deltaTime);
            } 

            for (int i = 2; i < ioints.Length; i++)
            {
                ioints[i -1].transform.position = (ioints[i - 2].transform.position + ioints[i].transform.position) / 2; 
            }
        }
    }
}
