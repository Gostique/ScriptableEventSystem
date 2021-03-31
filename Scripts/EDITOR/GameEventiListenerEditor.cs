using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace ScriptableEventSystem
{
    [CustomEditor(typeof(GameEventListener))]
    public class GameEventListenerEditor : Editor
    {

        GameEventListener listener;
        int _eventIndex;

        private void OnEnable()
        {
            listener = (GameEventListener)target;
            _eventIndex = -1;
        }

        public override void OnInspectorGUI()
        {

            #region Navigation bar
            EditorGUILayout.BeginHorizontal();


            // button <
            EditorGUI.BeginDisabledGroup(_eventIndex < 0);
            if (GUILayout.Button("<", GUILayout.Width(20)))
            {
                _eventIndex -= 1;
            }
            EditorGUI.EndDisabledGroup();
            // Label
            GUILayout.FlexibleSpace();
            GUILayout.Label(_eventIndex == -1 ? "Main page" : listener.m_gameEvents[_eventIndex].name);
            GUILayout.FlexibleSpace();
            // button >
            EditorGUI.BeginDisabledGroup(!(listener.m_gameEvents.Count > 0 && _eventIndex < listener.m_gameEvents.Count - 1));
            if (GUILayout.Button(">", GUILayout.Width(20)))
            {
                _eventIndex += 1;
            }
            EditorGUI.EndDisabledGroup();
            // button Go to main
            EditorGUI.BeginDisabledGroup(_eventIndex == -1);
            if (GUILayout.Button("Go to main", GUILayout.Width(80)))
            {
                _eventIndex = -1;
            }
            EditorGUI.EndDisabledGroup();


            EditorGUILayout.EndHorizontal();
            #endregion

            #region Main page
            if (_eventIndex == -1)
            {
                // Add field
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Add event :", GUILayout.Width(66));
                Object add = EditorGUILayout.ObjectField(null, typeof(GameEvent), false);
                if (add is GameEvent && !listener.m_gameEvents.Contains((GameEvent)add))
                {
                    listener.m_gameEvents.Add((GameEvent)add);
                    listener.m_unityEvents.Add(new UnityEvent());
                }
                EditorGUILayout.EndHorizontal();

                // List of event
                if (listener.m_gameEvents.Count == 0)
                {
                    GUILayout.Label("No event setup.");
                }
                else
                {
                    for (int i = 0; i < listener.m_gameEvents.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        // Event button
                        if (GUILayout.Button(listener.m_gameEvents[i].name))
                        {
                            _eventIndex = i;
                            return;
                        }
                        // Call button
                        if (GUILayout.Button("Call", GUILayout.Width(45)))
                        {
                            listener.m_unityEvents[i].Invoke();
                            return;
                        }
                        // Remove buttonn
                        if (GUILayout.Button("X", GUILayout.Width(30)))
                        {
                            listener.m_unityEvents.RemoveAt(i);
                            listener.m_gameEvents.RemoveAt(i);
                            return;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            #endregion

            #region Event page
            else
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_unityEvents").GetArrayElementAtIndex(_eventIndex), new GUIContent("Response"));
                if (GUILayout.Button($"Call {listener.m_gameEvents[_eventIndex].name}"))
                {
                    listener.m_unityEvents[_eventIndex].Invoke();
                }
                serializedObject.ApplyModifiedProperties();
            }
            #endregion

        }
    }
}