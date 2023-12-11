using UnityEngine;

public class EnnemyTest : MonoBehaviour
{
    [SerializeField] public Cell cellOn;
    public GameGrid grid;
    private Entity entity;

    [SerializeField] float life;

    void Start()
    {
        grid = GameGrid.instance;

        entity = GetComponent<Entity>();

        cellOn = grid.GetCell(3,3);

        cellOn.SetEntity(entity);

        transform.position = cellOn.pos;
    }

    public void TakeDamage(float amount)
    {
        life -= amount;
    }
}
