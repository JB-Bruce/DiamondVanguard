using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float pv;
    [SerializeField] int startX, startY;
    [SerializeField] bool snapToGrid;

    [SerializeField] float minMoveDelay;
    [SerializeField] float maxMoveDelay;

    float moveDelay;
    float stopTimer = 0f;
    
    [SerializeField] private float moveSpeed, rotationSpeed, attackSpeed;
    [SerializeField] float damages;
    [SerializeField] public Cell cellOn;
    [SerializeField] private List<Cell> closeCells = new List<Cell>();
    [SerializeField] private int index = 0;
    [SerializeField] private float rangeDistance = 2;
    private bool hasTarget = false;
    private int nbRot;
    private Cell targetCell;
    private int angleIndex;

    [SerializeField] Animator animator;

    CharactersControler cc;

    private Vector3 lastPosition, targetPosition;

    public GameGrid grid;

    PlayerMovement player;

    private bool isMoving = false;
    private bool isRotating;
    private bool isAttacking;
    public bool isInAction { get => isMoving || isRotating || isAttacking; }
    
    private Entity entity;
    private Quaternion lastRotation, targetRotation;
    private float timer;
    private bool dead;

    [SerializeField] LayerMask enemyLayer;
    void Start()
    {
        grid = GameGrid.instance;
        entity = GetComponent<Entity>();
        if (!snapToGrid)
        {
            cellOn = grid.GetCell(startX,startY);
        }
        else
        {
            cellOn = grid.GetClosestCell(transform.position);
        }
        moveDelay = Random.Range(minMoveDelay, maxMoveDelay);

        transform.position = cellOn.pos;
        player = PlayerMovement.instance;
        cc = CharactersControler.instance;

        Invoke("GoToCell", moveDelay);
    }

    void Update()
    {
        if (!dead)
        {
            if (isMoving)
            {
                //Go from a cell to another
                timer += Time.deltaTime * moveSpeed;
                timer = Mathf.Clamp01(timer);
                transform.position = Vector3.Lerp(lastPosition, targetPosition, timer);
                if (timer == 1f)
                {
                    animator.SetBool("isWalking", false);
                    stopTimer += Time.deltaTime;
                    if (stopTimer >= moveDelay || !hasTarget)
                    {
                        moveDelay = Random.Range(minMoveDelay, maxMoveDelay);
                        stopTimer = 0f;
                        isMoving = false;
                        hasTarget = false;
                        GoToCell();
                    }
                }
            }
            else if (isRotating)
            {
                //rotating
                timer += Time.deltaTime * rotationSpeed;
                timer = Mathf.Clamp01(timer);
                //transform.rotation = Quaternion.Lerp(lastRotation, targetRotation, timer);
                if (timer == 1f)
                {
                    isRotating = false;
                    GoToCell();
                }
            }
            else if (isAttacking)
            {
                timer += Time.deltaTime * attackSpeed;
                timer = Mathf.Clamp01(timer);
                if (timer == 1f)
                {
                    isAttacking = false;
                    GoToCell();
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        pv -= amount;
        if (pv <= 0)
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject,2);
            dead = true;
        }
    }

    private float DistancetoCell()
    {
        float rangeDistanceCell = rangeDistance * grid.cellSpacement;
        return rangeDistanceCell;
    }
    private Cell GetCloseCells()
    {
        var frontCell = GetCloseCell(1, 0);
        var backCell = GetCloseCell(-1, 0);
        var rightCell = GetCloseCell(0, 1);
        var leftCell = GetCloseCell(0, -1);

        List<Cell> nearCells = new List<Cell>();

        if(frontCell != null)
            nearCells.Add(frontCell);
        if(backCell != null)
            nearCells.Add(backCell);
        if(rightCell != null)
            nearCells.Add(rightCell);
        if(leftCell != null)
            nearCells.Add(leftCell);

        if(nearCells.Count == 0)
            return null;

        int randomIndex = Random.Range(0, nearCells.Count);
        return nearCells[randomIndex];
    }
    private Cell GetCloseCell(int x, int y)
    {
        Cell cell = grid.GetCell(cellOn.gridPos.Item1 + x, cellOn.gridPos.Item2 + y);

        //This part of the code is made by the lead dev, based on my previous works
        if (cell != null && !cell.HasEntity() && !WallDetection(cellOn.pos, cell.pos))
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

        timer = 0f;

        if (PlayerDetection())
        {
            hasTarget = false;
            targetCell = NextCellToGoToTarget(player.cellOn);
        }
        else if(!hasTarget)
        {
            if (!GetRandomTarget())
                return;
        }

        if (targetCell != null && targetCell.HasEntity())
            if (!GetRandomTarget())
                return;

        Vector3Int forward = Vector3Int.RoundToInt(transform.forward);
        Cell forwardCell = grid.GetCell(cellOn.gridPos.Item1 + forward.x, cellOn.gridPos.Item2 + forward.z);
        if (forwardCell == targetCell)
        {
            if (targetCell == player.cellOn)
                AttackPlayer();
            else
                MoveToCell(targetCell);
        }
        else
        {
            RotateCell();
        }
    }

    private bool GetRandomTarget()
    {
        hasTarget = true;
        targetCell = GetCloseCells();
        if (targetCell == null)
        {
            hasTarget = false;
            Invoke("GoToCell", 1f);
            return false;
        }
        return true;
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        cc.TakeDamage(damages);
        animator.SetTrigger("attack");
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

        if (finalCell.entity != null)
            finalCell = cellOn;

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
            animator.SetTrigger("isTurningL");
            Invoke("AnimationRotateWaitL", 1);
        }
        else if (target.eulerAngles.y < 180)
        {
            animator.SetTrigger("isTurningR");
            Invoke("AnimationRotateWaitR", 1);
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
        }
        isRotating = true;
    }

    private void AnimationRotateWaitR()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
    }

    private void AnimationRotateWaitL()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);
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
        animator.SetBool("isWalking", true);
    }
}
