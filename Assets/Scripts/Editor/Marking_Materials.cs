using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mark))]
public class Marking_Materials : Editor
{
   public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		Mark mark = (Mark)target;
		if (GUILayout.Button("Get materials"))
		{
			mark.Get();
		}
	}
}
