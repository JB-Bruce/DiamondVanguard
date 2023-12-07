using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] public List<List<Cell>> grid = new();
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float cellSpacement;
    [SerializeField] private Transform gridCenter;
    void Start()
    {
        GridCreation();
        Debug.Log(GetCell(0, 4).pos);
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
                Cell cell = new(new Vector3(gridCenter.position.x + i - (gridWidth - 1) / 2, gridCenter.position.y, gridCenter.position.z + j - (gridHeight - 1) / 2), (i, j));
                grid[i].Add(cell);
            }
        }
    }

    Cell GetCell(int x, int y)
    {
        if (x < grid.Count && y < grid[0].Count)
        {
            return grid[x][y];
        }
        else
        {
            return null;
        }
    }
}
