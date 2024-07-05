using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MapGeneratorController : MonoBehaviour
{
    [CustomEditor(typeof(MapGeneratorController))]
    public class MyScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MapGeneratorController myScript = (MapGeneratorController)target;
            if (GUILayout.Button("Generar"))
            {
                myScript.GenerateGrid();
            }
        }
    }
    internal class CardinalPoints
    {
        public HashSet<int> north;
        public HashSet<int> east;
        public HashSet<int> south;
        public HashSet<int> west;

        public CardinalPoints(HashSet<int> north, HashSet<int> east, HashSet<int> south, HashSet<int> west)
        {
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
        }
    }

    internal class Neighbours
    {
        public CellElement north;
        public CellElement east;
        public CellElement south;
        public CellElement west;

        public Neighbours(CellElement north=null, CellElement east = null, CellElement south = null, CellElement west = null)
        {
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
        }

        public bool HasNoNeighbours()
        {
            return (north is null || !north.IsSet) && (east is null || !east.IsSet) && (south is null || !south.IsSet) && (west is null || !west.IsSet);
        }

        override public string ToString()
        {
            var n = north is null ? "null" : north.ToString();
            var e = east is null ? "null" : east.ToString();
            var s = south is null ? "null" : south.ToString();
            var w = west is null ? "null" : west.ToString();

            return $"N:{n} S:{e} E:{s} O:{w}";
        }
    }

    internal class Cell
    {
        public int id;
        public CardinalPoints cardinalPoints;

        public Cell(int id, CardinalPoints cardinalPoints)
        {
            this.id = id;
            this.cardinalPoints = cardinalPoints;
        }
    }

    internal class CellElement
    {
        public int id;
        public HashSet<int> availableCellChoices;
        public MapController placedMap;
        public bool IsOrigin;
        public bool IsLeaf;

        public CellElement(int id)
        {
            this.id = id;
            IsOrigin = false;
            IsLeaf = false;
            availableCellChoices = new HashSet<int> {0,1,2,3,4,5,6};
        }

        public void UpdateAvailableChoices(HashSet<int> availableChoices)
        {
            availableCellChoices.IntersectWith(availableChoices);
        }

        public bool IsSet => id != -1;

        public override string ToString()
        {
            return IsSet ? id.ToString() : ".";
        }
    }

    [Header("Map Data")]
    [SerializeField] ChapterMapData chapterMapData;

    [Header("Grid settings")]
    [SerializeField] Transform origin;
    [SerializeField] int gap; // Espacio entre los distintos mapas.
    
    private Cell[] cellDefinitions;

    private CellElement[,] grid;

    bool IsLeafId(int id)
    {
        return new[] { 3, 4, 5, 6 }.Contains(id);
    }

    int CountTotalRooms()
    {
        int total = 0;
        for (int y = 0; y < chapterMapData.size; y++)
        {
            for (int x = 0; x < chapterMapData.size; x++)
            {
                if (grid[y,x].IsSet)
                    total++;
            }
        }

        return total;
    }

    int CountTotalLeafRooms() 
    { 
        int total = 0;
        
        for (int y = 0; y < chapterMapData.size; y++)
        {
            for (int x = 0; x < chapterMapData.size; x++)
            {
                if (IsLeafId(grid[y, x].id))
                    total++;
            }
        }

        return total;
    }
    void PrintGrid()
    {
        string rowString = "";
        for (int y = 0; y < chapterMapData.size; y++)
        {
            for (int x = 0; x < chapterMapData.size; x++)
            {
                rowString += $"{grid[y, x]} ";
            }

            rowString += "\n";
        }
        print(rowString);
    }

    CellElement Up(Vector2Int position)
    {
        if (position.y == 0)
            return null;

        return grid[position.y - 1, position.x];
    }

    CellElement Right(Vector2Int position)
    {
        if (position.x == chapterMapData.size - 1)
            return null;

        return grid[position.y, position.x + 1];
    }

    CellElement Down(Vector2Int position)
    {
        if (position.y == chapterMapData.size - 1)
            return null;

        return grid[position.y + 1, position.x];
    }

    CellElement Left(Vector2Int position)
    {
        if (position.x == 0)
            return null;

        return grid[position.y, position.x - 1];
    }

    Neighbours GetNeighbours(Vector2Int position)
    {
        Neighbours neighbours = new();

        neighbours.north = Up(position);
        neighbours.east = Right(position);
        neighbours.south = Down(position);
        neighbours.west = Left(position);

        return neighbours;
    }

    int PickRandom(HashSet<int> hashSet)
    {
        List<int> list = new List<int>(hashSet);

        // Crear un generador de números aleatorios
        System.Random random = new System.Random();

        // Seleccionar un índice aleatorio
        int randomIndex = random.Next(list.Count);

        // Obtener el elemento aleatorio
        return list[randomIndex];
    }

    void SetCell(Vector2Int position, int radius, int id, bool isLeaf=false)
    {
        if (grid[position.y, position.x].IsSet)
            return;

        Neighbours neighbours = GetNeighbours(position);

        // Si id es -1 estoy haciendo un seteo random.
        if (id == -1)
        {
            // Si no tengo vecinos me voy
            if (neighbours.HasNoNeighbours())
                return;

            HashSet<int> choices = grid[position.y, position.x].availableCellChoices;

            // Las primeras iteraciones no pueden ser hojas
            if (radius <= 2)
                choices.IntersectWith(new HashSet<int> { 0, 1, 2 });

            // Fuerzo a colocar una celda hoja
            if (isLeaf)
                choices.IntersectWith(new HashSet<int> { 3, 4, 5, 6 });

            if (choices.Count == 0)
                return;

            id = PickRandom(choices);

            if (IsLeafId(id))
            {
                grid[position.y, position.x].IsLeaf = true;
            }
        }

        grid[position.y, position.x].id = id;

        if (neighbours.north is not null && !neighbours.north.IsSet)
            Up(position).UpdateAvailableChoices(cellDefinitions[id].cardinalPoints.north);

        if (neighbours.east is not null && !neighbours.east.IsSet)
            Right(position).UpdateAvailableChoices(cellDefinitions[id].cardinalPoints.east);

        if (neighbours.south is not null && !neighbours.south.IsSet)
            Down(position).UpdateAvailableChoices(cellDefinitions[id].cardinalPoints.south);

        if (neighbours.west is not null && !neighbours.west.IsSet)
            Left(position).UpdateAvailableChoices(cellDefinitions[id].cardinalPoints.west);
    }

    private void IterateGridGeneration(Vector2Int position)
    {
        int maxRadius = chapterMapData.size / 2;

        // Recorro la grilla desde el centro hacia afuera
        // de modo que toco todos los puntos que estan a distancia "radio" del centro
        for (int radius = 1; radius <= maxRadius; radius++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                int available = radius - Math.Abs(y);

                for (int x = -available; x <= available; x++)
                {
                    if (available - Math.Abs(x) != 0)
                        continue;

                    // Condicion de corte
                    if (CheckValidGrid())
                        return;

                    SetCell(new Vector2Int(position.x + x, position.y + y), radius, -1, radius == maxRadius);
                }
            }
        }
    }

    public void GenerateGrid()
    {
        chapterMapData.size += (chapterMapData.size % 2 == 0 ? 1 : 0); // Fuerzo a que sea tamaño impar.

        // Defino las reglas para los puntos cardinales de cada tipo de celda
        cellDefinitions = new Cell[]
        {
            new Cell(0, new CardinalPoints(new HashSet<int>{0, 2, 5},   new HashSet<int>{0,1,6},    new HashSet<int>{0,2,3},    new HashSet<int>{0,1,4})),
            new Cell(1, new CardinalPoints(new HashSet<int>{ },         new HashSet<int>{0,1,6},    new HashSet<int>{ },        new HashSet<int>{0,1,4})),
            new Cell(2, new CardinalPoints(new HashSet<int>{0, 2, 5},   new HashSet<int>{ },        new HashSet<int>{0,2,3},    new HashSet<int>{ })),
            new Cell(3, new CardinalPoints(new HashSet<int>{0, 2},      new HashSet<int>{ },        new HashSet<int>{ },        new HashSet<int>{ })),
            new Cell(4, new CardinalPoints(new HashSet<int>{ },         new HashSet<int>{0,1},      new HashSet<int>{ },        new HashSet<int>{ })),
            new Cell(5, new CardinalPoints(new HashSet<int>{ },         new HashSet<int>{ },        new HashSet<int>{0,2},      new HashSet<int>{ })),
            new Cell(6, new CardinalPoints(new HashSet<int>{ },         new HashSet<int>{ },        new HashSet<int>{ },        new HashSet<int>{0,1}))
        };

        // inicializo la grilla
        grid = new CellElement[chapterMapData.size, chapterMapData.size];

        for (int y = 0; y < chapterMapData.size; y++)
        {
            for (int x = 0; x < chapterMapData.size; x++)
            {
                grid[y, x] = new CellElement(-1);
            }
        }

        Vector2Int position = new Vector2Int(chapterMapData.size / 2, chapterMapData.size / 2);

        int intialId = PickRandom(new HashSet<int> {0,1,2,3,4,5,6});

        // Seteo la celda inicial
        SetCell(position, 0, intialId);
        grid[position.y, position.x].IsOrigin = true;

        // Seteo el resto de las celdas
        IterateGridGeneration(position);

        PrintGrid();
    }

    void PlaceMaps()
    {
        Vector3 center = new Vector3
        (
            origin.position.x - (chapterMapData.size * gap) / 2,
            origin.position.y -(chapterMapData.size * gap) / 2,
            0
        );

        int leafCounter = 0;

        for (int y = 0; y < chapterMapData.size;  y++)
        {
            for(int x = 0;x < chapterMapData.size; x++)
            {
                if(!grid[y, x].IsSet)
                    continue;

                if (grid[y, x].IsLeaf)
                {
                    MapController newLeafMap;

                    switch (leafCounter)
                    {
                        case 0: // Primero la sala del diario
                            newLeafMap = Instantiate(chapterMapData.JournalMap, center + new Vector3(x * gap, y * gap, 0), Quaternion.identity);
                            break;

                        case 1: // Segundo la sala de Boost
                            newLeafMap = Instantiate(chapterMapData.BoosterMap, center + new Vector3(x * gap, y * gap, 0), Quaternion.identity);
                            break;

                        case 2: // Tercero la sala del Boss
                            newLeafMap = Instantiate(chapterMapData.BossMap, center + new Vector3(x * gap, y * gap, 0), Quaternion.identity);
                            break;

                        default: // Las demas salas Hoja son comunes
                            newLeafMap = Instantiate(chapterMapData.PickRandomMonsterMap(), center + new Vector3(x * gap, y * gap, 0), Quaternion.identity);
                            break;
                    }
                    newLeafMap.SetEnabledAllDoors(false);

                    grid[y, x].placedMap = newLeafMap;
                    leafCounter++;
                } 
                else if(grid[y, x].IsOrigin)
                {
                    MapController newMap = Instantiate(chapterMapData.StartMap, center + new Vector3(x * gap, y * gap, 0), Quaternion.identity);
                    newMap.SetEnabledAllDoors(false);

                    grid[y, x].placedMap = newMap;
                    GameManager.Instance.startingMap = newMap;
                    VictorController victor = Instantiate(GameManager.Instance.VictorPrefab, newMap.transform.position, Quaternion.identity);
                    GameManager.Instance.VictorInstance = victor;
                }
                else
                {
                    MapController newMap = Instantiate(chapterMapData.PickRandomMonsterMap(), center + new Vector3(x * gap, y * gap, 0), Quaternion.identity);
                    newMap.SetEnabledAllDoors(false);

                    grid[y, x].placedMap = newMap;
                }
            }
        }
    }
    void PlaceDoors()
    {
        for (int y = 0; y < chapterMapData.size; y++)
        {
            for (int x = 0; x < chapterMapData.size; x++)
            {
                if (grid[y, x].IsSet)
                {
                    Neighbours neighbours = GetNeighbours(new Vector2Int(x, y));

                    // En el algoritmo el eje Y crece para abajo.
                    // En Unity el eje Y crece para arriba (x_x)
                    // Por ende las puertas están invertidas en el eje vertical

                    if (neighbours.north is not null && neighbours.north.placedMap is not null)
                    {
                        MapController.ConnectDoors(
                            grid[y, x].placedMap, 
                            neighbours.north.placedMap,
                            MapController.Doors.Up
                        );
                    }

                    if (neighbours.east is not null && neighbours.east.placedMap is not null)
                    {
                        MapController.ConnectDoors(
                            grid[y, x].placedMap,
                            neighbours.east.placedMap,
                            MapController.Doors.Right
                        );
                    }

                    if (neighbours.south is not null && neighbours.south.placedMap is not null)
                    {
                        MapController.ConnectDoors(
                            grid[y, x].placedMap,
                            neighbours.south.placedMap,
                            MapController.Doors.Bottom
                        );
                    }
                    if (neighbours.west is not null && neighbours.west.placedMap is not null)
                    {
                        MapController.ConnectDoors(
                            grid[y, x].placedMap,
                            neighbours.west.placedMap,
                            MapController.Doors.Left
                        );
                    }
                }
            }
        }
    }

    bool CheckValidGrid()
    {
        // 3 mapas hoja (Journal, Booster, Boost) + 1 por las dudas que el origen sea hoja.
        return CountTotalLeafRooms() >= 4 && CountTotalRooms() >= chapterMapData.minMapCount;
    }

    void Start()
    {

        bool generate = true;
        int attempts = 0;

        while (generate) { 
            GenerateGrid();

            // Validar que cumpla con los requisitos
            if (CheckValidGrid())
                generate = false;

            attempts++;
        }
        print(attempts);

        PlaceMaps();
        PlaceDoors();
    }
}
