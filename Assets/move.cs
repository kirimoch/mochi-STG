using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

    public float speed = 1.0f;

    string pathName;
    iTweenPath iTpathCash;
    bool isRunning;

    // Use this for initialization
    void OnEnable ()
    {
        CreatePath();
	}

    void CreatePath()
    {
        pathName = gameObject.name + Time.time.ToString();

        var iTPath = gameObject.AddComponent<iTweenPath>();

        Vector2 playerPos = transform.position;
        iTPath.nodes.Clear();
        iTPath.nodes.Add(playerPos + new Vector2(-1.8f, -1.9f) * 2);
        iTPath.nodes.Add(playerPos + new Vector2(-2.797019f, -5f) * 2);
        iTPath.nodes.Add(playerPos + new Vector2(-1.8f, -8.1f) * 2);
        iTPath.nodes.Add(playerPos + new Vector2(0f, -10f) * 2);
        iTPath.nodes.Add(playerPos);
        iTPath.pathName = pathName;
        iTPath.nodeCount = iTPath.nodes.Count;
        iTPath.initialized = true;
        iTweenPath.paths.Add(pathName.ToLower(), iTPath);
        iTpathCash = iTPath;
    }

    void MovePath()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName),
                                              "time", 3,
                                              "oncomplete", "FinishMove",
                                              "easetype", iTween.EaseType.easeOutSine));
        isRunning = true;
    }

    void FinishMove()
    {
        isRunning = false;
        if(iTweenPath.paths.ContainsKey(pathName.ToLower()))
        {
            iTweenPath.paths.Remove(pathName);
            Destroy(iTpathCash);
        }
        CreatePath();
    }

    void Start()
    {
        MovePath();
    }
	
	// Update is called once per frame
	void Update () {
	    if(!isRunning)
        {
            MovePath();
        }
	}
}
