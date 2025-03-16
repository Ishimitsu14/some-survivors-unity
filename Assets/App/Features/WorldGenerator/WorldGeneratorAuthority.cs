using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator2D : MonoBehaviour
{
    [Header("Settings")]
    public List<GameObject> locationPrefabs;
    public Vector2 locationSize = new Vector2(40f, 24f);
    public int generationRadius = 1; // Радиус генерации

    [Header("Debug")]
    [SerializeField] private Vector2Int currentPlayerCoordinate;
    private Dictionary<Vector2Int, GameObject> spawnedLocations = new Dictionary<Vector2Int, GameObject>();

    private void Start()
    {
        GenerateLocation(Vector2Int.zero);
        GenerateNeighbors(Vector2Int.zero);
    }

    private void Update()
    {
        Vector2Int newCoordinate = GetPlayerCoordinate();
        
        if(newCoordinate != currentPlayerCoordinate)
        {
            currentPlayerCoordinate = newCoordinate;
            GenerateNeighbors(currentPlayerCoordinate);
        }
    }

    private Vector2Int GetPlayerCoordinate()
    {
        Vector2 playerPos = transform.position;
        int x = Mathf.RoundToInt(playerPos.x / locationSize.x);
        int y = Mathf.RoundToInt(playerPos.y / locationSize.y);
        return new Vector2Int(x, y);
    }

    public void GenerateNeighbors(Vector2Int center)
    {
        // Генерируем локации по кругу с заданным радиусом
        for(int x = -generationRadius; x <= generationRadius; x++)
        {
            for(int y = -generationRadius; y <= generationRadius; y++)
            {
                // Пропускаем центр
                if(x == 0 && y == 0) continue;
                
                Vector2Int newCoord = center + new Vector2Int(x, y);
                CheckAndGenerateLocation(newCoord);
            }
        }
    }

    private void CheckAndGenerateLocation(Vector2Int coordinate)
    {
        if(!spawnedLocations.ContainsKey(coordinate))
        {
            GenerateLocation(coordinate);
        }
    }

    private void GenerateLocation(Vector2Int coordinate)
    {
        GameObject randomPrefab = locationPrefabs[Random.Range(0, locationPrefabs.Count)];
        Vector2 position = new Vector2(
            coordinate.x * locationSize.x,
            coordinate.y * locationSize.y
        );
        
        GameObject newLocation = Instantiate(randomPrefab, position, Quaternion.identity);
        newLocation.transform.SetParent(transform);
        
        spawnedLocations.Add(coordinate, newLocation);
        Location2D locationComponent = newLocation.AddComponent<Location2D>();
        locationComponent.coordinate = coordinate;
    }
}

public class Location2D : MonoBehaviour
{
    public Vector2Int coordinate;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            WorldGenerator2D generator = FindObjectOfType<WorldGenerator2D>();
            generator.GenerateNeighbors(coordinate);
        }
    }
}