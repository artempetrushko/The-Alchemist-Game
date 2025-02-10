namespace GameLogic.PlayerMenu
{
    public interface IPlayerMenuInteractable
    {
        NeighboringInteractableElements Neighbours { get; set; }

        void Select();
        void Deselect();
        void Click();
    }
}