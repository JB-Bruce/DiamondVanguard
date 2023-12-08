using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnnemyTest : MonoBehaviour
{
    [SerializeField] public Cell cellOn;
    public GameGrid grid;
    private Entity entity;
    void Start()
    {
        grid = GameGrid.instance;

        entity = GetComponent<Entity>();

        cellOn = grid.GetCell(3,3);

        cellOn.SetEntity(entity);

        transform.position = cellOn.pos;
    }

    void Update()
    {
        
    }
}
