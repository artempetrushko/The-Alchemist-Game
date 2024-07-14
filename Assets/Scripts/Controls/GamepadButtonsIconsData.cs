using UnityEngine;

[CreateAssetMenu(fileName = "Gamepad Buttons Icons", menuName = "UI/Gamepad Buttons Icons", order = 51)]
public class GamepadButtonsIconsData : ScriptableObject
{
    [Header("Gamepad Buttons Icons")]
    [SerializeField] private Sprite _northButtonIcon;
    [SerializeField] private Sprite _southButtonIcon;
    [SerializeField] private Sprite _westButtonIcon;
    [SerializeField] private Sprite _eastButtonIcon;
    [SerializeField] private Sprite _dPadUpButtonIcon;
    [SerializeField] private Sprite _dPadDownButtonIcon;
    [SerializeField] private Sprite _dPadLeftButtonIcon;
    [SerializeField] private Sprite _dPadRightButtonIcon;

    public Sprite NorthButtonIcon => _northButtonIcon;
    public Sprite SouthButtonIcon => _southButtonIcon;
    public Sprite WestButtonIcon => _westButtonIcon;
    public Sprite EastButtonIcon => _eastButtonIcon;
    public Sprite DPadUpButtonIcon => _dPadUpButtonIcon;
    public Sprite DPadDownButtonIcon => _dPadDownButtonIcon;
    public Sprite DPadLeftButtonIcon => _dPadLeftButtonIcon;
    public Sprite DPadRightButtonIcon => _dPadRightButtonIcon;
}
