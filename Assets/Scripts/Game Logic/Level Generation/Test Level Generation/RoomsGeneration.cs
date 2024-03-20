using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RoomsGeneration : MonoBehaviour
{
    private void Awake()
    {
        var rooms = FindObjectsOfType<GeneratedRoomInfo>();
        foreach (var room in rooms)
        {
            room.TryEnableRoom();
        }
        foreach (var room in rooms)
        {
            room.TryEnablePassages();
        }
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
