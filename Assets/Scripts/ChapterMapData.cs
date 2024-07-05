using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChapterMapData", menuName = "ScriptableObjects/ChapterMapData", order = 1)]
public class ChapterMapData : ScriptableObject
{
    [Header("Map Data")]
    [SerializeField] public MapController StartMap;
    [SerializeField] public List<MapController> MonsterMapList;
    [SerializeField] public MapController BossMap;
    [SerializeField] public MapController JournalMap;
    [SerializeField] public MapController BoosterMap;

    [Header("Grid size")]
    [SerializeField] public int size; // tamaño de la grilla
    
    [Header("Minimum Map count")]
    [SerializeField] public int minMapCount;

    public MapController PickRandomMonsterMap()
    {
        System.Random random = new System.Random();

        // Seleccionar un índice aleatorio
        int randomIndex = random.Next(MonsterMapList.Count);

        // Obtener el elemento aleatorio
        return MonsterMapList[randomIndex];
    }
}