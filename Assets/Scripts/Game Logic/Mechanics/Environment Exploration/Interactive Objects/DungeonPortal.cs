using System;
using System.Collections;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public enum PortalState
    {
        Unstable,
        Stabilizing,
        Stable
    }

    public class DungeonPortal : InteractiveObject
    {
        public event Action StabilizationStarted; // PortalInteractionDisabled + удалять стабилизаторы портала из инвентаря
        public event Action PortalInteractionDisabled; // отключать коллайдер и Interactive Object Info View

        [Space, SerializeField]
        private int nextSceneIndex;
        [SerializeField, Tooltip("Отметить, если текущий уровень последний в забеге")]
        private bool isLastLevel;
        [Space, SerializeField]
        private GameObject stabilizationEffect;
        [SerializeField]
        private int stabilizationTimeInSeconds;
        [SerializeField]
        private int spawnerAppearanceDelayInSeconds;

        public int NextSceneIndex => nextSceneIndex;
        public bool IsLastLevel => isLastLevel;
        public int StabilizationTimeInSeconds => stabilizationTimeInSeconds;
        public PortalState PortalState { get; private set; }

        public void SetInteractionAvailability(bool isInteractable) => GetComponent<Collider>().enabled = isInteractable;

        public void StartStabilization(bool isStabilizerAvailable)
        {
            if (PortalState == PortalState.Unstable && isStabilizerAvailable)
            {
                PortalState = PortalState.Stabilizing;
                StabilizationStarted?.Invoke();
                StartCoroutine(StabilizePortal_COR());
            }
        }

        private IEnumerator StabilizePortal_COR()
        {
            stabilizationEffect.SetActive(true);
            yield return new WaitForSeconds(stabilizationTimeInSeconds);
            stabilizationEffect.SetActive(false);
            PortalState = PortalState.Stable;
            SetInteractionAvailability(true);
        }
    }
}