using UnityEngine;
using System.Collections;

public class StageObjBase : MonoBehaviour{

    public float speed;
    public float iTweenDuration;
    public MoveType moveType;

    bool hasFound;
    bool hasItweenMoving;
    [HideInInspector]
    public bool isActive;

    public enum MoveType
    {
        straight,
        itween
    }

    protected void Move()
    {
        if (!isActive) return;

        switch (moveType)
        {
            case MoveType.straight:
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
                break;
            case MoveType.itween:
                if (hasItweenMoving) return;

                MyiTweenPath iPath = GetComponent<MyiTweenPath>();
                if (iPath == null) break;

                for (int i = 0; i < iPath.nodeCount; i++)
                {
                    iPath.nodes[i] = (iPath.nodes[i] - transform.position) + transform.localPosition;
                }
                string pathName = iPath.pathName;

                hasItweenMoving = true;
                iTween.MoveTo(gameObject, iTween.Hash("movetopath", false,
                                                      "path", MyiTweenPath.GetPath(pathName),
                                                      "time", iTweenDuration,
                                                      "easetype", iTween.EaseType.easeOutSine,
                                                      "isLocal", true));
                break;
        }

    }
}
