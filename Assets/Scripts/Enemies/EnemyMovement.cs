using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float actionTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public Cell cellOn;
    [SerializeField] private List<Cell> closeCells = new List<Cell>();
    [SerializeField] private int index = 0;
    [SerializeField] private float rangeDistance = 2;
    private bool hasTarget = false;
    private int nbRot;
    private Cell targetCell;
    private int angleIndex;

    private Vector3 lastPosition, targetPosition;

    public GameGrid grid;

    PlayerMovement player;

    private bool isMoving = false;
    private bool isRotating;
    public bool isInAction { get => isMoving || isRotating; }
    
    private Entity entity;
    private Quaternion lastRotation, targetRotation;
    private float timer;

    [SerializeField] LayerMask enemyLayer;
    void Start()
    {
        grid = GameGrid.instance;
        entity = GetComponent<Entity>();
        cellOn = grid.GetCell((grid.gridWidth - 1) / 2, (grid.gridHeight - 1) / 2);
        transform.position = cellOn.pos;

        player = PlayerMovement.instance;

        Invoke("GoToCell", actionTime);
    }

    void Update()
    {
        if (isMoving)
        {
            //Go from a cell to another
            timer += Time.deltaTime * moveSpeed;
            timer = Mathf.Clamp01(timer);
            transform.position = Vector3.Lerp(lastPosition, targetPosition, timer);
            if (timer == 1f)
            {
                isMoving = false;
                hasTarget = false;
                GoToCell();
            }
        }
        else if (isRotating)
        {
            //rotating
            timer += Time.deltaTime * rotationSpeed;
            timer = Mathf.Clamp01(timer);
            transform.rotation = Quaternion.Lerp(lastRotation, targetRotation, timer);
            if (timer == 1f)
            {
                isRotating = false;
                GoToCell();
            }
        }
    }

    private float DistancetoCell()
    {
        float rangeDistanceCell = rangeDistance * grid.cellSpacement;
        return rangeDistanceCell;
    }
    private void GetCloseCells()
    {
        closeCells.Add(GetCloseCell(1, 0));
        closeCells.Add(GetCloseCell(-1, 0));
        closeCells.Add(GetCloseCell(0, 1));
        closeCells.Add(GetCloseCell(0, -1));
        for (int i = 0; i < closeCells.Count; i++)
        {
            if (closeCells[i] == null)
            {
                closeCells.Remove(closeCells[i]);
            }
        }

        int randomIndex = Random.Range(0, closeCells.Count);
        targetCell = closeCells[randomIndex];
        closeCells.Clear();
    }
    private Cell GetCloseCell(int x, int y)
    {
        Cell cell = grid.GetCell(cellOn.gridPos.Item1 + x, cellOn.gridPos.Item2 + y);

        //This part of the code is made by the lead dev, based on my previous works
        if (cell != null && cell.entity == null && !WallDetection(cellOn.pos, cell.pos))
        {
            return cell;
        }
        else
        {
            return null;
        }
    }

    void GoToCell()
    {
        if (isInAction)
        {
            return;
        }

        if (PlayerDetection())
        {
            hasTarget = false;
            targetCell = NextCellToGoToTarget(player.cellOn);
        }
        else if(!hasTarget)
        {
            hasTarget = true;
            GetCloseCells();
        }

        Vector3Int forward = Vector3Int.RoundToInt(transform.forward);
        Cell forwardCell = grid.GetCell(cellOn.gridPos.Item1 + forward.x, cellOn.gridPos.Item2 + forward.z);
        if (forwardCell == targetCell)
        {
            MoveToCell(targetCell);
        }
        else
        {
            RotateCell();
        }
    }

    private Cell NextCellToGoToTarget(Cell cell)
    {
        Cell finalCell = cellOn;

        List<Cell> path = new();

        if(cellOn == cell)
        {
            path.Add(cellOn);
        }

        List<Cell> unvisitedCells = new();

        Dictionary<Cell, Cell> previous = new();
        Dictionary<Cell, float> distances = new();

        List<Cell> allCells = grid.GetAllCells();

        foreach (var item in allCells)
        {
            unvisitedCells.Add(item);
            distances.Add(item, float.MaxValue);
        }

        distances[cellOn] = 0f;

        while (unvisitedCells.Count != 0)
        {
            unvisitedCells = unvisitedCells.OrderBy(newCell => distances[newCell]).ToList();

            Cell nextCell = unvisitedCells[0];
            unvisitedCells.Remove(nextCell);

            if(nextCell == cell)
            {
                while (previous.ContainsKey(nextCell))
                {
                    path.Insert(0, nextCell);
                    nextCell = previous[nextCell];
                }
                path.Insert(0, nextCell);
                break;
            }

            List<Cell> connectedCells = GetPossibilities(nextCell, grid.GetNeighbors(nextCell));

            foreach (Cell neighbor in connectedCells)
            {
                float dist = Vector3.Distance(nextCell.pos, neighbor.pos);
                float total = distances[nextCell] + dist;

                if(total < distances[neighbor])
                {
                    distances[neighbor] = total;
                    previous[neighbor] = nextCell;
                }
            }
        }


        finalCell = path[1];

        return finalCell;
    }

    private List<Cell> GetPossibilities(Cell startCell, List<Cell> previousList)
    {
        List<Cell> newList = new();

        foreach (var cell in previousList)
        {
        
            if(cell != null && (!cell.HasEntity() || cell == player.cellOn) && !WallDetection(startCell.pos, cell.pos))
                newList.Add(cell);
        }

        return newList;
    }

    bool PlayerDetection()
    {
        float distanceEnemyPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceEnemyPlayer <= DistancetoCell() && !Physics.Raycast(PlayerMovement.instance.transform.position, transform.position - PlayerMovement.instance.transform.position, distanceEnemyPlayer, enemyLayer))
        {
            return true;
        }

        return false;
    }
    void RotateCell()
    {
        if (isInAction)
        {
            return;
        }
        lastRotation = transform.rotation;
        Quaternion target = Quaternion.FromToRotation(transform.forward, (targetCell.pos - transform.position).normalized);

        if (target.eulerAngles.y > 180)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        }
        else if (target.eulerAngles.y < 180)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        }
        isRotating = true;
        timer = 0f;
    }

    public bool WallDetection(Vector3 startPos, Vector3 endPos)
    {
        Debug.DrawRay(startPos, (endPos - startPos) * Vector3.Distance(startPos, endPos), Color.red, 2);
        if (Physics.Raycast(startPos, endPos - startPos, Vector3.Distance(startPos, endPos)))
        {
            return true;
        }
        return false;
    }
    void MoveToCell(Cell cell)
    {
        if (isInAction)
        {
            return;
        }
        lastPosition = cellOn.pos;
        targetPosition = cell.pos;
        cellOn.DeleteEntity();
        cell.SetEntity(entity);
        cellOn = cell;
        isMoving = true;
        timer = 0f;
    }
}
