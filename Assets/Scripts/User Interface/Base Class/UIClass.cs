using UnityEngine;

namespace Lindon.UI
{
    public abstract class UIClass : MonoBehaviour
    {
        /// <summary>
        /// call when open this panel
        /// </summary>
        protected abstract void SetValues();

        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);

            if (value)
            {
                SetValues();
            }
        }
    }
}