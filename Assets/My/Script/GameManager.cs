using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject playerPre;
    public GameObject scroll;
    public static int score;
    public static float life = 3;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatePlayer(Vector3 lastPos, float waittime)
    {
        life--;
        if (life > 0)
        {
            player = Instantiate(playerPre, Vector3.zero, Quaternion.identity) as GameObject;
            player.transform.parent = scroll.transform;
            lastPos.y = 0;
            player.transform.localPosition = lastPos;
            player.gameObject.layer = LayerMask.NameToLayer("invinciblePlayer");
        }
    }
}
