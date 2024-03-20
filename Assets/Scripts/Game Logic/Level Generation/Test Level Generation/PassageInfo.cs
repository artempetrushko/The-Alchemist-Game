using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject passage;
    [SerializeField]
    private List<GeneratedRoomInfo> linkedRooms = new List<GeneratedRoomInfo>();
    [Space]
    [SerializeField]
    private List<ToggleEnvironmentObjectsInfo> toggleEnvironmentObjects = new List<ToggleEnvironmentObjectsInfo>();

    public List<GeneratedRoomInfo> LinkedRooms => linkedRooms;

    public void ToggleState(bool isPassageEnable)
    {
        passage.SetActive(isPassageEnable);

        var currentCondition = passage.activeInHierarchy ? ToggleCondition.PassageEnabled : ToggleCondition.PassageDisabled;
        foreach (var obj in toggleEnvironmentObjects)
        {
            obj.ExecuteToggleAction(currentCondition);
        }
    }
}
