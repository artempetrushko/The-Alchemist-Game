using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public abstract class PlayerMenuMechanicsManager : MonoBehaviour
    {
        public abstract void InitializeLinkedView(PlayerMenuSectionView mechanicsLinkedView);
    }
}