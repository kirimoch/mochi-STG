using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MyiTweenPath : iTweenPath
{
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
