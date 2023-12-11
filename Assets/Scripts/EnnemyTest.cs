using UnityEngine;

public class EnnemyTest : MonoBehaviour
{
    [SerializeField] public Cell cellOn;
    public GameGrid grid;
    private Entity entity;

    [SerializeField] float life;

    private CharactersControler player;

    private PlayerMovement playerPos;

    void Start()
    {
        grid = GameGrid.instance;

        playerPos = PlayerMovement.instance;

        player = CharactersControler.instance;

        entity = GetComponent<Entity>();

        cellOn = grid.GetCell(3,3);

        cellOn.SetEntity(entity);

        transform.position = cellOn.pos;

        InvokeRepeating("EnnemyAttack", 2.0f, 1f);
    }

    public void TakeDamage(float amount)
    {
        life -= amount;
    }

    public bool PlayerDetection() 
    { 
        if (grid.GetCell(cellOn.gridPos.Item1 + 1, cellOn.gridPos.Item2)==playerPos.cellOn)
        {
            return true;
        }
        else if (grid.GetCell(cellOn.gridPos.Item1 - 1, cellOn.gridPos.Item2) == playerPos.cellOn)
        {
            return true;
        }
        else if (grid.GetCell(cellOn.gridPos.Item1, cellOn.gridPos.Item2 + 1) == playerPos.cellOn)
        {
            return true;
        }
        else if (grid.GetCell(cellOn.gridPos.Item1, cellOn.gridPos.Item2 - 1) == playerPos.cellOn)
        {
            return true;
        }
        return false;
    }

    public void EnnemyAttack()
    {
        if (PlayerDetection())
        {
            player.TakeDamage(40);
        }
    }
}
