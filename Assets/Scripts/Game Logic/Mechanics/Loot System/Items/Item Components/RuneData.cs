using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Rune", menuName = "Game Entities/Items/Rune", order = 51)]
    public class RuneData : ScriptableObject
    {
        [SerializeField]
        protected List<AspectData> sealedAspects = new();
        [SerializeField]
        protected ItemEffect resultEffect;

        public List<AspectData> SealedAspects => sealedAspects;
        public ItemEffect ResultEffect => resultEffect;
    }
}