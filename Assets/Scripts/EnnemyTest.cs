using UnityEngine;

public class EnnemyTest : MonoBehaviour
{
    [SerializeField] public Cell cellOn;
    [SerializeField] int posX, posY;
    public GameGrid grid;
    private Entity entity;

    [SerializeField] float life;

    void Start()
    {
        grid = GameGrid.instance;

        entity = GetComponent<Entity>();

        cellOn = grid.GetCell(posX,posY);

        cellOn.SetEntity(entity);

        transform.position = cellOn.pos;
    }

    public void TakeDamage(float amount)
    {
        life -= amount;
    }
}
