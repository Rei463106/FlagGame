using System;
using System.Linq;
using UnityEditor;

/// <summary>
/// インスペクターにInterfaceを出す
/// </summary>
[CustomEditor(typeof(ForInterfaceEdit))]
public class ForInterfaceEdit : Editor
{
    public override void OnInspectorGUI()
    {
        var interfaces = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsInterface)
            .ToArray();

        foreach (var i in interfaces)
        {
            EditorGUILayout.LabelField(i.Name);
        }

        DrawDefaultInspector();
    }
}