using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float actionTime;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] public Cell cellOn;

    [SerializeField] bool startInTheMidle;

    [SerializeField] int posX, posY;

    public PlayerAttack attack;

    private Vector3 lastPosition, targetPosition;

    public GameGrid grid;

    private bool isMoving;

    private bool isRotating;

    public bool isInAction { get => isMoving || isRotating; }

    private Entity entity;

    private float timer;

    private Quaternion lastRotation, targetRotation;

    public static PlayerMovement instance;

    private float life;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        grid = GameGrid.instance;

        entity = GetComponent<Entity>();

        //center on the grid or set manualy
        if (startInTheMidle)
            cellOn = grid.GetCell((grid.gridWidth - 1) / 2, (grid.gridHeight - 1) / 2);
        else
            cellOn = grid.GetCell(posX, posY);

        cellOn.SetEntity(entity);

        
        transform.position = cellOn.pos;
        

        isMoving = false;

        isRotating = false;
    }


    void Update()
    {
        if (isMoving) 
        {
            timer += Time.deltaTime * moveSpeed;
            timer = Mathf.Clamp01(timer);
            transform.position = Vector3.Lerp(lastPosition, targetPosition, timer) ;
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
    }

    public void OnMovementEnter(InputAction.CallbackContext ctx)
    {
        if (isInAction) 
        {
            return;
        }

        Vector2 movement = ctx.ReadValue<Vector2>();

        float angle = transform.eulerAngles.y;

        Vector3Int right = Vector3Int.RoundToInt(transform.right);
        Vector3Int forward = Vector3Int.RoundToInt(transform.forward);

        if (movement.x == 1)
        {
            MoveTo(right.x, right.z);
        }
        else if (movement.x == -1)
        {
            MoveTo(-right.x, -right.z);
        }
        else if (movement.y == 1)
        {
            MoveTo(forward.x, forward.z);
        }
        else if (movement.y == -1)
        {
            MoveTo(-forward.x, -forward.z);
        }
    }

    private void MoveTo(int x, int z)
    {
        Cell targetCell = grid.GetCell(cellOn.gridPos.Item1 + x, cellOn.gridPos.Item2 + z);
        if (targetCell != null && !WallDetection(cellOn.pos, targetCell.pos) && !targetCell.HasEntity())
        {
            MoveToCell(targetCell);
        }
        return;
    }

    void MoveToCell(Cell cell)
    {
        lastPosition = cellOn.pos;

        targetPosition = cell.pos;

        cellOn.DeleteEntity();

        cell.SetEntity(entity);

        cellOn = cell;

        transform.position = cellOn.pos;

        isMoving = true;

        timer = 0f;
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        if (isInAction)
        {
            return;
        }
        float rotation = ctx.ReadValue<float>();
        lastRotation = transform.rotation;
        if (rotation > 0f)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0,90,0);
        } else if (rotation < 0f)
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

}
