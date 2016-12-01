using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public GameObject playerPre;
    public GameObject objScroll;
    public static int score;
    public static float life = 3;

    bool resurrection = false;
    [HideInInspector]public GameObject objPlayer;

    // Use this for initialization
    void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        objScroll = GameObject.Find("scroll");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CreatePlayer()
    { 
            Vector3 ScrollObj = objScroll.transform.position;
            transform.position = ScrollObj;
            objPlayer = (GameObject)Instantiate(playerPre, this.transform.position, Quaternion.identity);
            objPlayer.transform.parent = objScroll.transform;
    }
    public void Resurrection()
    {
        if (life >= 0)
        {
            Invoke("CreatePlayer", 1f);
        }
    }
}
