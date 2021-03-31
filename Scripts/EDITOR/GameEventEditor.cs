using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ScriptableEventSystem
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button($"Raise {target.name}."))
            {
                ((GameEvent)target).Raise();
            }
        }

    }
}