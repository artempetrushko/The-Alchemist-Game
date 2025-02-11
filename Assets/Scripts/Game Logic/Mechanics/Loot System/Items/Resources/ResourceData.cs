using UnityEngine;

namespace GameLogic.LootSystem
{
    public enum RareClass
    {
        Junk,
        Ordinary,
        Rare,
        Precious
    }

    public abstract class ResourceData : StackableItemData
    {
        [Header("��������� �������")]
        [SerializeField]
        protected RareClass rare;

        public RareClass Rare => rare;
    }
}
