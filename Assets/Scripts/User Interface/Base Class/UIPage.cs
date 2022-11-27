using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Lindon.UI
{
    public abstract class UIPage : UIClass
    {
        [Header("Page Info"), HorizontalLine]
        [SerializeField] protected string title;
        [SerializeField] private LoadTiming loadTiming = LoadTiming.InStart;
        //[Scene, SerializeField] private int loadScene;
        [SerializeField] private List<UIElement> elements;

        [Header("Events")]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        public string Title
        {
            get
            {
                if (title == null || title == string.Empty)
                {
                    title = gameObject.name;
                }
                return title;
            }
        }

        /// <summary>
        /// call on load game
        /// </summary>
        protected abstract void SetValuesOnSceneLoad();

        public virtual void Load(LoadTiming mode)
        {
            if (loadTiming == mode)
            {
                SetValuesOnSceneLoad();
            }
        }

        public override void SetActive(bool value)
        {
            base.SetActive(value);

            foreach (var element in elements)
            {
                element.SetActive(value);
            }

            if (value)
            {
                onOpen?.Invoke();
            }
            else
            {
                onClose?.Invoke();
            }
        }
    }

    public enum LoadTiming
    {
        InAwake,
        InStart
    }
}