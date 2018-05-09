using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Events
{
    public class NotificationManager : Singleton<NotificationManager>
    {
        Dictionary<string, List<Component>> listeners = new Dictionary<string, List<Component>>();

        void OnLevelWasLoaded()
        {
            RemoveRedundancies();
        }

        public void AddListener(Component sender, string eventName)
        {
            if (!listeners.ContainsKey(eventName))
            {
                listeners.Add(eventName, new List<Component>());
            }

            listeners[eventName].Add(sender);
        }

        public void PostNotification(Component sender, string eventName)
        {
            if (!listeners.ContainsKey(eventName))
                return;

            foreach (var listener in listeners[eventName])
            {
                listener.SendMessage(eventName, sender, SendMessageOptions.DontRequireReceiver);
            }
        }

        public void RemoveListener(Component sender, string eventName)
        {
            if (!listeners.ContainsKey(eventName))
                return;

            foreach (var listener in listeners[eventName])
            {
                if (listener.GetInstanceID() == sender.GetInstanceID())
                {
                    listeners[eventName].Remove(listener);
                }
            }
        }

        public void RemoveRedundancies()
        {
            Dictionary<string, List<Component>> temp = new Dictionary<string, List<Component>>();

            foreach (KeyValuePair<string, List<Component>> pair in listeners)
            {
                for (int i = pair.Value.Count - 1; i >= 0; i--)
                {
                    // if there's no components associated to certain event
                    if (pair.Value[i] == null)
                        pair.Value.RemoveAt(i);
                }

                if (pair.Value.Count > 0)
                {
                    temp.Add(pair.Key, pair.Value);
                }
            }

            listeners = temp;
        }

        public void ClearListeners()
        {
            listeners.Clear();
        }
    }
}