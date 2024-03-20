using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gamepad Buttons Icons", menuName = "UI/Gamepad Buttons Icons", order = 51)]
public class GamepadButtonsIcons : ScriptableObject
{
    [Header("Gamepad Buttons Icons")]
    [SerializeField]
    private Sprite northButtonIcon;
    [SerializeField]
    private Sprite southButtonIcon;
    [SerializeField]
    private Sprite westButtonIcon;
    [SerializeField]
    private Sprite eastButtonIcon;
    [SerializeField]
    private Sprite dPadUpButtonIcon;
    [SerializeField]
    private Sprite dPadDownButtonIcon;
    [SerializeField]
    private Sprite dPadLeftButtonIcon;
    [SerializeField]
    private Sprite dPadRightButtonIcon;

    public Sprite NorthButtonIcon => northButtonIcon;
    public Sprite SouthButtonIcon => southButtonIcon;
    public Sprite WestButtonIcon => westButtonIcon;
    public Sprite EastButtonIcon => eastButtonIcon;
    public Sprite DPadUpButtonIcon => dPadUpButtonIcon;
    public Sprite DPadDownButtonIcon => dPadDownButtonIcon;
    public Sprite DPadLeftButtonIcon => dPadLeftButtonIcon;
    public Sprite DPadRightButtonIcon => dPadRightButtonIcon;
}
