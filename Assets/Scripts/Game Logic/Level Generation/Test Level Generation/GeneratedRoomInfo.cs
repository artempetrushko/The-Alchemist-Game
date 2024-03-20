using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratedRoomInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject room;
    [SerializeField]
    [Tooltip("Шанс активации комнаты в процентах. При 100% шансе комната всегда будет активна")]
    [Range(0f, 100f)]
    private float chanceToBeEnable;
    [SerializeField]
    private List<PassageInfo> passages = new List<PassageInfo>();

    public bool IsActive => room.activeInHierarchy;

    public void TryEnableRoom()
    {
        room.SetActive(Random.Range(1f, 100f) > 100f - chanceToBeEnable);
    }

    public void TryEnablePassages()
    {
        foreach (var passage in passages)
        {
            passage.ToggleState(passage.LinkedRooms.Where(room => room.IsActive).Count() == passage.LinkedRooms.Count);
        }
    }
}
