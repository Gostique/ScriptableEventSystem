using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEventSystem
{
    public class GameEventListener : MonoBehaviour
    {

        public List<GameEvent> m_gameEvents = new List<GameEvent>();
        public List<UnityEvent> m_unityEvents = new List<UnityEvent>();

        private void OnEnable()
        {
            foreach (GameEvent gameEvent in m_gameEvents)
            {
                gameEvent.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            foreach (GameEvent gameEvent in m_gameEvents)
            {
                gameEvent.UnregisterListener(this);
            }
        }

        public void OnEventRaised(GameEvent gameEvent)
        {
            m_unityEvents[m_gameEvents.IndexOf(gameEvent)].Invoke();
            /*if (m_events.ContainsKey(gameEvent))
            {
                m_events[gameEvent].Invoke();
            }*/
        }

    }
}