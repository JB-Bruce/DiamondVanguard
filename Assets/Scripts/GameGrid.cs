using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] public List<List<Cell>> grid = new();
    [SerializeField] public int gridWidth;
    [SerializeField] public int gridHeight;
    [SerializeField] public int cellSpacement;
    [SerializeField] private Transform gridCenter;
    public static GameGrid instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GridCreation();
    }


    void Update()
    {

    }

    void GridCreation()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            grid.Add(new());
            for (int j = 0; j < gridHeight; j++)
            {
                Cell cell = new(new Vector3(gridCenter.position.x + (i - (gridWidth - 1) / 2)*cellSpacement, gridCenter.position.y, gridCenter.position.z + (j - (gridHeight - 1) / 2)*cellSpacement), (i, j));
                grid[i].Add(cell);
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x < grid.Count && y < grid[0].Count)
        {
            if (x >= 0 && y>= 0)
            {
                return grid[x][y];
            }
            return grid[x][y];
        }
        else
        {
            return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Gizmos.DrawSphere(new Vector3(gridCenter.position.x + (i - (gridWidth - 1) / 2) * cellSpacement, gridCenter.position.y, gridCenter.position.z + (j - (gridHeight - 1) / 2) * cellSpacement), 0.1f);
            }
        }
        
    }
}
