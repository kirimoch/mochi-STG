using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyiTweenPath : iTweenPath {
    //相対移動するオブジェクトの位置にpathの起点を置くことを推奨
    void Reset()
    {
        initialized = true;
        //コンポーネントのアタッチ時にオブジェクトの位置にpathの起点を持ってくる
        nodes[0] = transform.position;
        //画面の幅が40なのでpathの終点を40左に作る。pathを作るときはこれを基準にするといい
        nodes[1] = transform.position + Vector3.left * 30;
    }

    void OnEnable()
    {
        //https://goo.gl/Scrlk2 参考
        if (!paths.ContainsKey(pathName))
        {
            //paths.Add(pathName.ToLower(),this); //org
            paths[pathName.ToLower()] = this;
        }
    }
}
