﻿using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefaultAsset))]
public class IconEditor : Editor
{
    [SerializeField]
    private Texture2D icon;

    public override void OnInspectorGUI()
    {
        GUI.enabled = true;
        icon = (Texture2D)EditorGUILayout.ObjectField("Icon Texture", icon, typeof(Texture2D), false);

        if (GUILayout.Button("Set Icons for child script assets"))
        {
            Object[] selectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            for (int i = 0; i < selectedAsset.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(selectedAsset[i]);
                if (path.Contains(".cs"))
                {
                    SetIcon(selectedAsset[i], icon);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        GUI.enabled = false;
    }

    private static void SetIcon(Object selectedObject, Texture2D texture)
    {
        typeof(EditorGUIUtility).GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, new object[] { selectedObject, texture });
    }
}