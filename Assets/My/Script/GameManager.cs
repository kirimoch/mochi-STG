using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject playerPre;
    public GameObject scroll;
    GameObject player;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreatePlayer(Vector3 lastPos, float waittime)
    {
        StartCoroutine(Wait(waittime, () =>
         {
             player = Instantiate(playerPre, Vector3.zero, Quaternion.identity) as GameObject;
             player.transform.parent = scroll.transform;
             player.transform.localPosition = lastPos;
         }));
    }

    IEnumerator Wait(float time, System.Action act)
    {
        yield return new WaitForSeconds(time);
        act();
    }
}
