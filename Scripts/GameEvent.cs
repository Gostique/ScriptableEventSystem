﻿using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEventSystem
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "ScriptableEventSystem/Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(this);
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}