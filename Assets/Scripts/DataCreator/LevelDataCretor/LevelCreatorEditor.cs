
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(LevelDataCreator))]
public class IncomeDataCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelDataCreator levelCreator = (LevelDataCreator)target;

        GUILayout.Space(25f);

        EditorGUI.BeginChangeCheck();

        GUILayout.Label("LevelData");

        levelCreator.TempLevelData = EditorGUILayout.ObjectField("", levelCreator.TempLevelData, typeof(LevelData), true) as LevelData;

        GUILayout.Label("Name Of Level To Be Created");
        levelCreator.CreatedLevelName = EditorGUILayout.TextField("Name Of Level", levelCreator.CreatedLevelName);

        GUILayout.Space(25f);

        if (GUILayout.Button("Save Level"))
        {
            levelCreator.SaveLevel();
        }
        if (GUILayout.Button("Create Random Level"))
        {
            levelCreator.CreateRandomLevel();
        }
        if (GUILayout.Button("Load Level Data"))
        {
            levelCreator.LoadData();
        }
        if (GUILayout.Button("Clear Scene"))
        {
            levelCreator.ClearScene();
        }
    }

}
#endif

