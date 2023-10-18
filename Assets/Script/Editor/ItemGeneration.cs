using UnityEngine;
using UnityEditor;
using System.CodeDom.Compiler;

[CustomEditor(typeof(Item))]
public class ItemGeneration : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Item item = (Item)target;
        if (GUILayout.Button("Reset amount"))
        {
            item.amount = 0;
        }
    }
}