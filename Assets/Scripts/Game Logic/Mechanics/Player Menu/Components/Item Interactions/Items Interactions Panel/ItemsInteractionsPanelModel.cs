namespace GameLogic.PlayerMenu
{
    public class ItemsInteractionsPanelModel
    {
        public readonly ItemSlot SelectedItemSlot;
        public readonly (ItemsInteraction interaction, ItemsInteractionButton linkedButton)[] InteractionInfos;

        public int SelectedInteractionNumber;

        public ItemsInteractionsPanelModel(ItemSlot selectedItemSlot, (ItemsInteraction interaction, ItemsInteractionButton linkedButton)[] interactionInfos)
        {
            SelectedItemSlot = selectedItemSlot;
            InteractionInfos = interactionInfos;
            SelectedInteractionNumber = 1;
        }
    }
}