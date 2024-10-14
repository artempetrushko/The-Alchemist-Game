namespace Controls
{
    public class ControlTipPresenter
    {
        public void ShowControlTip(ControlTip tip, ControlTipView view)
        {
            if (tip.KeyName != null)
            {
                view.SetKeyIconContainerActive(true);
                view.SetKeyNameText(tip.KeyName);
            }
            else if (tip.KeyIcon != null)
            {
                view.SetGamepadKeyIconContainerActive(true);
                view.SetGamepadKeyIcon(tip.KeyIcon);
            }
        }

        public void ShowDetailedControlTip(DetailedControlTip tip, DetailedControlTipView view)
        {
            ShowControlTip(tip, view);

            view.SetActionTitleText(tip.ActionTitle);
        }
    }
}