﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class MyiTweenPath : iTweenPath {

    [NonSerialized] public bool isLockedBeginNodesPosition;
    [NonSerialized] public bool isLockedAllNodesPosition;

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
