using Common;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallPlacementRule
{
    public RoomType room_a;
    public RoomType room_b;
    public WallType wall;
}
[Serializable]
public class OfferedNeighbours
{
    public RoomType parent_room;
    public Vector2Int position;
    public Vector2Int diraction;
    public RoomType cild_room;
}

[CreateAssetMenu(fileName = "WallPlacementRules", menuName = "ScriptableObjects/WallPlacementRules", order = 1)]
public class WallPlacementRules : ScriptableObject
{
    public List<WallPlacementRule> neighborsLimitation;
    public List<OfferedNeighbours> offeredNeighbours;
}
