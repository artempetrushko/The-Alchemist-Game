using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.LootSystem
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField]
        private ItemData itemData;

        private ItemState currentItemState;

        public ItemState CurrentItemState
        {
            get => currentItemState ?? itemData.GetItemState();
            set
            {
                if (value.BaseParams.Equals(itemData))
                {
                    currentItemState = value;
                }
            }
        }

        private void OnEnable()
        {
            if (GetComponentInParent<PlayerInput>() != null)
            {
                var particleSystem = GetComponentInChildren<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.gameObject.SetActive(false);
                }
                if (GetComponent<SphereCollider>() != null)
                {
                    Destroy(GetComponent<SphereCollider>());
                }
            }
        }
    }
}