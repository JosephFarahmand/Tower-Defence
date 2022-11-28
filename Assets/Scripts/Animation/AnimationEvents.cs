using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WD.Animation
{
    public class AnimationEvents : MonoBehaviour
    {
        [SerializeField] private List<AnimationEventStruct> events;

        public void PlayEvent(string eventName)
        {
            events.Find(x => x.EventName == eventName).Event?.Invoke();
        }

        [System.Serializable]
        public struct AnimationEventStruct
        {
            [SerializeField] private string eventName;
            [SerializeField] private UnityEvent @event;

            public string EventName { get => eventName; }
            public UnityEvent Event { get => @event; }
        }
    }
}