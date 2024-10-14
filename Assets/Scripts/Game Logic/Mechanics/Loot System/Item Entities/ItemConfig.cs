using GameLogic.EnvironmentExploration;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.LootSystem
{
    public abstract class ItemConfig : ScriptableObject
    {
        [Header("Общие параметры")]
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private PickableItem _physicalRepresentation;
        [SerializeField] private LocalizedString _title;
        [SerializeField] private LocalizedString _description;
        [SerializeField] private int _castingDamage;
        [SerializeField] private ItemEffect[] _effects;

        public string Id => _id;
        public Sprite Icon => _icon;
        public PickableItem PhysicalRepresentation => _physicalRepresentation;
        public LocalizedString Title => _title;

        public abstract ItemState CreateItem();
    }
}