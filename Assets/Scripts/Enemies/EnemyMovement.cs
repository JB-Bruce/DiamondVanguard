using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float actionTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public Cell cellOn;
    [SerializeField] private List<Cell> closeCells = new List<Cell>();
    [SerializeField] private int index = 0;
    private Cell targetCell;

    private Vector3 lastPosition, targetPosition;

    public GameGrid grid;

    private bool isMoving;
    private bool isRotating;
    public bool isInAction { get => isMoving || isRotating; }
    
    private Entity entity;
    private Quaternion lastRotation, targetRotation;
    private float timer;
    void Start()
    {
        grid = GameGrid.instance;
        entity = GetComponent<Entity>();
        cellOn = grid.GetCell((grid.gridWidth - 1) / 2, (grid.gridHeight - 1) / 2);
        transform.position = cellOn.pos;
        isMoving = false;
        isRotating = false;
        GetCloseCells();
    }

    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime * moveSpeed;
            timer = Mathf.Clamp01(timer);
            transform.position = Vector3.Lerp(lastPosition, targetPosition, timer);
            if (timer == 1f)
            {
                isMoving = false;
            }
        }
        else if (isRotating)
        {
            timer += Time.deltaTime * rotationSpeed;
            timer = Mathf.Clamp01(timer);
            transform.rotation = Quaternion.Lerp(lastRotation, targetRotation, timer);
            if (timer == 1f)
            {
                isRotating = false;
            }
        }
        if (!isMoving)
        {
            GetCloseCells();
        }
    }

    private void GetCloseCells()
    {
        int directionx = 1;
        int directiony = 0;
        for (int i = 0; i < 4; i++) 
        {
            if (directionx == 1)
            {
                closeCells.Add(GetCloseCell(directionx, directiony));
                directionx = -1;
            }
            if (directionx == -1)
            {
                closeCells.Add(GetCloseCell(directionx, directiony));
                directionx = 0;
                directiony = 1;
            }
            if (directiony == 1)
            {
                closeCells.Add(GetCloseCell(directionx, directiony));
                directiony = -1;
            }
            if (directiony == -1) 
            {
                closeCells.Add(GetCloseCell(directionx, directiony));
                directiony = 0;
            }
        }
        targetCell = closeCells[Random.Range(0, closeCells.Count)];
        closeCells.Clear();
        if (targetCell != null)
        {
            GoToCell();
        }
    }
    private Cell GetCloseCell(int x, int y)
    {
        Cell cell = grid.GetCell(cellOn.gridPos.Item1 + x, cellOn.gridPos.Item2 + y);
        if (cell != null)
        {
            index++;
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
        while (cellOn != targetCell)
        {
            RotateToCell(targetCell);
            //MoveToCell(targetCell);
        }
    }

    void RotateToCell(Cell cell)
    {
        if (isInAction)
        {
            return;
        }
        lastRotation = transform.rotation;
        Vector3 dirfromcellOntotarget = (cell.pos - cellOn.pos).normalized;
        float dotProd = Vector3.Dot(dirfromcellOntotarget, cellOn.pos);
        if (dotProd > 0)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        }
        else if (dotProd < 0)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
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
        lastPosition = cellOn.pos;
        targetPosition = cell.pos;
        cellOn.DeleteEntity();
        cell.SetEntity(entity);
        cellOn = cell;
        isMoving = true;
        timer = 0f;
    }
}
