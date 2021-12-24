using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class Rename : EditorWindow
{
	private Editor editor;
	private string strName = null;
	private Vector3 gameObjectPosition = new Vector3();

    [SerializeField]
    private List<GameObject> gameObjectsList = new List<GameObject>();

    [MenuItem("Tools/Rename")]
    private static void Open()
	{
		Rename win = GetWindow<Rename>();
        win.titleContent = new GUIContent("Rename Tool");
        win.Show();
	}
	private void OnEnable()
	{
	}
	private void OnDisable()
	{
		gameObjectPosition = Vector3.zero;
	}
	private void OnGUI()
	{
		if (!editor) { editor = Editor.CreateEditor(this); }
	    if(editor) { editor.OnInspectorGUI(); }

		GUILayout.BeginVertical("Box");

		GUILayout.Label("Change Name");
		strName = EditorGUILayout.TextField(strName, GUILayout.ExpandWidth(true));



		if(GUILayout.Button("Change"))
		{
			if(gameObjectsList.Count ==0)
			{
				Debug.LogError("Empty List");
			}
			foreach (var elem in gameObjectsList)
			{
				gameObjectPosition = elem.gameObject.transform.position;
				elem.name = strName + gameObjectPosition;
			}
		}
		GUILayout.EndVertical();
	}
}
[CustomEditor(typeof(Rename), true)]
public class RenameEditor :Editor
{
	public override void OnInspectorGUI()
	{
		var list = serializedObject.FindProperty("gameObjectsList");
		EditorGUILayout.PropertyField(list, new GUIContent("Objects List"), true);

		serializedObject.ApplyModifiedProperties();
	if(GUILayout.Button("Clear"))
		{
			list.arraySize = 0;
		}
	}

}
