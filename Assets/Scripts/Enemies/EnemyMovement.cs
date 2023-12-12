using System.Collections.Generic;
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
    private int nbRot;
    private Cell targetCell;
    private int angleIndex;

    private Vector3 lastPosition, targetPosition;

    public GameGrid grid;

    private bool isMoving = false;
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
                //if timer is complete, it's not moving
                isMoving = false;
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
        else
        {
            //if it's not moving nor rotating
            GetCloseCells();
        }
    }

    private void GetCloseCells()
    {
        //TO DO : verify the out of range
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
        print(closeCells.Count);
        int randomIndex = Random.Range(0, closeCells.Count);
        targetCell = closeCells[randomIndex];
        closeCells.Clear();
        if (targetCell != null)
        {
            GoToCell();
        }
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

    void RotateCell()
    {
        if (isInAction)
        {
            return;
        }
        lastRotation = transform.rotation;
        Quaternion target = Quaternion.FromToRotation(transform.forward, (targetCell.pos - transform.position).normalized);
        print(target.eulerAngles.y);

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
        print("MoveToCell");
        lastPosition = cellOn.pos;
        targetPosition = cell.pos;
        cellOn.DeleteEntity();
        cell.SetEntity(entity);
        cellOn = cell;
        isMoving = true;
        timer = 0f;
    }
}
