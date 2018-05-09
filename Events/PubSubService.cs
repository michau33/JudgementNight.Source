using System;
using System.Collections.Generic;
using Assets.Scripts.Events.CustomEvents;
using UnityEngine;

namespace Assets.Scripts.Events
{
    public static class PubSubService
    {
        static readonly Dictionary<Type, List<Action<object>>> Listeners = new Dictionary<Type, List<Action<object>>>();

        public static void RegisterListener<T>(Action<object> listener) where T : IEvent
        {
            if (!Listeners.ContainsKey(typeof(T)))
                Listeners.Add(typeof(T), new List<Action<object>>());

            Listeners[typeof(T)].Add(listener);
        }

        public static void Publish<T>(T publishedEvent) where T : IEvent
        {
            if (!Listeners.ContainsKey(typeof(T)))
            {
                Debug.LogWarning("There are no listeners for event " + publishedEvent);
                return;
            }

            foreach (var action in Listeners[typeof(T)])
            {
                action.Invoke(publishedEvent);
            }
        }
    }
}