using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class ItemData : ScriptableObject
    {
        [Header("Общие параметры")]
        [SerializeField]
        protected int id;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected PickableItem physicalRepresentation;
        [SerializeField]
        protected string title;
        [SerializeField, Multiline]
        protected string description;

        public int ID => id;
        public Sprite Icon => icon;
        public PickableItem PhysicalRepresentation => physicalRepresentation;
        public string Title => title;
        public string BaseDescription => description;

        public abstract ItemState GetItemState();
    }
}