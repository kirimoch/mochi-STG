using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MyiTweenPath))]
public class MyiTweenPathEditor : Editor
{
    MyiTweenPath _target;
    public static int count = 0;

    List<Vector3> nodesByPositon = new List<Vector3>();

    void Reset()
    {

    }

    void OnEnable()
    {
        _target = (MyiTweenPath)target;

        //lock in a default path name:
        if (!_target.initialized)
        {
            _target.initialized = true;
            _target.pathName = "My New Path " + ++count;
            _target.initialName = _target.pathName;
            _target.nodes[0] = _target.transform.position;
            _target.nodes[1] = _target.transform.position + Vector3.left * 35;
        }
    }

    // The OnInspectorGUI() functuon is:
    // Copyright(c) 2011 Bob Berkebile(pixelplacment)
    public override void OnInspectorGUI()
    {
        //path name:
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Path Name");
        _target.pathName = EditorGUILayout.TextField(_target.pathName);
        EditorGUILayout.EndHorizontal();

        if (_target.pathName == "")
        {
            _target.pathName = _target.initialName;
        }

        //path color:
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Path Color");
        _target.pathColor = EditorGUILayout.ColorField(_target.pathColor);
        EditorGUILayout.EndHorizontal();

        //exploration segment count control:
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Node Count");
        _target.nodeCount = Mathf.Clamp(EditorGUILayout.IntSlider(_target.nodeCount, 0, 10), 2, 100);
        EditorGUILayout.EndHorizontal();

        //add node?
        if (_target.nodeCount > _target.nodes.Count)
        {
            Vector3 last = _target.nodes[_target.nodes.Count - 1];
            for (int i = 0; i < _target.nodeCount - _target.nodes.Count; i++)
            {
                _target.nodes.Add(new Vector3(last.x, last.y, last.z));
            }

            if(_target.isLockedAllNodesPosition)
            {
                GetDiffNodesFromObject();
            }
        }

        //remove node?
        if (_target.nodeCount < _target.nodes.Count)
        {
            if (EditorUtility.DisplayDialog("Remove path node?", "Shortening the node list will permantently destory parts of your path. This operation cannot be undone.", "OK", "Cancel"))
            {
                int removeCount = _target.nodes.Count - _target.nodeCount;
                _target.nodes.RemoveRange(_target.nodes.Count - removeCount, removeCount);
            }
            else
            {
                _target.nodeCount = _target.nodes.Count;
            }
        }

        //node display:
        EditorGUI.indentLevel = 4;
        for (int i = 0; i < _target.nodes.Count; i++)
        {
            _target.nodes[i] = EditorGUILayout.Vector3Field("Node " + (i + 1), _target.nodes[i]);
        }

        //update and redraw:
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }

        //nodes value reset button
        GUILayout.Space(10);
        if (GUILayout.Button("Nodes reset", GUILayout.Width(100)))
        {
            Undo.RecordObject(_target, "Reset nodes");
            _target.nodes.Clear();
            _target.nodes.Add(_target.transform.position);
            _target.nodes.Add(_target.transform.position + Vector3.left * 35);
            _target.nodeCount = 2;

            if (_target.isLockedAllNodesPosition)
            {
                GetDiffNodesFromObject();
            }
            EditorUtility.SetDirty(_target);
        }

        if (EditorGUILayout.ToggleLeft("Lock Begin Node", _target.isLockedBeginNodesPosition))
        {
            if (_target.isLockedBeginNodesPosition)
            {
                if (!EditorApplication.isPlaying)
                {
                    _target.nodes[0] = _target.transform.position;
                }
            }
            else
            {
                _target.isLockedBeginNodesPosition = true;
                EditorUtility.SetDirty(_target);
            }
        }
        else if(_target.isLockedBeginNodesPosition)
        {
            _target.isLockedBeginNodesPosition = false;
        }

        if (EditorGUILayout.ToggleLeft("Lock All Nodes", _target.isLockedAllNodesPosition, GUILayout.MinWidth(100)))
        {
            if (_target.isLockedAllNodesPosition)
            {
                if (!EditorApplication.isPlaying)
                {
                    Debug.Log(_target.nodes.Count + " " + nodesByPositon.Count);
                    if (_target.nodes.Count != nodesByPositon.Count)
                    {
                        GetDiffNodesFromObject();
                    }
                    for (int i = 0; i < _target.nodes.Count; i++)
                    {
                        _target.nodes[i] = _target.transform.position + nodesByPositon[i];
                    }
                }
            }
            else
            {
                _target.isLockedAllNodesPosition = true;
                GetDiffNodesFromObject();
            }
        }
        else if (_target.isLockedAllNodesPosition)
        {
            _target.isLockedAllNodesPosition = false;
        }
    }

    void GetDiffNodesFromObject()
    {
        nodesByPositon.Clear();
        for (int i = 0; i < _target.nodes.Count; i++)
        {
            nodesByPositon.Add(_target.nodes[i] - _target.transform.position);
        }
    }

    // The OnSceneGUI() functuon is:
    // Copyright(c) 2011 Bob Berkebile(pixelplacment)
    void OnSceneGUI()
    {
        if (_target.enabled)
        { // dkoontz
            if (_target.nodes.Count > 0)
            {
                //allow path adjustment undo:
                //	Undo.SetSnapshotTarget(_target,"Adjust iTween Path");

                //path begin and end labels:
                Handles.Label(_target.nodes[0], "'" + _target.pathName + "' Begin");
                Handles.Label(_target.nodes[_target.nodes.Count - 1], "'" + _target.pathName + "' End");

                //node handle display:
                for (int i = 1; i < _target.nodes.Count; i++)
                {
                    _target.nodes[i] = Handles.PositionHandle(_target.nodes[i], Quaternion.identity);
                }
            }
        } // dkoontz
    }
}