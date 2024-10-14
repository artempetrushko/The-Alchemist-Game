using GameLogic.PlayerMenu;

namespace GameLogic
{
    public interface IPlayerMenuSectionPresenter
    {
        ISelectableCollection DefaultSelectableCollection { get; }

        void Show();
        void Hide();
    }
}