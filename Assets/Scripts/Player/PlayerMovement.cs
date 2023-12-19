using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float actionTime;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] public Cell cellOn;

    [SerializeField] bool startInTheMidle;

    [SerializeField] bool snapToGrid;

    [SerializeField] int posX, posY;

    [SerializeField] public GameObject pauseCanvas;

    [SerializeField] List<MonoBehaviour> behaviours = new();

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

    public bool isOnPause;

    Vector2 movement;

    float rotation;

    public WalkScreenShake HeadBob;

    [SerializeField] AudioSource stepSound;

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
        {
            cellOn = grid.GetCell((grid.gridWidth - 1) / 2, (grid.gridHeight - 1) / 2);
        }
        else if (snapToGrid)
        {
            cellOn = grid.GetClosestCell(transform.position);
        }
        else
        {
            cellOn = grid.GetCell(posX, posY);
        }


        cellOn.SetEntity(entity);

        
        transform.position = cellOn.pos;
        

        isMoving = false;

        isRotating = false;

        isOnPause = false;


    }

    public void SpeedChange(bool flash)
    {
        if (flash)
        {
            moveSpeed *= 2;
            rotationSpeed *= 2;
        }
        else
        {
            moveSpeed /= 2f;
            rotationSpeed /= 2f;
        }
    }

    public void Spawn(Vector2Int SpawnPoint, float rota)
    {
        posX = SpawnPoint.x;
        posY = SpawnPoint.y;
        Cell targetCell = grid.GetCell(SpawnPoint.x, SpawnPoint.y);
        MoveToCell(targetCell);
        transform.rotation = Quaternion.Euler(0, rota, 0);
    }

    void Update()
    {
        if (isMoving) 
        {
            timer += Time.deltaTime * moveSpeed;
            timer = Mathf.Clamp01(timer);
            transform.position = Vector3.Lerp(lastPosition, targetPosition, timer);
            if (!stepSound.isPlaying)
            {
                stepSound.Play();
            }
            if (timer == 1f)
            {
                HeadBob.StopBobbing();
                isMoving = false;
                TryMove();
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
                TryRotate();
            }
        }
        else
        {
            stepSound.Stop();
        }
    }

    public void OnMovementEnter(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();

        if (isInAction)
        {
            return;
        }
        TryMove();
    }

    public void TryMove()
    {
        
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

        isMoving = true;

        HeadBob.StartBobbing();

        timer = 0f;
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        rotation = ctx.ReadValue<float>();
        
        if (isInAction)
        {
            return;
        }
        TryRotate();
    }

    private void TryRotate()
    {
        lastRotation = transform.rotation;
        if (rotation > 0f)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
            isRotating = true;
        }
        else if (rotation < 0f)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
            isRotating = true;
        }
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

    public void OnPause(InputAction.CallbackContext ctx)
    {
        isOnPause = !isOnPause;
        if (isOnPause)
        {
            ActivateBehaviours();
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
        else
        {
            DeactivateBehaviours();
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true); 
        }
    }

    public void DeactivateBehaviours()
    {
        foreach (MonoBehaviour argument in behaviours)
        {
            argument.gameObject.SetActive(false);
        }
    }

    public void ActivateBehaviours()
    {
        foreach (MonoBehaviour argument in behaviours)
        {
            argument.gameObject.SetActive(true);
        }
    }

}
