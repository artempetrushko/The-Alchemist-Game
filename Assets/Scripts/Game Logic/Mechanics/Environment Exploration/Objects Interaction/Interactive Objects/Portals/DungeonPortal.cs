using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public class DungeonPortal : InteractiveObject
	{
        [Space]
        [SerializeField] private GameObject _stabilizationEffectPrefab;
        [SerializeField] private int _stabilizationTimeInSeconds;

        public GameObject StabilizationEffectPrefab => _stabilizationEffectPrefab;
        public int StabilizationTimeInSeconds => _stabilizationTimeInSeconds;
        public bool IsStable { get; set; } = false;

        public override void Interact()
        {
            throw new System.NotImplementedException();
        }

        public void SetInteractionAvailability(bool isInteractable) => GetComponent<Collider>().enabled = isInteractable;
	}
}