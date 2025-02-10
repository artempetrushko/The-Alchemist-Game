using System;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        public event Action ObjectDestroyed;

        [SerializeField]
        protected string title;

        public string Title => title;

        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}